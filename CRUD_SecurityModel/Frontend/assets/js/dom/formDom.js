import { fetchAllForms, registrarForm, getFormPorId, actualizarForm, eliminarForm} from "../api/formAPI.js"

export async function listarForms() {
    const data = await fetchAllForms();
    const tab_datos = document.getElementById("cuerpoTabla");
    let contador = 1
    tab_datos.innerHTML = "";

    if (data.length === 0) {
        tab_datos.innerHTML = `
            <tr>
                <td colspan="5" class="text-center text-muted">
                    <i class="fa-solid fa-hourglass"></i>
                    No Hay Nada Que Ver Aqui
                    <i class="fa-solid fa-hourglass"></i>
                </td>
            </tr>
        `;
        return;
    }

    data.forEach((form) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${form.name}</td>
                <td>${form.description}</td>
                <td>${form.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${form.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarForm">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${form.id}" data-name="${form.name}" class="btn btn-danger btn-sm btnEliminar">
                        <i class="fa-solid fa-trash icono-acciones"></i>
                    </button>
                </td>
            </tr>
        `;
    });

    document.querySelectorAll(".btnActualizar").forEach((btn) => {
        btn.addEventListener("click", function () {
            const id = this.getAttribute("data-id");
            // alert(id)
            inicializarModalActualizar(id);
        });
    });

    document.querySelectorAll(".btnEliminar").forEach((btn) => {
        btn.addEventListener("click", function () {
            const id = this.getAttribute("data-id");
            const name = this.getAttribute("data-name");

            // alert(id + name)
            Swal.fire({
                title: "¿Qué tipo de eliminación deseas?",
                text: `Form: ${name} (ID: ${id})`,
                icon: "warning",
                showCancelButton: true,
                showDenyButton: true,
                confirmButtonText: "Lógica",
                denyButtonText: "Permanente",
                cancelButtonText: "Cancelar",
                confirmButtonColor: "#3085d6",
                denyButtonColor: "#d33",
            }).then(async (result) => {
                if (result.isConfirmed) {
                    await eliminarConTipo(id, 0); // Lógica
                } else if (result.isDenied) {
                    await eliminarConTipo(id, 1); // Permanente
                }
            });
        });
    });
}

export function inicializarFormularioRegistro() {
    const formulario = document.getElementById("formularioForm");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroForm"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const form = {
            name: formulario.name.value,
            description: formulario.description.value,
        };

        try {
            await registrarForm(form);
            Swal.fire("¡Form registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarForms();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el form.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarForm");

    try {
        const form = await getFormPorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarForm" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${form.id}" />
                <div class="col-md-12">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" name="name" value="${form.name}" class="form-control" required>
                    
                    <label for="description" class="form-label">Description</label>
                    <input type="text" name="description" value="${form.description}" class="form-control" required>

                    <label for="status" class="form-label d-none">Estado</label>
                    <select class="form-select d-none" id="status" name="status">
                        <option selected disabled value="">Elije...</option>
                        <option value="true">Activo</option>
                        <option value="false">Desactivo</option>
                    </select>
                </div>
                <div class="col-md-12 text-center mt-3">
                    <button type="submit" class="btn btn-success">Actualizar</button>
                </div>
            </form>
        `;
        manejarSubmitActualizar();


    } catch (error) {
        Swal.fire("Error", "No se pudo cargar el form.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarForm");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarForm"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const form = {
            id: parseInt(formActualizar.id.value),
            name: formActualizar.name.value,
            description: formActualizar.description.value,
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        // console.log(form)

        try {
            await actualizarForm(form);
            Swal.fire("¡Form actualizado!", "", "success");
            modalActualizar.hide();
            await listarForms();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el form.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarForm(id, tipo);
        Swal.fire("¡Form eliminado!", "", "success");
        await listarForms();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el form.", "error");
    }
}

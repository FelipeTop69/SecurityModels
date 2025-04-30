import { fetchAllModules, registrarModule, getModulePorId, actualizarModule, eliminarModule} from "../api/moduleAPI.js"

export async function listarModules() {
    const data = await fetchAllModules();
    const tab_datos = document.getElementById("cuerpoTabla");
    let contador = 1;
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

    data.forEach((module) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++ }</td>
                <td>${module.name}</td>
                <td>${module.description}</td>
                <td>${module.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${module.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarModule">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${module.id}" data-name="${module.name}" class="btn btn-danger btn-sm btnEliminar">
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
                text: `Module: ${name} (ID: ${id})`,
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
    const formulario = document.getElementById("formularioModule");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroModule"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const module = {
            name: formulario.name.value,
            description: formulario.description.value
        };

        try {
            await registrarModule(module);
            Swal.fire("¡Module registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarModules();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el module.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarModule");

    try {
        const module = await getModulePorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarModule" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${module.id}" />
                <div class="col-md-12">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" name="name" value="${module.name}" class="form-control" required>
                    
                    <label for="description" class="form-label">Description</label>
                    <input type="text" name="description" value="${module.description}" class="form-control" required>

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
        Swal.fire("Error", "No se pudo cargar el module.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarModule");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarModule"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const module = {
            id: parseInt(formActualizar.id.value),
            name: formActualizar.name.value,
            description: formActualizar.description.value,
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        try {
            await actualizarModule(module);
            Swal.fire("¡Module actualizado!", "", "success");

            // console.log(module)

            modalActualizar.hide();
            await listarModules();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el module.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarModule(id, tipo);
        Swal.fire("¡Module eliminado!", "", "success");
        await listarModules();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el module.", "error");
    }
}
import { fetchAllRols, registrarRol, getRolPorId, actualizarRol, eliminarRol} from "../api/rolAPI.js"

export async function listarRols() {
    const data = await fetchAllRols();
    const tab_datos = document.getElementById("cuerpoTabla");
    let contador = 1;
    tab_datos.innerHTML = "";

    if (data.length === 0) {
        tab_datos.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-muted">
                    <i class="fa-solid fa-hourglass"></i>
                    No Hay Nada Que Ver Aqui
                    <i class="fa-solid fa-hourglass"></i>
                </td>
            </tr>
        `;
        return;
    }

    data.forEach((rol) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${rol.name}</td>
                <td>${rol.description}</td>
                <td>${rol.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${rol.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarRol">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${rol.id}" data-name="${rol.name}" class="btn btn-danger btn-sm btnEliminar">
                        <i class="fa-solid fa-trash icono-acciones"></i>
                    </button>
                </td>
            </tr>
        `;
    });

    document.querySelectorAll(".btnActualizar").forEach((btn) => {
        btn.addEventListener("click", function () {
            const id = this.getAttribute("data-id");
            inicializarModalActualizar(id);
        });
    });

    document.querySelectorAll(".btnEliminar").forEach((btn) => {
        btn.addEventListener("click", function () {
            const id = this.getAttribute("data-id");
            const name = this.getAttribute("data-name");

            Swal.fire({
                title: "¿Qué tipo de eliminación deseas?",
                text: `Rol: ${name} (ID: ${id})`,
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
    const formulario = document.getElementById("formularioRol");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroRol"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const rol = {
            name: formulario.name.value,
            description: formulario.description.value
        };

        try {
            await registrarRol(rol);
            Swal.fire("¡Rol registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarRols();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el rol.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarRol");

    try {
        const rol = await getRolPorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarRol" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${rol.id}" />
                <div class="col-md-12">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" name="name" value="${rol.name}" class="form-control" required>
                    
                    <label for="description" class="form-label">Description</label>
                    <input type="text" name="description" value="${rol.description}" class="form-control" required>

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
        Swal.fire("Error", "No se pudo cargar el rol.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarRol");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarRol"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const rol = {
            id: parseInt(formActualizar.id.value),
            name: formActualizar.name.value,
            description: formActualizar.description.value,
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        try {
            await actualizarRol(rol);
            Swal.fire("¡Rol actualizado!", "", "success");

            // console.log(rol)

            modalActualizar.hide();
            await listarRols();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el rol.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarRol(id, tipo);
        Swal.fire("¡Rol eliminado!", "", "success");
        await listarRols();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el rol.", "error");
    }
}
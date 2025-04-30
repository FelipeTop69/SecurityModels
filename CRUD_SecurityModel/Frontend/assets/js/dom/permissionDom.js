import { fetchAllPermissions, registrarPermission, getPermissionPorId, actualizarPermission, eliminarPermission} from "../api/permissionAPI.js"

export async function listarPermissions() {
    const data = await fetchAllPermissions();
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

    data.forEach((permission) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${permission.name}</td>
                <td>${permission.description}</td>
                <td>${permission.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${permission.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarPermission">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${permission.id}" data-name="${permission.name}" class="btn btn-danger btn-sm btnEliminar">
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
                text: `Permission: ${name} (ID: ${id})`,
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
    const formulario = document.getElementById("formularioPermission");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroPermission"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const permission = {
            name: formulario.name.value,
            description: formulario.description.value
        };

        try {
            await registrarPermission(permission);
            Swal.fire("¡Permission registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarPermissions();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el permission.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarPermission");

    try {
        const permission = await getPermissionPorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarPermission" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${permission.id}" />
                <div class="col-md-12">
                    <label for="name" class="form-label">Name</label>
                    <input type="text" name="name" value="${permission.name}" class="form-control" required>
                    
                    <label for="description" class="form-label">Description</label>
                    <input type="text" name="description" value="${permission.description}" class="form-control" required>

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
        Swal.fire("Error", "No se pudo cargar el permission.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarPermission");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarPermission"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const permission = {
            id: parseInt(formActualizar.id.value),
            name: formActualizar.name.value,
            description: formActualizar.description.value,
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        try {
            await actualizarPermission(permission);
            Swal.fire("¡Permission actualizado!", "", "success");

            // console.log(permission)

            modalActualizar.hide();
            await listarPermissions();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el permission.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarPermission(id, tipo);
        Swal.fire("¡Permission eliminado!", "", "success");
        await listarPermissions();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el permission.", "error");
    }
}
import { fetchAllRolFormPermissions, registrarRolFormPermission, getRolFormPermissionPorId, actualizarRolFormPermission, eliminarRolFormPermission, fetchAllRols , fetchAllForms, fetchAllPermissions} from "../api/rolFormPermissionAPI.js"

export async function listarRolFormPermissions() {
    const data = await fetchAllRolFormPermissions();
    const tab_datos = document.getElementById("cuerpoTabla");
    let contador = 1;
    tab_datos.innerHTML = "";

    if (data.length === 0) {
        tab_datos.innerHTML = `
            <tr>
                <td colspan="6" class="text-center text-muted">
                    <i class="fa-solid fa-hourglass"></i>
                    No Hay Nada Que Ver Aqui
                    <i class="fa-solid fa-hourglass"></i>
                </td>
            </tr>
        `;
        return;
    }

    data.forEach((rolFormPermission) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${rolFormPermission.rolName}</td>
                <td>${rolFormPermission.permissionName}</td>
                <td>${rolFormPermission.formName}</td>
                <td>${rolFormPermission.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${rolFormPermission.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarRolFormPermission">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${rolFormPermission.id}" class="btn btn-danger btn-sm btnEliminar">
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

            Swal.fire({
                title: "¿Qué tipo de eliminación deseas?",
                text: `RolFormPermission: ID: ${id}`,
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
    const formulario = document.getElementById("formularioRolFormPermission");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroRolFormPermission"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const rolFormPermission = {
            rolId: parseInt(formulario.rolId.value),
            permissionId: parseInt(formulario.permissionId.value),
            formId: parseInt(formulario.formId.value)
        };

        try {
            await registrarRolFormPermission(rolFormPermission);
            Swal.fire("¡RolFormPermission registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarRolFormPermissions();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el rolFormPermission.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarRolFormPermission");

    try {
        const rolFormPermission = await getRolFormPermissionPorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarRolFormPermission" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${rolFormPermission.id}" />
                <div class="col-md-12">
                    <label for="rolIdUpdate" class="form-label">Rol</label>
                    <select name="rolIdUpdate" id="rolIdUpdate" class="form-select" required>
                    </select>

                    <label for="permissionIdUpdate" class="form-label">Permission</label>
                    <select name="permissionIdUpdate" id="permissionIdUpdate" class="form-select" required>
                    </select>

                    <label for="formIdUpdate" class="form-label">Form</label>
                    <select name="formIdUpdate" id="formIdUpdate" class="form-select" required>
                    </select>

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

        await cargarRolsEnSelect("rolIdUpdate");
        await cargarPermissionsEnSelect("permissionIdUpdate");
        await cargarFormsEnSelect("formIdUpdate");
        document.getElementById("rolIdUpdate").value = rolFormPermission.rolId;
        document.getElementById("permissionIdUpdate").value = rolFormPermission.permissionId;
        document.getElementById("formIdUpdate").value = rolFormPermission.formId;
        
        manejarSubmitActualizar();


    } catch (error) {
        Swal.fire("Error", "No se pudo cargar el rolFormPermission.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarRolFormPermission");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarRolFormPermission"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const rolFormPermission = {
            id: parseInt(formActualizar.id.value),
            rolId: parseInt(formActualizar.rolIdUpdate.value),
            permissionId: parseInt(formActualizar.permissionIdUpdate.value),
            formId: parseInt(formActualizar.formIdUpdate.value),
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        try {
            await actualizarRolFormPermission(rolFormPermission);
            Swal.fire("¡RolFormPermission actualizado!", "", "success");

            // console.log(rolFormPermission)

            modalActualizar.hide();
            await listarRolFormPermissions();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el rolFormPermission.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarRolFormPermission(id, tipo);
        Swal.fire("¡RolFormPermission eliminado!", "", "success");
        await listarRolFormPermissions();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el rolFormPermission.", "error");
    }
}

export async function cargarRolsEnSelect(selectId) {
    const select = document.getElementById(selectId);
    select.innerHTML = `<option selected disabled value="">Elije...</option>`;

    try {
        const rols = await fetchAllRols();

        rols.forEach(rol => {
            select.innerHTML += `<option value="${rol.id}">${rol.name}</option>`;
        });

    } catch (error) {
        Swal.fire("Error", "No se pudieron cargar las rols.", "error");
    }
}

export async function cargarPermissionsEnSelect(selectId) {
    const select = document.getElementById(selectId);
    select.innerHTML = `<option selected disabled value="">Elije...</option>`;

    try {
        const permissions = await fetchAllPermissions();

        permissions.forEach(permission => {
            select.innerHTML += `<option value="${permission.id}">${permission.name}</option>`;
        });

    } catch (error) {
        Swal.fire("Error", "No se pudieron cargar las permissions.", "error");
    }
}

export async function cargarFormsEnSelect(selectId) {
    const select = document.getElementById(selectId);
    select.innerHTML = `<option selected disabled value="">Elije...</option>`;

    try {
        const forms = await fetchAllForms();

        forms.forEach(form => {
            select.innerHTML += `<option value="${form.id}">${form.name}</option>`;
        });

    } catch (error) {
        Swal.fire("Error", "No se pudieron cargar las permissions.", "error");
    }
}
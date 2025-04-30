import { fetchAllRolUsers, registrarRolUser, getRolUserPorId, actualizarRolUser, eliminarRolUser, fetchAllRols, fetchAllUsers} from "../api/rolUserAPI.js"

export async function listarRolUsers() {
    const data = await fetchAllRolUsers();
    const tab_datos = document.getElementById("cuerpoTabla");
    let contador = 1;
    tab_datos.innerHTML = "";

    if (data.length === 0) {
        tab_datos.innerHTML = `
            <tr>
                <td colspan="7" class="text-center text-muted">
                    <i class="fa-solid fa-hourglass"></i>
                    No Hay Nada Que Ver Aqui
                    <i class="fa-solid fa-hourglass"></i>
                </td>
            </tr>
        `;
        return;
    }

    data.forEach((rolUser) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${rolUser.rolName}</td>
                <td>${rolUser.userName}</td>
                <td>${rolUser.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${rolUser.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarRolUser">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${rolUser.id}" class="btn btn-danger btn-sm btnEliminar">
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
                text: `RolUser: ID: ${id}`,
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
    const formulario = document.getElementById("formularioRolUser");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroRolUser"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const rolUser = {
            rolId: parseInt(formulario.rolId.value),
            userId: parseInt(formulario.userId.value)
        };

        try {
            await registrarRolUser(rolUser);
            Swal.fire("¡RolUser registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarRolUsers();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el rolUser.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarRolUser");

    try {
        const rolUser = await getRolUserPorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarRolUser" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${rolUser.id}" />
                <div class="col-md-12">
                    <label for="rolIdUpdate" class="form-label">Rol</label>
                    <select name="rolIdUpdate" id="rolIdUpdate" class="form-select" required>
                    </select>

                    <label for="userIdUpdate" class="form-label">User</label>
                    <select name="userIdUpdate" id="userIdUpdate" class="form-select" required>
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
        await cargarUsersEnSelect("userIdUpdate");
        document.getElementById("rolIdUpdate").value = rolUser.rolId;
        document.getElementById("userIdUpdate").value = rolUser.userId;
        
        manejarSubmitActualizar();


    } catch (error) {
        Swal.fire("Error", "No se pudo cargar el rolUser.", "error");
    }

}

function manejarSubmitActualizar() {
    const rolActualizar = document.getElementById("formActualizarRolUser");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarRolUser"));

    rolActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const rolUser = {
            id: parseInt(rolActualizar.id.value),
            rolId: parseInt(rolActualizar.rolIdUpdate.value),
            userId: parseInt(rolActualizar.userIdUpdate.value),
            status: rolActualizar.status.value === "true" || rolActualizar.status.value === ""
        };

        try {
            await actualizarRolUser(rolUser);
            Swal.fire("¡RolUser actualizado!", "", "success");

            // console.log(rolUser)

            modalActualizar.hide();
            await listarRolUsers();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el rolUser.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarRolUser(id, tipo);
        Swal.fire("¡RolUser eliminado!", "", "success");
        await listarRolUsers();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el rolUser.", "error");
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
        Swal.fire("Error", "No se pudieron cargar las users.", "error");
    }
}

export async function cargarUsersEnSelect(selectId) {
    const select = document.getElementById(selectId);
    select.innerHTML = `<option selected disabled value="">Elije...</option>`;

    try {
        const users = await fetchAllUsers();

        console.log(users)
        users.forEach(user => {
            select.innerHTML += `<option value="${user.id}">${user.username}</option>`;
        });

    } catch (error) {
        Swal.fire("Error", "No se pudieron cargar las users.", "error");
    }
}
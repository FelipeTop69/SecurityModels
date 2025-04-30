import { fetchAllUsers, registrarUsuario, getUsuarioPorId, actualizarUsuario, eliminarUsuario, fetchAvailablePersons } from "../api/userAPI.js"
import {getRol} from "../../../auth/js/authService.js"

export async function listarUsuarios() {
    const data = await fetchAllUsers();
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

    data.forEach((user) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${user.username}</td>
                <td>${user.password}</td>
                <td>${user.personName}</td>
                <td>${user.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${user.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarUser">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${user.id}" data-username="${user.username}" class="btn btn-danger btn-sm btnEliminar">
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
            const username = this.getAttribute("data-username");

            Swal.fire({
                title: "¿Qué tipo de eliminación deseas?",
                text: `Usuario: ${username} (ID: ${id})`,
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
    const formulario = document.getElementById("formularioUser");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroUser"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const user = {
            username: formulario.username.value,
            password: formulario.password.value,
            personId: parseInt(formulario.personId.value)
        };

        try {
            await registrarUsuario(user);
            Swal.fire("¡Usuario registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarUsuarios();
            await cargarPersonasEnSelect("personId");
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el usuario.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarUser");

    try {
        const user = await getUsuarioPorId(id);
        const isAdmin = getRol() === "Administrador";

        modalBody.innerHTML = `
            <form id="formActualizarUser" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${user.id}" />
                <div class="col-md-12">
                    <label for="username" class="form-label">Username</label>
                    <input type="text" name="username" value="${user.username}" class="form-control" required>
                    
                    <label for="password" class="form-label">Password</label>
                    <input type="text" name="password" class="form-control" required>

                    <input type="hidden" name="personIdUpdate" value="${user.personId}" />
                    

                    <label for="status" class="form-label ${isAdmin ? "" : "d-none"}">Estado</label>
                    <select class="form-select ${isAdmin ? "" : "d-none"}" id="status" name="status">
                        <option selected disabled value="">Elije...</option>
                        <option value="true" ${user.status === true ? "selected" : ""}>Activo</option>
                        <option value="false ${user.status === true ? "selected" : ""}">Desactivo</option>
                    </select>
                </div>
                <div class="col-md-12 text-center mt-3">
                    <button type="submit" class="btn btn-success">Actualizar</button>
                </div>
            </form>
        `;
        manejarSubmitActualizar();


    } catch (error) {
        Swal.fire("Error", "No se pudo cargar el usuario.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarUser");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarUser"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const user = {
            id: parseInt(formActualizar.id.value),
            username: formActualizar.username.value,
            password: formActualizar.password.value,
            personId: parseInt(formActualizar.personIdUpdate.value),
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        // console.log(user)
        try {
            await actualizarUsuario(user);
            Swal.fire("¡Usuario actualizado!", "", "success");


            modalActualizar.hide();
            await listarUsuarios();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el usuario.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarUsuario(id, tipo);
        Swal.fire("¡Usuario eliminado!", "", "success");
        await listarUsuarios();
        await cargarPersonasEnSelect("personId");
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el usuario.", "error");
    }
}

export async function cargarPersonasEnSelect(selectId) {
    const select = document.getElementById(selectId);
    select.innerHTML = `<option selected disabled value="">Elije...</option>`;

    try {
        const persons = await fetchAvailablePersons();

        if (persons.length === 0) {
            select.innerHTML = `<option selected disabled>No hay personas disponibles</option>`;
            select.disabled = true;
            document.querySelector("#formularioUser button[type='submit']").disabled = true;
            return;
        }

        persons.forEach(person => {
            select.innerHTML += `<option value="${person.id}">${person.name}</option>`;
        });

        select.disabled = false;
        document.querySelector("#formularioUser button[type='submit']").disabled = false;

    } catch (error) {
        Swal.fire("Error", "No se pudieron cargar las personas.", "error");
    }
}
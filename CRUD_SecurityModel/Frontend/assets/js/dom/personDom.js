import { fetchAllPersons, registrarPerson, getPersonPorId, actualizarPerson, eliminarPerson} from "../api/personAPI.js"

export async function listarPersons() {
    const data = await fetchAllPersons();
    const tab_datos = document.getElementById("cuerpoTabla");
    let contador = 1;
    tab_datos.innerHTML = "";

    if (data.length === 0) {
        tab_datos.innerHTML = `
            <tr>
                <td colspan="10" class="text-center text-muted">
                    <i class="fa-solid fa-hourglass"></i>
                    No Hay Nada Que Ver Aqui
                    <i class="fa-solid fa-hourglass"></i>
                </td>
            </tr>
        `;
        return;
    }

    data.forEach((person) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${person.name}</td>
                <td>${person.lastName}</td>
                <td>${person.email}</td>
                <td>${person.documentType}</td>
                <td>${person.documentNumber}</td>
                <td>${person.phone}</td>
                <td>${person.address}</td>
                <td>${person.bloodType}</td>
                <td>${person.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${person.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarPerson">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${person.id}" data-name="${person.name}" class="btn btn-danger btn-sm btnEliminar">
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
                text: `Person: ${name} (ID: ${id})`,
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
    const formulario = document.getElementById("formularioPerson");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroPerson"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const person = {
            name: formulario.name.value,
            lastName: formulario.lastName.value,
            email: formulario.email.value,
            documentType: formulario.documentType.value,
            documentNumber: formulario.documentNumber.value,
            phone: formulario.phone.value,
            address: formulario.address.value,
            bloodType: formulario.bloodType.value
        };

        try {
            await registrarPerson(person);
            Swal.fire("¡Person registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarPersons();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el person.", "error");
        }
    });
}

async function inicializarModalActualizar(id) {
    const modalBody = document.getElementById("cuerpoModalActualizarPerson");

    try {
        const person = await getPersonPorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarPerson" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${person.id}" />
                <div class="col-md-12">
                    <label for="name" class="form-label">Nombre</label>
                    <input type="text" name="name" value="${person.name}" class="form-control" required>

                    <label for="lastName" class="form-label">Apellido</label>
                    <input type="text" name="lastName" value="${person.lastName}" class="form-control" required>

                    <label for="email" class="form-label">Email</label>
                    <input type="email" name="email" value="${person.email}" class="form-control" required>

                    <label for="documentType" class="form-label">Tipo de Documento</label>
                    <select name="documentType" id="documentType" class="form-select" required>
                        <option value="">Seleccione un tipo</option>
                        <option value="RC" ${person.documentType === "RC" ? "selected" : ""}>Registro Civil</option>
                        <option value="TI" ${person.documentType === "TI" ? "selected" : ""}>Tarjeta de Identidad</option>
                        <option value="CC" ${person.documentType === "CC" ? "selected" : ""}>Cédula de Ciudadanía</option>
                        <option value="CE" ${person.documentType === "CE" ? "selected" : ""}>Cédula de Extranjería</option>
                        <option value="NIT" ${person.documentType  === "NIT" ? "selected" : ""}>N° Identificación Tributaria</option>
                        <option value="PP" ${person.documentType === "PP" ? "selected" : ""}>Pasaporte</option>
                    </select>

                    <label for="documentNumber" class="form-label">Numero de Documento</label>
                    <input type="text" name="documentNumber" id="documentNumber" class="form-control" value="${person.documentNumber}" placeholder="Digite el Número de Documento" required 
                        pattern="[0-9]{1,10}" maxlength="12" title="Solo números, máximo 12 dígitos">

                    <label for="phone" class="form-label">Phone</label>
                    <input type="text" name="phone" id="phone" class="form-control" value="${person.phone}" placeholder="Digite el Phone" required 
                        pattern="[0-9]{1,15}" maxlength="10" title="Solo numeros, máximo 10 digitos">

                    <label for="address" class="form-label">Dirección</label>
                    <input type="text" name="address" value="${person.address}" class="form-control" required>

                    <label for="bloodType" class="form-label">Tipo de Sangre</label>
                    <select name="bloodType" id="bloodType" class="form-select" required>
                        <option value="">Seleccione un tipo</option>
                        <option value="A+" ${person.bloodType === "A+" ? "selected" : ""}>A+</option>
                        <option value="A-" ${person.bloodType === "A-" ? "selected" : ""}>A-</option>
                        <option value="B+" ${person.bloodType === "B+" ? "selected" : ""}>B+</option>
                        <option value="B-" ${person.bloodType === "B-" ? "selected" : ""}>B-</option>
                        <option value="AB+" ${person.bloodType === "AB+" ? "selected" : ""}>AB+</option>
                        <option value="AB-" ${person.bloodType === "AB-" ? "selected" : ""}>AB-</option>
                        <option value="O+" ${person.bloodType === "O+" ? "selected" : ""}>O+</option>
                        <option value="O-" ${person.bloodType === "O-" ? "selected" : ""}>O-</option>
                    </select>

                    <label for="status" class="form-label d-none">Estado</label>
                    <select class="form-select d-none" id="status" name="status">
                        <option disabled value="">Elije...</option>
                        <option value="true" ${person.status === true ? "selected" : ""}>Activo</option>
                        <option value="false" ${person.status === false ? "selected" : ""}>Desactivado</option>
                    </select>
                </div>
                <div class="col-md-12 text-center mt-3">
                    <button type="submit" class="btn btn-success">Actualizar</button>
                </div>
            </form>
        `;

        manejarSubmitActualizar();
    } catch (error) {
        Swal.fire("Error", "No se pudo cargar el person.", "error");
    }
}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarPerson");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarPerson"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const person = {
            id: parseInt(formActualizar.id.value),
            name: formActualizar.name.value,
            lastName: formActualizar.lastName.value,
            email: formActualizar.email.value,
            documentType: formActualizar.documentType.value,
            documentNumber: formActualizar.documentNumber.value,
            phone: formActualizar.phone.value,
            address: formActualizar.address.value,
            bloodType: formActualizar.bloodType.value,
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        // console.log(person)

        try {
            await actualizarPerson(person);
            Swal.fire("¡Person actualizado!", "", "success");

            modalActualizar.hide();
            await listarPersons();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el person.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarPerson(id, tipo);
        Swal.fire("¡Person eliminado!", "", "success");
        await listarPersons();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el person.", "error");
    }
}


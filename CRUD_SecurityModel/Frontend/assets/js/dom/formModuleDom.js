import { fetchAllFormModules, registrarFormModule, getFormModulePorId, actualizarFormModule, eliminarFormModule, fetchAllForms, fetchAllModules} from "../api/formModuleAPI.js"

export async function listarFormModules() {
    const data = await fetchAllFormModules();
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

    data.forEach((formModule) => {
        tab_datos.innerHTML += `
            <tr>
                <td>${contador++}</td>
                <td>${formModule.formName}</td>
                <td>${formModule.moduleName}</td>
                <td>${formModule.status ? 'Activo' : 'Inactivo'}</td>
                <td>
                    <button type="button" data-id="${formModule.id}" class="btn btn-warning btn-sm btnActualizar" data-bs-toggle="modal" data-bs-target="#modalActualizarFormModule">
                        <i class="fa-solid fa-pen-to-square icono-acciones"></i>
                    </button>
                    <button type="button" data-id="${formModule.id}" class="btn btn-danger btn-sm btnEliminar">
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

            // alert(id)
            Swal.fire({
                title: "¿Qué tipo de eliminación deseas?",
                text: `FormModule: ID: ${id}`,
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
    const formulario = document.getElementById("formularioFormModule");
    const modalRegistro = new bootstrap.Modal(document.getElementById("modalRegistroFormModule"));

    formulario.addEventListener("submit", async (e) => {
        e.preventDefault();

        const formModule = {
            formId: parseInt(formulario.formId.value),
            moduleId: parseInt(formulario.moduleId.value)
        };

        try {
            await registrarFormModule(formModule);
            Swal.fire("¡FormModule registrado!", "", "success");

            formulario.reset();
            modalRegistro.hide();
            await listarFormModules();
        } catch (error) {
            Swal.fire("Error", "No se pudo registrar el formModule.", "error");
        }
    });
}

async function  inicializarModalActualizar(id) {

    const modalBody = document.getElementById("cuerpoModalActualizarFormModule");

    try {
        const formModule = await getFormModulePorId(id);

        modalBody.innerHTML = `
            <form id="formActualizarFormModule" class="row formulario-registro needs-validation">
                <input type="hidden" name="id" value="${formModule.id}" />
                <div class="col-md-12">
                    <label for="formIdUpdate" class="form-label">Form</label>
                    <select name="formIdUpdate" id="formIdUpdate" class="form-select" required>
                    </select>

                    <label for="moduleIdUpdate" class="form-label">Module</label>
                    <select name="moduleIdUpdate" id="moduleIdUpdate" class="form-select" required>
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

        await cargarFormsEnSelect("formIdUpdate");
        await cargarModulesEnSelect("moduleIdUpdate");
        document.getElementById("formIdUpdate").value = formModule.formId;
        document.getElementById("moduleIdUpdate").value = formModule.moduleId;
        
        manejarSubmitActualizar();


    } catch (error) {
        Swal.fire("Error", "No se pudo cargar el formModule.", "error");
    }

}

function manejarSubmitActualizar() {
    const formActualizar = document.getElementById("formActualizarFormModule");
    const modalActualizar = bootstrap.Modal.getInstance(document.getElementById("modalActualizarFormModule"));

    formActualizar.addEventListener("submit", async (e) => {
        e.preventDefault();

        const formModule = {
            id: parseInt(formActualizar.id.value),
            formId: parseInt(formActualizar.formIdUpdate.value),
            moduleId: parseInt(formActualizar.moduleIdUpdate.value),
            status: formActualizar.status.value === "true" || formActualizar.status.value === ""
        };

        try {
            await actualizarFormModule(formModule);
            Swal.fire("¡FormModule actualizado!", "", "success");

            // console.log(formModule)

            modalActualizar.hide();
            await listarFormModules();
        } catch (error) {
            Swal.fire("Error", "No se pudo actualizar el formModule.", "error");
        }
    });
}

async function eliminarConTipo(id, tipo) {
    try {
        await eliminarFormModule(id, tipo);
        Swal.fire("¡FormModule eliminado!", "", "success");
        await listarFormModules();
    } catch (error) {
        Swal.fire("Error", "No se pudo eliminar el formModule.", "error");
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
        Swal.fire("Error", "No se pudieron cargar las modules.", "error");
    }
}

export async function cargarModulesEnSelect(selectId) {
    const select = document.getElementById(selectId);
    select.innerHTML = `<option selected disabled value="">Elije...</option>`;

    try {
        const modules = await fetchAllModules();

        modules.forEach(module => {
            select.innerHTML += `<option value="${module.id}">${module.name}</option>`;
        });

    } catch (error) {
        Swal.fire("Error", "No se pudieron cargar las modules.", "error");
    }
}
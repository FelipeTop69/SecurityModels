import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarFormModules, inicializarFormularioRegistro, cargarFormsEnSelect, cargarModulesEnSelect } from "./dom/formModuleDom.js";

protegerVista();
document.addEventListener("DOMContentLoaded", () => {
    incluirNavbar();
    inicializarExtensionSesion()
    listarFormModules();
    inicializarFormularioRegistro();
    cargarFormsEnSelect("formId");
    cargarModulesEnSelect("moduleId")
});
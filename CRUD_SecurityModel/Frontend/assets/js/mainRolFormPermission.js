import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarRolFormPermissions, inicializarFormularioRegistro, cargarRolsEnSelect, cargarPermissionsEnSelect, cargarFormsEnSelect  } from "./dom/rolFormPermissionDom.js";

protegerVista();
document.addEventListener("DOMContentLoaded", () => {
    incluirNavbar();
    inicializarExtensionSesion()
    listarRolFormPermissions();
    inicializarFormularioRegistro();
    cargarRolsEnSelect("rolId");
    cargarPermissionsEnSelect("permissionId");
    cargarFormsEnSelect("formId");
});
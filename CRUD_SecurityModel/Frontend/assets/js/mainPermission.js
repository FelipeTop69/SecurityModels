import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarPermissions, inicializarFormularioRegistro} from "./dom/permissionDom.js";

protegerVista();
document.addEventListener("DOMContentLoaded", () => {
    incluirNavbar();
    inicializarExtensionSesion()
    listarPermissions();
    inicializarFormularioRegistro();
});
    


import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarModules, inicializarFormularioRegistro} from "./dom/moduleDom.js";

protegerVista();
document.addEventListener("DOMContentLoaded", () => {
    incluirNavbar();
    inicializarExtensionSesion()
    listarModules();
    inicializarFormularioRegistro();
});
    


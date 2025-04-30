import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarPersons, inicializarFormularioRegistro} from "./dom/personDom.js";

protegerVista();
document.addEventListener("DOMContentLoaded", () => {
    incluirNavbar();
    inicializarExtensionSesion()
    listarPersons();
    inicializarFormularioRegistro();
});
    


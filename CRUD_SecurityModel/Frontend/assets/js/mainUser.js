import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarUsuarios, inicializarFormularioRegistro, cargarPersonasEnSelect  } from "./dom/userDom.js";

document.addEventListener("DOMContentLoaded", () => {
    protegerVista();
    incluirNavbar();
    inicializarExtensionSesion();
    listarUsuarios();
    inicializarFormularioRegistro();
    cargarPersonasEnSelect("personId");
});
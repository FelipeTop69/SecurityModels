import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { incluirNavbar } from "../../utils/include.js";
import { listarRolUsers, inicializarFormularioRegistro, cargarRolsEnSelect, cargarUsersEnSelect } from "./dom/rolUserdDom.js";

protegerVista();
document.addEventListener("DOMContentLoaded", () => {
    incluirNavbar();
    inicializarExtensionSesion()
    listarRolUsers();
    inicializarFormularioRegistro();
    cargarRolsEnSelect("rolId");
    cargarUsersEnSelect("userId")
});
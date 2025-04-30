import { protegerVista, inicializarExtensionSesion} from "../../auth/js/authGuard.js";
import { configurarBotonLogout } from "../../auth/js/authGuard.js";

protegerVista();
document.addEventListener('DOMContentLoaded', () =>{
    inicializarExtensionSesion();
    configurarBotonLogout();
});
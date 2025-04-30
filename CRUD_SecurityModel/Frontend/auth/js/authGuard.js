import { getToken, logout, extenderSesion } from "./authService.js";

/**
 * Protege una vista: si no hay token válido, redirige al login.
 */
export function protegerVista() {
    const token = getToken();

    if (!token) {
        logout();
    }
}

/**
 * Añade comportamiento al botón de logout.
 * Espera un botón con ID `btnLogout` en el DOM.
 */
export function configurarBotonLogout() {
    const btn = document.getElementById("btnLogout");

    if (btn) {
        btn.addEventListener("click", () => {
            Swal.fire({
                title: "¿Cerrar sesión?",
                icon: "question",
                showCancelButton: true,
                confirmButtonText: "Sí, salir",
                cancelButtonText: "Cancelar"
            }).then(result => {
                if (result.isConfirmed) {
                    logout();
                }
            });
        });
    }
}

export function inicializarExtensionSesion() {
    const eventos = ["click", "keydown", "mousemove", "scroll"];
    eventos.forEach(e =>
        document.addEventListener(e, extenderSesion)
    );
}
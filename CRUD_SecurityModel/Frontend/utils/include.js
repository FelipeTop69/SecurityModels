import { configurarBotonLogout } from "../auth/js/authGuard.js";

export async function incluirNavbar() {
    const container = document.getElementById("sidebarContainer");
    if (!container) return;

    try {
        const res = await fetch("../components/navbar.html");
        const html = await res.text();
        container.innerHTML = html;

        activarNavItemActual();
        configurarBotonLogout()
        cargarSidebarScript(); // 👈 Recargar el script
    } catch (err) {
        console.error("Error al cargar navbar:", err);
    }
}

function activarNavItemActual() {
    const currentPage = location.pathname.split("/").pop();
    document.querySelectorAll("#accordionSidebar .nav-item").forEach((item) => {
        const link = item.querySelector("a.nav-link");
        const href = link?.getAttribute("href");
        if (href && currentPage === href) {
            item.classList.add("active");
        } else {
            item.classList.remove("active");
        }
    });
}

function cargarSidebarScript() {
    const script = document.createElement("script");
    script.src = "../assets/js/dom/demo/sb-admin-2.min.js";
    script.defer = true;
    document.body.appendChild(script);
}

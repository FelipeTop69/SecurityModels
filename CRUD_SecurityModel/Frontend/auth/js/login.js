import { login, iniciarSesionTemporal } from "./authService.js";

document.getElementById("loginForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const username = e.target.username.value;
    const password = e.target.password.value;

    try {
        const user = await login(username, password);
        iniciarSesionTemporal(user);

        Swal.fire("Bienvenido", `Hola ${user.username}`, "success").then(() => {
            window.location.href = "../../views/user.html";
        });
    } catch (error) {
        Swal.fire("Error", "Credenciales inválidas", "error");
    }
});

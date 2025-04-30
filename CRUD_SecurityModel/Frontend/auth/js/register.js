import { getHeaders } from "../../utils/requestOptions.js";

document.addEventListener("DOMContentLoaded", async () => {
    await cargarRoles();

    document.getElementById("registerForm").addEventListener("submit", async (e) => {
        e.preventDefault();

        const form = e.target;
        const data = {
            name: form.name.value,
            lastName: form.lastName.value,
            email: form.email.value,
            documentNumber: form.documentNumber.value,
            phone: form.phone.value,
            address: form.address.value,
            documentType: form.documentType.value,
            bloodType: form.bloodType.value,
            username: form.username.value,
            password: form.password.value,
            rolId: parseInt(form.rolId.value)
        };

        console.log(data)

        try {
            const res = await fetch("https://localhost:7258/api/Auth/Register", {
                method: "POST",
                headers: getHeaders(),
                body: JSON.stringify(data)
            });

            if (!res.ok) throw new Error("Error al registrar");

            Swal.fire("¡Registrado!", "Tu cuenta fue creada con éxito", "success").then(() => {
                window.location.href = "login.html";
            });
        } catch (err) {
            Swal.fire("Error", "No se pudo registrar el usuario", "error");
            console.error(err);
        }
    });
});

async function cargarRoles() {
    try {
        const res = await fetch("https://localhost:7258/api/Rol/GetAll", {
            method: "GET",
            headers: getHeaders()
        });

        const roles = await res.json();
        const select = document.getElementById("rolId");
        roles.forEach((rol) => {
            select.innerHTML += `<option value="${rol.id}">${rol.name}</option>`;
        });
    } catch (err) {
        Swal.fire("Error", "No se pudieron cargar los roles", "error");
    }
}

import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/User/";

export async function fetchAllUsers() {
    try {
        const response = await fetch("https://localhost:7258/api/User/GetAllJWT", {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener usuarios");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar usuarios:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los usuarios.",
        });
        return [];
    }
}

export async function registrarUsuario(userData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(userData)
        });

        if (!response.ok) throw new Error("Error al registrar usuario");

        return await response.json();
    } catch (error) {
        console.error("Error registrando usuario:", error);
        throw error;
    }
}

export async function getUsuarioPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetByiId/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener usuario");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener usuario:", error);
        throw error;
    }
}

export async function actualizarUsuario(userData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(userData)
        });

        if (!response.ok) throw new Error("Error al actualizar usuario");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar usuario:", error);
        throw error;
    }
}

export async function eliminarUsuario(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar usuario");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando usuario:", error);
        throw error;
    }
}

export async function fetchAvailablePersons() {
    try {
        const response = await fetch("https://localhost:7258/api/Person/GetAvailable", {
            method: "GET",
            headers: getHeaders()

        });

        if (!response.ok) throw new Error("Error al obtener personas disponibles");

        return await response.json();
    } catch (error) {
        console.error("Error al consultar personas disponibles:", error);
        return [];
    }
}
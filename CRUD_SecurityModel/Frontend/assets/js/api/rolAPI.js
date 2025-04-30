import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/Rol/";

export async function fetchAllRols() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener Rols");

        return await response.json();
        // let respuesta = await response.json()
        console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar Rols:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los Rols.",
        });
        return [];
    }
}

export async function registrarRol(rolData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(rolData)
        });

        if (!response.ok) throw new Error("Error al registrar rol");

        return await response.json();
    } catch (error) {
        console.error("Error registrando rol:", error);
        throw error;
    }
}

export async function getRolPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetById/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener rol");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener rol:", error);
        throw error;
    }
}

export async function actualizarRol(rolData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(rolData)
        });

        if (!response.ok) throw new Error("Error al actualizar rol");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar rol:", error);
        throw error;
    }
}

export async function eliminarRol(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar rol");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando rol:", error);
        throw error;
    }
}
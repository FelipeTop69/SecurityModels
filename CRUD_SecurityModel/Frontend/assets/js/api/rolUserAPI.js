import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/Roluser/";

export async function fetchAllRolUsers() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener rolusers");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar rolusers:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los rolusers.",
        });
        return [];
    }
}

export async function registrarRolUser(roluserData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(roluserData)
        });

        if (!response.ok) throw new Error("Error al registrar roluser");

        return await response.json();
    } catch (error) {
        console.error("Error registrando roluser:", error);
        throw error;
    }
}

export async function getRolUserPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetByiId/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener roluser");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener roluser:", error);
        throw error;
    }
}

export async function actualizarRolUser(roluserData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(roluserData)
        });

        if (!response.ok) throw new Error("Error al actualizar roluser");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar roluser:", error);
        throw error;
    }
}

export async function eliminarRolUser(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar roluser");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando roluser:", error);
        throw error;
    }
}


export async function fetchAllRols() {
    try {
        const response = await fetch("https://localhost:7258/api/Rol/GetAll/", {
            method: "GET",
            headers: getHeaders()

        });

        if (!response.ok) throw new Error("Error al obtener rols");

        return await response.json();
    } catch (error) {
        console.error("Error al consultar rols:", error);
        return [];
    }
}

export async function fetchAllUsers() {
    try {
        const response = await fetch("https://localhost:7258/api/User/GetAll/", {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener users");

        return await response.json();
    } catch (error) {
        console.error("Error al consultar users:", error);
        return [];
    }
}
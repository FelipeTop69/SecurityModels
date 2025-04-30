import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/Module/";

export async function fetchAllModules() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener Modules");

        return await response.json();
        // let respuesta = await response.json()
        console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar Modules:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los Modules.",
        });
        return [];
    }
}

export async function registrarModule(moduleData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(moduleData)
        });

        if (!response.ok) throw new Error("Error al registrar module");

        return await response.json();
    } catch (error) {
        console.error("Error registrando module:", error);
        throw error;
    }
}

export async function getModulePorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetById/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener module");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener module:", error);
        throw error;
    }
}

export async function actualizarModule(moduleData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(moduleData)
        });

        if (!response.ok) throw new Error("Error al actualizar module");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar module:", error);
        throw error;
    }
}

export async function eliminarModule(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar module");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando module:", error);
        throw error;
    }
}
import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/Form/";

export async function fetchAllForms() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener Forms");

        return await response.json();
        // let respuesta = await response.json()
        console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar Forms:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los Forms.",
        });
        return [];
    }
}

export async function registrarForm(formData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(formData)
        });

        if (!response.ok) throw new Error("Error al registrar form");

        return await response.json();
    } catch (error) {
        console.error("Error registrando form:", error);
        throw error;
    }
}

export async function getFormPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetById/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener form");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener form:", error);
        throw error;
    }
}

export async function actualizarForm(formData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(formData)
        });

        if (!response.ok) throw new Error("Error al actualizar form");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar form:", error);
        throw error;
    }
}

export async function eliminarForm(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar form");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando form:", error);
        throw error;
    }
}

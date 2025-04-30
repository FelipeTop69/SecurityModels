import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/FormModule/";

export async function fetchAllFormModules() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener formModules");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar formModules:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los formModules.",
        });
        return [];
    }
}

export async function registrarFormModule(formModuleData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(formModuleData)
        });

        if (!response.ok) throw new Error("Error al registrar formModule");

        return await response.json();
    } catch (error) {
        console.error("Error registrando formModule:", error);
        throw error;
    }
}

export async function getFormModulePorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetByiId/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener formModule");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener formModule:", error);
        throw error;
    }
}

export async function actualizarFormModule(formModuleData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(formModuleData)
        });

        if (!response.ok) throw new Error("Error al actualizar formModule");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar formModule:", error);
        throw error;
    }
}

export async function eliminarFormModule(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar formModule");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando formModule:", error);
        throw error;
    }
}

export async function fetchAllForms() {
    try {
        const response = await fetch("https://localhost:7258/api/Form/GetAll/", {
            method: "GET",
            headers: getHeaders()

        });

        if (!response.ok) throw new Error("Error al obtener forms");

        return await response.json();
    } catch (error) {
        console.error("Error al consultar forms:", error);
        return [];
    }
}

export async function fetchAllModules() {
    try {
        const response = await fetch("https://localhost:7258/api/Module/GetAll/", {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener modules");

        return await response.json();
    } catch (error) {
        console.error("Error al consultar modules:", error);
        return [];
    }
}
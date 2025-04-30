import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/RolFormPermission/";

export async function fetchAllRolFormPermissions() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener rolFormPermissions");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar rolFormPermissions:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los rolFormPermissions.",
        });
        return [];
    }
}

export async function registrarRolFormPermission(rolFormPermissionData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(rolFormPermissionData)
        });

        if (!response.ok) throw new Error("Error al registrar rolFormPermission");

        return await response.json();
    } catch (error) {
        console.error("Error registrando rolFormPermission:", error);
        throw error;
    }
}

export async function getRolFormPermissionPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetByiId/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener rolFormPermission");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener rolFormPermission:", error);
        throw error;
    }
}

export async function actualizarRolFormPermission(rolFormPermissionData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(rolFormPermissionData)
        });

        if (!response.ok) throw new Error("Error al actualizar rolFormPermission");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar rolFormPermission:", error);
        throw error;
    }
}

export async function eliminarRolFormPermission(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar rolFormPermission");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando rolFormPermission:", error);
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

export async function fetchAllPermissions() {
    try {
        const response = await fetch("https://localhost:7258/api/Permission/GetAll/", {
            method: "GET",
            headers: getHeaders()

        });

        if (!response.ok) throw new Error("Error al obtener permissions");

        return await response.json();
    } catch (error) {
        console.error("Error al consultar permissions:", error);
        return [];
    }
}
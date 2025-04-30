import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/Permission/";

export async function fetchAllPermissions() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener Permissions");

        return await response.json();
        // let respuesta = await response.json()
        console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar Permissions:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los Permissions.",
        });
        return [];
    }
}

export async function registrarPermission(permissionData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(permissionData)
        });

        if (!response.ok) throw new Error("Error al registrar permission");

        return await response.json();
    } catch (error) {
        console.error("Error registrando permission:", error);
        throw error;
    }
}

export async function getPermissionPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetByiId/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener permission");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener permission:", error);
        throw error;
    }
}

export async function actualizarPermission(permissionData) {
    try {
        const response = await fetch(`${API_BASE_URL}Updated/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(permissionData)
        });

        if (!response.ok) throw new Error("Error al actualizar permission");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar permission:", error);
        throw error;
    }
}

export async function eliminarPermission(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar permission");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando permission:", error);
        throw error;
    }
}
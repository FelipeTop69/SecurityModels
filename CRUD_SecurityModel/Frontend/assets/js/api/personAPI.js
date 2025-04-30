import { getHeaders } from "../../../utils/requestOptions.js";

const API_BASE_URL = "https://localhost:7258/api/Person/";

export async function fetchAllPersons() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener Persons");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al consultar Persons:", error);
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "No se pudieron obtener los Persons.",
        });
        return [];
    }
}

export async function registrarPerson(personData) {
    try {
        const response = await fetch(`${API_BASE_URL}Create/`, {
            method: "POST",
            headers: getHeaders(),
            body: JSON.stringify(personData)
        });

        if (!response.ok) throw new Error("Error al registrar person");

        return await response.json();
    } catch (error) {
        console.error("Error registrando person:", error);
        throw error;
    }
}

export async function getPersonPorId(id) {
    try {
        const response = await fetch(`${API_BASE_URL}GetByiId/${id}`, {
            method: "GET",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al obtener person");

        return await response.json();
        // let respuesta = await response.json()
        // console.log(respuesta)
    } catch (error) {
        console.error("Error al obtener person:", error);
        throw error;
    }
}

export async function actualizarPerson(personData) {
    try {
        const response = await fetch(`${API_BASE_URL}Update/`, {
            method: "PUT",
            headers: getHeaders(),
            body: JSON.stringify(personData)
        });

        if (!response.ok) throw new Error("Error al actualizar person");

        return await response.json();
    } catch (error) {
        console.error("Error al actualizar person:", error);
        throw error;
    }
}

export async function eliminarPerson(id, tipo) {
    try {
        const response = await fetch(`${API_BASE_URL}Delete/${id}/?strategy=${tipo}`, {
            method: "DELETE",
            headers: getHeaders()
        });

        if (!response.ok) throw new Error("Error al eliminar person");

        return await response.json();
    } catch (error) {
        console.error("Error eliminando person:", error);
        throw error;
    }
}

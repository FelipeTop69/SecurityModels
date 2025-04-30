const API_BASE_URL = "https://localhost:7258/api/RolFormPermission/";

async function fetchAllRolFormPermissions() {
    try {
        const response = await fetch(`${API_BASE_URL}GetAll/`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "X-DB-Provider": "sqlserver"
            }
        });
        if (!response.ok) throw new Error("Error al obtener rolFormPermissions");

        // return await response.json();
        let respuesta = await response.json()
        console.log(respuesta)
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

fetchAllRolFormPermissions()
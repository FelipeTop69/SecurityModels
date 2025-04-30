export function getHeaders() {
    const provider = localStorage.getItem("X-DB-Provider") || "sqlserver";
    const token = localStorage.getItem("auth_token");

    const headers = {
        "Content-Type": "application/json",
        "X-DB-Provider": provider
    };

    if (token) {
        headers["Authorization"] = `Bearer ${token}`;
    }

    return headers;
}

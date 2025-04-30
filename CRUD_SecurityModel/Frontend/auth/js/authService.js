const AUTH_KEY = "auth_token";
const ROLE_KEY = "user_role";
const USER_KEY = "username";

export async function login(username, password) {
    const response = await fetch("https://localhost:7258/api/Auth/Login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
    });

    if (!response.ok) throw new Error("Login failed");
    return await response.json();
}

export function iniciarSesionTemporal({ token, username, rol }) {
    const now = new Date();
    const expiration = new Date(now.getTime() + 2 * 60 * 1000); 

    localStorage.setItem(AUTH_KEY, token);
    localStorage.setItem(ROLE_KEY, rol);
    localStorage.setItem(USER_KEY, username);
    localStorage.setItem("token_exp", expiration.toISOString()); // <--- clave nueva
}

export function isTokenValido() {
    const exp = localStorage.getItem("token_exp");
    if (!exp) return false;

    const expDate = new Date(exp);
    const now = new Date();
    return now < expDate;
}

export function logout() {
    localStorage.clear();
    window.location.href = "../auth/login.html";
}

export function getToken() {
    if (!isTokenValido()) {
        logout(); 
        return null;
    }
    return localStorage.getItem("auth_token");
}

export function getRol() {
    return localStorage.getItem(ROLE_KEY);
}

export function getUsername() {
    return localStorage.getItem(USER_KEY);
}

export function extenderSesion() {
    const exp = localStorage.getItem("token_exp");
    if (!exp) return;

    const nuevaExp = new Date(Date.now() + 1 * 60 * 1000); 
    localStorage.setItem("token_exp", nuevaExp.toISOString());
}

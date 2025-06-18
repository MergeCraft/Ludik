import api from "../../../lib/axios";
import { loginSuccess, logout } from "./userSlice";

// auth.js
export const iniciarSesion = async (credenciales, dispatch) => {
  try {
    const response = await api.post("/api/Login/login", {
      NombreUsuario: credenciales.usuario,
      Contrasenia: credenciales.contrasena,
    });

    const data = response.data;

    dispatch(loginSuccess(data));
    sessionStorage.setItem("userData", JSON.stringify(data));

    return data;
  } catch (error) {
    // Captura errores del backend y lanza mensaje amigable
    const mensaje = error.response?.data?.message || error.response?.data?.error || "Credenciales inválidas o error al iniciar sesión.";

    throw new Error(mensaje); // Esto lo captura el onError de React Query
  }
};

export const registrarse = async (data, tipoUsuario) => {
  const endpoint = tipoUsuario === "profesor" ? "/api/profesor/alta" : "/api/estudiante/alta";

  try {
    const response = await api.post(endpoint, data);
    return response.data;
  } catch (error) {
    const raw = error?.response?.data;
    const mensaje = raw?.mensaje || raw?.message || raw?.error || (typeof raw === "string" ? raw : "") || error.message || "Error desconocido al registrar.";

    throw new Error(mensaje);
  }
};

export const cerrarSesion = (dispatch) => {
  sessionStorage.removeItem("userData");
  dispatch(logout());
};

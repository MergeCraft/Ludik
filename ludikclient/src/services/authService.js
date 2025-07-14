// src/services/auth.js
import api from "../lib/axios";
import { loginSuccess, logout } from "../features/auth/hooks/userSlice";
import { persistor } from "../app/store";

export const iniciarSesion = async (credenciales, dispatch) => {
  try {
    const response = await api.post("/api/Login/login", {
      NombreUsuario: credenciales.usuario,
      Contrasenia: credenciales.contrasena,
    });

    const data = response.data;
    dispatch(loginSuccess(data));

    return data;
  } catch (error) {
    const mensaje = error.response?.data?.message || error.response?.data?.error || "Credenciales inválidas o error al iniciar sesión.";
    throw new Error(mensaje);
  }
};

export const registrarse = async (data, tipoUsuario, dispatch) => {
  const endpoint = tipoUsuario === "profesor" ? "/api/profesor/alta" : "/api/estudiante/alta";

  try {
    const response = await api.post(endpoint, data);

    dispatch(loginSuccess(response.data));

    return response.data;
  } catch (error) {
    const raw = error?.response?.data;
    const mensaje = raw?.mensaje || raw?.message || raw?.error || (typeof raw === "string" ? raw : "") || error.message || "Error desconocido al registrar.";
    throw new Error(mensaje);
  }
};

export const cerrarSesion = (dispatch) => {
  dispatch(logout());
  persistor.purge();
};

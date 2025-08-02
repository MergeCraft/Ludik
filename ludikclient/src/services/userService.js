import api from "../lib/axios";
import { parseBackendErrors, handleApiError } from "../lib/apiUtils";

export const obtenerPerfilUsuario = async () => {
  try {
    const response = await api.get("/api/Usuario/me");
    const data = response.data;

    if (!data.esExitoso) {
      const mensajes = parseBackendErrors(data.errores, "Error al obtener datos del usuario.");
      throw mensajes;
    }

    return data.valor;
  } catch (error) {
    handleApiError(error, "Error al obtener datos del usuario.");
  }
};

import api from "../../lib/axios";

export const crearGrupo = async (grupo) => {
  try {
    const response = await api.post("/api/grupo/alta", grupo);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.message || error.response?.data?.error || "Error al crear el grupo.";
    throw new Error(mensaje);
  }
};

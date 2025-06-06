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

export const obtenerGruposProfesor = async () => {
  try {
    const response = await api.get("/api/profesor/mis-grupos");
    return response.data;
  } catch (error) {
    const mensaje =
      error.response?.data?.mensaje ||
      "No se pudieron obtener los grupos del profesor.";
    throw new Error(mensaje);
  }
};

import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;

  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }

  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const crearMedalla = async (medalla) => {
  try {
    const response = await api.post("/api/medalla/alta", medalla);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al crear la medalla.");
  }
};

export const obtenerMedallasProfesor = async () => {
  try {
    const response = await api.get("/api/Medalla");
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudieron obtener las medallas del profesor.");
  }
};

export const obtenerMedallaPorId = async (id) => {
  try {
    const response = await api.get(`/api/Medalla/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo obtener la medalla.");
  }
};

export const editarMedalla = async ({ id, ...data }) => {
  try {
    const response = await api.put(`/api/medalla/${id}`, data);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al editar la medalla.");
  }
};

export const eliminarMedalla = async (id) => {
  try {
    const response = await api.delete(`/api/Medalla/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al eliminar la medalla.");
  }
};

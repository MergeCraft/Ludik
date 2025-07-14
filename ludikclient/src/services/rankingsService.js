// services/rankingsService.js
import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;
  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }
  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const obtenerRankings = async () => {
  try {
    const response = await api.get("/api/TablaClasificacion");
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener los rankings.");
  }
};

export const crearRanking = async (grupoId, data) => {
  try {
    const response = await api.post("/api/TablaClasificacion", data, {
      params: { grupoId },
      headers: { "Content-Type": "application/json" },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al crear el ranking.");
  }
};

export const eliminarRanking = async (id) => {
  try {
    await api.delete(`/api/TablaClasificacion/${id}`);
  } catch (error) {
    throw parseError(error, "Error al eliminar el ranking.");
  }
};

export const obtenerRankingPorId = async (id) => {
  try {
    const response = await api.get(`/api/TablaClasificacion/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener el detalle del ranking.");
  }
};

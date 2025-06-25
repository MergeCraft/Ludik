// services/storeService.js
import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;

  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }

  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const obtenerRecompensasTienda = async (tiendaId) => {
  try {
    const response = await api.get(`/api/Tienda/${tiendaId}/recompensas`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener las recompensas de la tienda.");
  }
};

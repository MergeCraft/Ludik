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

export const crearRecompensa = async ({ nombre, rutaImagenCompleta, rutaImagenMiniatura, precio }) => {
  try {
    const response = await api.post("/api/Recompensa/alta", {
      nombre,
      rutaImagenCompleta,
      rutaImagenMiniatura,
      precio,
    });
    return response.data;
  } catch (error) {
    console.log(error);
    throw parseError(error, "Error al crear la recompensa.");
  }
};

export const obtenerRecompensasTienda = async (tiendaId) => {
  try {
    const response = await api.get(`/api/Tienda/${tiendaId}/recompensas`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener las recompensas de la tienda.");
  }
};

export const canjearRecompensa = async ({ perfilId, recompensaId }) => {
  if (!perfilId || !recompensaId) {
    throw ["ID de perfil o recompensa inválido."];
  }

  try {
    const response = await api.post(`/api/Estudiante/perfiles/${perfilId}/recompensas/${recompensaId}/canjear`);
    if (!response || !response.data) {
      throw ["Respuesta vacía al intentar canjear la recompensa."];
    }

    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo canjear la recompensa.");
  }
};

export const obtenerRecompensasProfesor = async () => {
  try {
    const response = await api.get("/api/Recompensa/obtener-recompensas-profesor");
    console.log(response.data);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudieron obtener las recompensas del profesor.");
  }
};

export const editarRecompensa = async ({ recompensaId, data }) => {
  try {
    const response = await api.put(`/api/Recompensa/${recompensaId}`, data);
    return response.data;
  } catch (error) {
    console.log(error);
    throw parseError(error, "Error al editar la recompensa.");
  }
};

export const eliminarRecompensa = async (recompensaId) => {
  try {
    const response = await api.delete(`/api/Recompensa/${recompensaId}`);
    return response.data;
  } catch (error) {
    console.log(error);
    throw parseError(error, "Error al eliminar la recompensa.");
  }
};

export const asignarRecompensaAGrupos = async ({ recompensaId, gruposIds }) => {
  try {
    const response = await api.post("/api/Recompensa/asignar-a-grupos", {
      recompensaId,
      gruposIds,
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al asignar la recompensa a los grupos.");
  }
};

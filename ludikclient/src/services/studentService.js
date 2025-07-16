//services/studentService.js
import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;
  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }
  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const obtenerPerfilGrupo = async (grupoId) => {
  try {
    const response = await api.get(`/api/PerfilEstudiante/mi-perfil/grupo/${grupoId}/medallas`);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo obtener el perfil del grupo.");
  }
};

export const obtenerRecompensasPerfil = async (perfilId) => {
  try {
    const response = await api.get(`/api/Estudiante/perfiles/${perfilId}/recompensas`);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo obtener el inventario de recompensas.");
  }
};

export const obtenerBarraProgresoPerfil = async (perfilId) => {
  try {
    const response = await api.get(`/api/BarraProgreso/${perfilId}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo obtener la barra de progreso del estudiante.");
  }
};

export const definirMetaCalificacion = async ({ perfilEstudianteId, metaCalificacion }) => {
  try {
    const response = await api.post(`/api/PerfilEstudiante/definir-meta-califiacion`, {
      perfilEstudianteId,
      metaCalificacion,
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo establecer la meta de calificación.");
  }
};

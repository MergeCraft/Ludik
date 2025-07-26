// services/groupService.js

import api from "../lib/axios";

// ─────────────────────────────────────────────
// 🧰 UTILIDAD PARA MANEJO DE ERRORES
// ─────────────────────────────────────────────

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;

  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }

  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

// ─────────────────────────────────────────────
// 🧩 GRUPOS
// ─────────────────────────────────────────────

export const crearGrupo = async (grupo) => {
  try {
    const response = await api.post("/api/grupo/alta", grupo);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al crear el grupo.");
  }
};

export const editarGrupo = async (grupo) => {
  try {
    const response = await api.put(`/api/Grupo/editar/${grupo.id}`, grupo);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al editar el grupo.");
  }
};

export const eliminarGrupo = async (id) => {
  try {
    const response = await api.delete(`/api/Grupo/eliminar/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al eliminar el grupo.");
  }
};

export const reiniciarLogrosGrupo = async (grupoId) => {
  try {
    const response = await api.post(`/api/Profesor/reiniciar-logros`, null, {
      params: { grupoId },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al reiniciar los logros del grupo.");
  }
};

export const obtenerGrupos = async (rol) => {
  try {
    const endpoint = rol === "Profesor" ? "/api/profesor/mis-grupos" : "/api/estudiante/mis-grupos";

    const response = await api.get(endpoint);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudieron obtener los grupos.");
  }
};

export const obtenerGrupo = async (id) => {
  try {
    const response = await api.get(`/api/Grupo/info/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener el grupo.");
  }
};

export const obtenerAlumnosGrupo = async (id) => {
  try {
    const response = await api.get(`/api/Grupo/${id}/perfiles`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener los alumnos.");
  }
};

// ─────────────────────────────────────────────
// 📩 SOLICITUDES DE UNIÓN
// ─────────────────────────────────────────────

export const obtenerSolicitudesUnion = async (id) => {
  try {
    const response = await api.get("/api/Profesor/solicitudes-union", {
      params: { grupoId: id },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener las solicitudes de unión.");
  }
};

export const solicitarUnirseGrupo = async (codigo) => {
  try {
    const response = await api.post("/api/Estudiante/unirse-grupo", null, {
      params: { codigo },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al solicitar unirte al grupo.");
  }
};

export const aceptarSolicitud = async (solicitudId) => {
  try {
    const response = await api.post("/api/Profesor/aceptar-solicitud", null, {
      params: { solicitudId },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al aceptar la solicitud.");
  }
};

export const rechazarSolicitud = async (solicitudId) => {
  try {
    const response = await api.post("/api/Profesor/rechazar-solicitud", null, {
      params: { solicitudId },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al rechazar la solicitud.");
  }
};

// ─────────────────────────────────────────────
// 🏅 MEDALLAS
// ─────────────────────────────────────────────

export const asignarMedalla = async ({ perfilId, medallaId }) => {
  try {
    const response = await api.post(`/api/AsignacionMedallas/perfil-estudiante/${perfilId}/medalla/${medallaId}`);
    return response.data;
  } catch (error) {
    const data = error?.response?.data;
    const errores = Array.isArray(data) ? data.map((e) => e.mensaje || e.message || e.error).filter(Boolean) : [data?.mensaje || data?.message || data?.error || "Error al asignar medalla"];

    throw errores;
  }
};

export const eliminarMedalla = async ({ perfilId, medallaId }) => {
  try {
    const response = await api.delete(`/api/AsignacionMedallas/perfil-estudiante/${perfilId}/medalla/${medallaId}`);
    return response.data;
  } catch (error) {
    const data = error?.response?.data;
    const errores = Array.isArray(data) ? data.map((e) => e.mensaje || e.message || e.error).filter(Boolean) : [data?.mensaje || data?.message || data?.error || "Error al eliminar la medalla"];

    throw errores;
  }
};

// ─────────────────────────────────────────────
// 🧱 PAC (Proyectos de Aula Colaborativos)
// ─────────────────────────────────────────────

export const crearPac = async ({ grupoId, pacData }) => {
  try {
    const response = await api.post("/api/Profesor/pac", pacData, {
      params: { grupoId },
    });
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al crear el proyecto de aula.");
  }
};

export const obtenerPacsGrupo = async (grupoId) => {
  try {
    const response = await api.get("/api/Profesor/pac", {
      params: { grupoId },
    });
    return response.data; // Array de PACs
  } catch (error) {
    throw parseError(error, "Error al obtener los proyectos colaborativos.");
  }
};

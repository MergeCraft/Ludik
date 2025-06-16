// services/groupService.js
import api from "../lib/axios";

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
    const mensaje = error.response?.data?.mensaje || "No se pudieron obtener los grupos del profesor.";
    throw new Error(mensaje);
  }
};

export const obtenerGrupo = async (id) => {
  try {
    const response = await api.get(`/api/Grupo/info/${id}`);

    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al obtener el grupo.";
    throw new Error(mensaje);
  }
};

export const obtenerAlumnosGrupo = async (id) => {
  try {
    const response = await api.get(`/api/Grupo/${id}/perfiles`);

    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al obtener los alumnos.";
    throw new Error(mensaje);
  }
};

export const obtenerSolicitudesUnion = async (id) => {
  try {
    // Envía el id del grupo como query parameter
    const response = await api.get("/api/Profesor/solicitudes-union", {
      params: { grupoId: id },
    });
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al obtener las solicitudes de unión.";
    throw new Error(mensaje);
  }
};

export const solicitarUnirseGrupo = async (codigo) => {
  try {
    const response = await api.post("/api/Estudiante/unirse-grupo", null, {
      params: { codigo },
    });
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || error.response?.data?.error || "Error al solicitar unirte al grupo.";
    throw new Error(mensaje);
  }
};

// services/groupService.js

export const aceptarSolicitud = async (solicitudId) => {
  try {
    const response = await api.post('/api/Profesor/aceptar-solicitud', null, {
      params: { solicitudId },
    });
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje ||
      "Error al aceptar la solicitud.";
    throw new Error(mensaje);
  }
};

export const rechazarSolicitud = async (solicitudId) => {
  try {
    const response = await api.post('/api/Profesor/rechazar-solicitud', null, {
      params: { solicitudId },
    });
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje ||
      "Error al rechazar la solicitud.";
    throw new Error(mensaje);
  }
};


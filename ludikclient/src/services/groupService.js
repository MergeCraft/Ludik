import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;

  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }

  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const crearGrupo = async (grupo) => {
  try {
    const response = await api.post("/api/grupo/alta", grupo);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al crear el grupo.");
  }
};

export const obtenerGruposProfesor = async () => {
  try {
    const response = await api.get("/api/profesor/mis-grupos");
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudieron obtener los grupos del profesor.");
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

//Asignacion de medallas

export const asignarMedalla = async ({ perfilId, medallaId }) => {
  try {
    const response = await api.post(
      `/api/AsignacionMedallas/perfil-estudiante/${perfilId}/medalla/${medallaId}`
    );
    return response.data;
  } catch (error) {
    const data = error?.response?.data;
    const errores = Array.isArray(data)
      ? data.map((e) => e.mensaje || e.message || e.error).filter(Boolean)
      : [data?.mensaje || data?.message || data?.error || "Error al asignar medalla"];
    throw errores;
  }
};
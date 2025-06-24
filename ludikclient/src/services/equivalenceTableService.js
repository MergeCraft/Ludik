import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;

  if (Array.isArray(data)) {
    // Lista de errores con campos 'mensaje'
    const mensajes = data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
    return mensajes.length ? mensajes : [defaultMsg];
  }

  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const crearTablaEquivalencia = async (equivalencia) => {
  try {
    const response = await api.post("/api/TablaEquivalencia", equivalencia);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al crear la tabla de equivalencia.");
  }
};

export const obtenerTablaEquivalencia = async (id) => {
  try {
    const response = await api.get(`/api/TablaEquivalencia/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "No se pudo obtener la tabla de equivalencia.");
  }
};

export const eliminarTablaEquivalencia = async (id) => {
  try {
    const response = await api.delete(`/api/TablaEquivalencia/${id}`);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al eliminar la tabla de equivalencia.");
  }
};

export const actualizarTablaEquivalencia = async (id, data) => {
  try {
    const response = await api.put(`/api/TablaEquivalencia/${id}`, data);
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al actualizar la tabla de equivalencia.");
  }
};

export const obtenerTablasEquivalencia = async () => {
  try {
    const response = await api.get("/api/TablaEquivalencia");
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener las tablas de equivalencia.");
  }
};

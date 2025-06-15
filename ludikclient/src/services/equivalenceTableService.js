// services/equivalenceTableService.js
import api from "../lib/axios";

export const crearTablaEquivalencia = async (equivalencia) => {
  try {
    const response = await api.post("/api/TablaEquivalencia", equivalencia);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.message || error.response?.data?.error || "Error al crear la tabla de equivalencia.";
    throw new Error(mensaje);
  }
};

export const obtenerTablaEquivalencia = async (id) => {
  try {
    const response = await api.get(`/api/TablaEquivalencia/${id}`);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "No se pudo obtener la tabla de equivalencia.";
    throw new Error(mensaje);
  }
};

export const eliminarTablaEquivalencia = async (id) => {
  try {
    const response = await api.delete(`/api/TablaEquivalencia/${id}`);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al eliminar la tabla de equivalencia.";
    throw new Error(mensaje);
  }
};

export const actualizarTablaEquivalencia = async (id, data) => {
  try {
    const response = await api.put(`/api/TablaEquivalencia/${id}`, data);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al actualizar la tabla de equivalencia.";
    throw new Error(mensaje);
  }
};

export const obtenerTablasEquivalencia = async () => {
  try {
    const response = await api.get("/api/TablaEquivalencia");
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al obtener las tablas de equivalencia.";
    throw new Error(mensaje);
  }
};

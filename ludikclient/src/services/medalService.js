// services/medals/medalService.js
import api from "../lib/axios";

export const crearMedalla = async (medalla) => {
  try {
    const response = await api.post("/api/medalla/alta", medalla);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.message || error.response?.data?.error || "Error al crear la medalla.";
    throw new Error(mensaje);
  }
};

export const obtenerMedallasProfesor = async () => {
  try {
    const response = await api.get("/api/Medalla");
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "No se pudieron obtener las medallas del profesor.";
    throw new Error(mensaje);
  }
};

export const obtenerMedallaPorId = async (id) => {
  try {
    const response = await api.get(`/api/Medalla/${id}`);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "No se pudo obtener la medalla.";
    throw new Error(mensaje);
  }
};

export const editarMedalla = async ({ id, ...data }) => {
  try {
    const response = await api.put(`/api/medalla/${id}`, data);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al editar la medalla.";
    throw new Error(mensaje);
  }
};

export const eliminarMedalla = async (id) => {
  try {
    const response = await api.delete(`/api/Medalla/${id}`);
    return response.data;
  } catch (error) {
    const mensaje = error.response?.data?.mensaje || "Error al eliminar la medalla.";
    throw new Error(mensaje);
  }
};

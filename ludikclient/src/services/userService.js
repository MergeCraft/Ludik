import api from "../lib/axios";

const parseError = (error, defaultMsg) => {
  const data = error?.response?.data;
  if (Array.isArray(data)) {
    return data.map((e) => e.mensaje || e.message || e.error).filter(Boolean);
  }
  const mensaje = data?.mensaje || data?.message || data?.error;
  return [mensaje || defaultMsg];
};

export const obtenerPerfilUsuario = async () => {
  try {
    const response = await api.get("/api/Usuario/me");
    return response.data;
  } catch (error) {
    throw parseError(error, "Error al obtener datos del usuario.");
  }
};

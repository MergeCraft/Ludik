// src/lib/apiUtils.js
import * as Toast from "./toastify";

/**
 * Parsea los errores que vienen en el formato estándar del backend.
 *
 * @param {Array} errores Array de objetos error { codigo, mensaje }
 * @param {string} defaultMsg Mensaje por defecto si no hay errores
 * @returns {string[]} Array con mensajes de error
 */
export const parseBackendErrors = (errores, defaultMsg = "Ocurrió un error.") => {
  if (!Array.isArray(errores) || errores.length === 0) {
    return [defaultMsg];
  }
  return errores.map((e) => e.mensaje || e.codigo || defaultMsg);
};

/**
 * Función genérica para manejar errores en peticiones API,
 * que puede lanzar el array de mensajes o construir uno genérico.
 *
 * @param {any} error Objeto error capturado en try/catch
 * @param {string} defaultMsg Mensaje por defecto para error inesperado
 * @throws {string[]} Array de mensajes de error
 */
export const handleApiError = (error, defaultMsg) => {
  if (Array.isArray(error)) {
    throw error; // Ya es array de mensajes
  }

  // Si viene el formato estándar { errores: [...] }
  const erroresBackend = error?.response?.data?.errores;

  if (erroresBackend) {
    const mensajes = parseBackendErrors(erroresBackend, defaultMsg);
    throw mensajes;
  }

  // Si no, intentamos obtener un mensaje genérico o el mensaje del error
  const mensaje = error?.response?.data?.mensaje || error?.message || defaultMsg;
  throw [mensaje];
};

/**
 * Función para manejar errores en hooks React Query,
 * recibe el error (array o string) y muestra toasts para cada mensaje.
 *
 * @param {any} error Error recibido en onError de React Query
 * @param {function} notificar Función para mostrar notificación (toast)
 */
export const manejarVisualizacionDeErrores = (error) => {
  const mensajes = Array.isArray(error) ? error : [error?.message || "Error inesperado"];
  mensajes.forEach((msg) => Toast.notificarError(msg));
};

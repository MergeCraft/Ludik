import { toast } from "react-toastify";

// Estilo común para todos los toasts
const estiloToastComun = {
  boxShadow: "0 3px 0 3px var(--blanco-secundario)",
  borderRadius: "10px",
  width: "100%",
  maxWidth: window.innerWidth < 768 ? "80%" : "600px",
  marginTop: "10px",
  whiteSpace: "nowrap",
  overflow: "hidden",
  textOverflow: "ellipsis",
};

// Detectar si es móvil o escritorio
const obtenerPosicionToast = () => {
  return window.innerWidth < 768 ? "top-center" : "top-right";
};

// Funciones reutilizables
export const notificarExito = (msg) =>
  toast.success(msg, {
    position: obtenerPosicionToast(),
    style: estiloToastComun,
  });

export const notificarError = (msg) =>
  toast.error(msg, {
    position: obtenerPosicionToast(),
    autoClose: 2500,
    style: estiloToastComun,
  });

export const notificarInfo = (msg) =>
  toast.info(msg, {
    position: obtenerPosicionToast(),
    style: estiloToastComun,
  });

export const notificarWarning = (msg) =>
  toast.warning(msg, {
    position: obtenerPosicionToast(),
    style: estiloToastComun,
  });

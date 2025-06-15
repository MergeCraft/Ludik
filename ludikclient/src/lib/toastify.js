import { toast } from "react-toastify";

export const notificarExito = (msg) =>
  toast.success(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "100%", // <- que use el 100% del contenedor
      maxWidth: "600px", // opcional, así no se vuelve extremadamente grande
      marginTop: "10px",
      whiteSpace: "nowrap", // opcional si deseas que aparezcan en una línea
      overflow: "hidden",
      textOverflow: "ellipsis",
    },
  });

export const notificarError = (msg) =>
  toast.error(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "100%",
      maxWidth: "600px",
      marginTop: "10px",
      whiteSpace: "nowrap",
      overflow: "hidden",
      textOverflow: "ellipsis",
    },
  });

export const notificarInfo = (msg) =>
  toast.info(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "100%",
      maxWidth: "600px",
      marginTop: "10px",
      whiteSpace: "nowrap",
      overflow: "hidden",
      textOverflow: "ellipsis",
    },
  });

export const notificarWarning = (msg) =>
  toast.warning(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "100%",
      maxWidth: "600px",
      marginTop: "10px",
      whiteSpace: "nowrap",
      overflow: "hidden",
      textOverflow: "ellipsis",
    },
  });

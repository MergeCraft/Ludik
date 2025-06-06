import { toast } from "react-toastify";

export const notificarExito = (msg) =>
  toast.success(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "90%",
      marginTop: "10px",
    },
  });

export const notificarError = (msg) =>
  toast.error(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "90%",
      marginTop: "10px",
    },
  });

export const notificarInfo = (msg) =>
  toast.info(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "90%",
      marginTop: "10px",
    },
  });

export const notificarWarning = (msg) =>
  toast.warning(msg, {
    position: "top-center",
    style: {
      boxShadow: "0 3px 0 3px var(--blanco-secundario)",
      borderRadius: "10px",
      width: "90%",
      marginTop: "10px",
    },
  });

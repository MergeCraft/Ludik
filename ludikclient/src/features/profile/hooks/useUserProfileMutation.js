// hooks/useUser.js
import { useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";
import { obtenerPerfilUsuario } from "../../../services/userService";

const manejarErrores = (error) => {
  const mensajes = Array.isArray(error) ? error : [error.message];
  mensajes.forEach((msg) => Toast.notificarError(msg));
};

export const usePerfilUsuario = () => {
  return useQuery({
    queryKey: ["perfilUsuario"],
    queryFn: obtenerPerfilUsuario,
    onError: manejarErrores,
  });
};

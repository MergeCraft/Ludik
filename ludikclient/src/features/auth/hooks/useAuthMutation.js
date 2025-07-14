import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { iniciarSesion, registrarse } from "../../../services/authService";
import { useDispatch } from "react-redux";
import * as Toast from "../../../lib/toastify";

export const useLogin = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (credenciales) => iniciarSesion(credenciales, dispatch),
    onSuccess: (user) => {
      navigate("/groups");
      Toast.notificarExito(`Bienvenido ${user.nombreUsuario}`);
    },
    onError: (error) => {
      Toast.notificarError(error.message || "Error al iniciar sesión");
    },
  });
};

export const useRegistro = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  return useMutation({
    mutationFn: ({ data, tipoUsuario }) => registrarse(data, tipoUsuario, dispatch),
    onSuccess: (user) => {
      navigate("/groups");
      Toast.notificarExito("Registro exitoso");
      Toast.notificarExito(`Bienvenido ${user.nombreUsuario}`);
    },
    onError: (error) => {
      Toast.notificarError(error.message || "Error al registrar");
    },
  });
};

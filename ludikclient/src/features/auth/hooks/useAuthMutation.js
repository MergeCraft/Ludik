import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { iniciarSesion, registrarse } from "./auth";
import { useDispatch } from "react-redux";
import * as Toast from "../../../lib/toastify";

export const useLogin = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  return useMutation({
    mutationFn: (credenciales) => iniciarSesion(credenciales, dispatch),
    onSuccess: (user) => {
      navigate("/groups");
      console.log(user);
      Toast.notificarExito(`Bienvenido ${user.nombreUsuario}`);
    },
    onError: (error) => {
      Toast.notificarError(error.message || "Error al iniciar sesión");
    },
  });
};

export const useRegistro = () => {
  const navigate = useNavigate();

  return useMutation({
    mutationFn: ({ data, tipoUsuario }) => registrarse(data, tipoUsuario),
    onSuccess: () => {
      Toast.notificarExito("Registro exitoso");
      navigate("/login");
    },
    onError: (error) => {
      Toast.notificarError(error.message || "Error al registrar");
    },
  });
};

// src/features/reward/hooks/useRewardMutation.js
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { obtenerRecompensasProfesor, crearRecompensa, editarRecompensa, eliminarRecompensa, asignarRecompensaAGrupos } from "../../../services/storeService";
import * as Toast from "../../../lib/toastify.js";

const manejarErrores = (error) => {
  const mensajes = Array.isArray(error) ? error : [error.message];
  mensajes.forEach((msg) => Toast.notificarError(msg));
};

export const useRecompensasProfesor = () => {
  return useQuery({
    queryKey: ["recompensasProfesor"],
    queryFn: obtenerRecompensasProfesor,
    enabled: true,
    onError: manejarErrores,
  });
};

export const useCrearRecompensa = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: crearRecompensa,
    onSuccess: (data) => {
      Toast.notificarExito("Recompensa creada correctamente.");
      queryClient.invalidateQueries(["recompensas"]); // Ajusta la key según cómo cargues recompensas
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEditarRecompensa = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ recompensaId, data }) => editarRecompensa({ recompensaId, data }),
    onSuccess: (data) => {
      Toast.notificarExito("Recompensa editada correctamente.");
      queryClient.invalidateQueries(["recompensasProfesor"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEliminarRecompensa = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (recompensaId) => eliminarRecompensa(recompensaId),
    onSuccess: () => {
      Toast.notificarExito("Recompensa eliminada correctamente.");
      queryClient.invalidateQueries(["recompensasProfesor"]);
      if (onSuccessCallback) onSuccessCallback();
    },
    onError: manejarErrores,
  });
};

export const useAsignarRecompensaAGrupos = (onSuccess) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: asignarRecompensaAGrupos,
    onSuccess: (data) => {
      Toast.notificarExito("Recompensa asignada correctamente.");
      queryClient.invalidateQueries(["recompensas"]); // invalidar cache recompensas si es necesario
      if (onSuccess) onSuccess(data);
    },
    onError: manejarErrores,
  });
};

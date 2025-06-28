import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify.js";
import { crearMedalla, obtenerMedallasProfesor, obtenerMedallaPorId, editarMedalla, eliminarMedalla } from "../../../services/medalService.js";

const handleErrores = (error) => {
  const mensajes = Array.isArray(error) ? error : [error.message];
  mensajes.forEach((msg) => Toast.notificarError(msg));
};

export const useCrearMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla creada exitosamente");
      queryClient.invalidateQueries(["medallas"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: handleErrores,
  });
};

export const useMedallasProfesor = (isProfesor) => {
  return useQuery({
    queryKey: ["medallas", "profesor"],
    queryFn: obtenerMedallasProfesor,
    enabled: isProfesor,
    onError: handleErrores,
  });
};

export const useObtenerMedallaPorId = (id) => {
  return useQuery({
    queryKey: ["medalla", id],
    queryFn: () => obtenerMedallaPorId(id),
    enabled: id !== null && id !== undefined,
    onError: handleErrores,
  });
};

export const useEditarMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: editarMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla actualizada correctamente");
      queryClient.invalidateQueries(["medallas"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: handleErrores,
  });
};

export const useEliminarMedalla = (options) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: eliminarMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla eliminada correctamente.");
      queryClient.invalidateQueries(["medallas"]);
      if (options?.onSuccess) options.onSuccess(data);
    },
    onError: (error) => {
      handleErrores(error);
      if (options?.onError) options.onError(error);
    },
  });
};

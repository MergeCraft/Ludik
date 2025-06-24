import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";
import { crearTablaEquivalencia, obtenerTablaEquivalencia, eliminarTablaEquivalencia, actualizarTablaEquivalencia, obtenerTablasEquivalencia } from "../../../services/equivalenceTableService";

const handleErrores = (error) => {
  const mensajes = Array.isArray(error) ? error : [error.message];
  mensajes.forEach((msg) => Toast.notificarError(msg));
};

export const useCrearTablaEquivalencia = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearTablaEquivalencia,
    onSuccess: (data) => {
      Toast.notificarExito("Tabla de equivalencia creada.");
      queryClient.invalidateQueries(["tablasEquivalencia"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: handleErrores,
  });
};

export const useEditarTablaEquivalencia = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, ...payload }) => actualizarTablaEquivalencia(id, payload),
    onSuccess: (data) => {
      Toast.notificarExito("Tabla de equivalencia actualizada.");
      queryClient.invalidateQueries(["tablasEquivalencia"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: handleErrores,
  });
};

export const useEliminarTablaEquivalencia = (options) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: eliminarTablaEquivalencia,
    onSuccess: (data) => {
      Toast.notificarExito("Tabla de equivalencia eliminada.");
      queryClient.invalidateQueries(["tablasEquivalencia"]);
      if (options?.onSuccess) options.onSuccess(data);
    },
    onError: (error) => {
      handleErrores(error);
      if (options?.onError) options.onError(error);
    },
  });
};

export const useObtenerTablaEquivalencia = (id) => {
  return useQuery({
    queryKey: ["tablaEquivalencia", id],
    queryFn: () => obtenerTablaEquivalencia(id),
    enabled: !!id,
    onError: handleErrores,
  });
};

export const useTablasEquivalencia = () => {
  return useQuery({
    queryKey: ["tablasEquivalencia"],
    queryFn: obtenerTablasEquivalencia,
    onError: handleErrores,
  });
};

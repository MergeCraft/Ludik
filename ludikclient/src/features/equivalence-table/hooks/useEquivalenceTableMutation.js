// hooks/useEquivalenceTableMutation.js
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";

import * as Toast from "../../../lib/toastify";

import { crearTablaEquivalencia, obtenerTablaEquivalencia, eliminarTablaEquivalencia, actualizarTablaEquivalencia, obtenerTablasEquivalencia } from "../../../services/equivalenceTableService.js";

export const useCrearTablaEquivalencia = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearTablaEquivalencia,
    onSuccess: (data) => {
      Toast.notificarExito("Tabla de equivalencia creada.");
      queryClient.invalidateQueries(["tablasEquivalencia"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      Toast.notificarError(error.message);
    },
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
    onError: (error) => {
      Toast.notificarError(error.message);
    },
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
      Toast.notificarError(error.message);
      if (options?.onError) options.onError(error);
    },
  });
};

export const useObtenerTablaEquivalencia = (id) => {
  return useQuery({
    queryKey: ["tablaEquivalencia", id],
    queryFn: () => obtenerTablaEquivalencia(id),
    enabled: !!id,
    onError: (error) => {
      Toast.notificarError(error.message);
    },
  });
};

export const useTablasEquivalencia = () => {
  return useQuery({
    queryKey: ["tablasEquivalencia"],
    queryFn: obtenerTablasEquivalencia,
    onError: (error) => {
      Toast.notificarError(error.message);
    },
  });
};

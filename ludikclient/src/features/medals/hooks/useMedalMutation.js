// hooks/useMedalMutation.js
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify.js";
import { crearMedalla, obtenerMedallasProfesor, obtenerMedallaPorId, editarMedalla, eliminarMedalla } from "../../../services/medals/medalService.js";

export const useCrearMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla creada exitosamente");
      queryClient.invalidateQueries(["medallas"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      const raw = error?.response?.data;
      const msg = raw?.mensaje || raw?.message || raw?.error || JSON.stringify(raw) || error.message || "No se pudo crear la medalla.";
      Toast.notificarError(msg);
    },
  });
};

export const useMedallasProfesor = () => {
  return useQuery({
    queryKey: ["medallas", "profesor"],
    queryFn: obtenerMedallasProfesor,
    onError: (error) => {
      Toast.notificarError(error.message || "Error al cargar medallas");
    },
  });
};

export const useObtenerMedallaPorId = (id) => {
  return useQuery({
    queryKey: ["medalla", id],
    queryFn: () => obtenerMedallaPorId(id), // ✅ usa el service
    enabled: id !== null && id !== undefined,
    onError: (error) => {
      const msg = error?.message || "Error al obtener la medalla.";
      Toast.notificarError(msg);
    },
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
    onError: (error) => {
      const msg = error?.message || "No se pudo actualizar la medalla.";
      Toast.notificarError(msg);
    },
  });
};

export const useEliminarMedalla = (options) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: eliminarMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla eliminada correctamente.");
      queryClient.invalidateQueries(["medallas"]); // Refresca la lista de medallas
      if (options?.onSuccess) options.onSuccess(data);
    },
    onError: (error) => {
      const msg = error?.message || "No se pudo eliminar la medalla.";
      Toast.notificarError(msg);
      if (options?.onError) options.onError(error);
    },
  });
};

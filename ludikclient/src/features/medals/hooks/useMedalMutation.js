// hooks/useMedalMutation.js
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify.js";
import { crearMedalla, obtenerMedallasProfesor } from "../../../services/medals/medalService.js";

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

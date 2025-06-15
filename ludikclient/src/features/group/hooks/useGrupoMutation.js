import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import { crearGrupo, obtenerGruposProfesor } from "../../../services/groupService.js";
import * as Toast from "../../../lib/toastify";

export const useCrearGrupo = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Grupo creado exitosamente");
      queryClient.invalidateQueries(["grupos"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      // Manejo de errores centralizado
      const raw = error?.response?.data;
      const msg = raw?.mensaje || raw?.message || raw?.error || JSON.stringify(raw) || error.message || "No se pudo crear el grupo";
      Toast.notificarError(msg);
    },
  });
};

export const useGruposProfesor = () => {
  return useQuery({
    queryKey: ["grupos", "profesor"],
    queryFn: obtenerGruposProfesor,
    onError: (error) => {
      Toast.notificarError(error.message || "Error al cargar grupos");
    },
  });
};

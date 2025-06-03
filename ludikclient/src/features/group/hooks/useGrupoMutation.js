import { useMutation, useQueryClient } from "@tanstack/react-query";
import { crearGrupo } from "../../../services/groups/groupService.js";
import * as Toast from "../../../lib/toastify";

export const useCrearGrupo = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Grupo creado exitosamente");
      queryClient.invalidateQueries(["grupos"]); // si estás listando grupos
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      const msg = error.response?.data?.message || "No se pudo crear el grupo";
      Toast.notificarError(msg);
    },
  });
};

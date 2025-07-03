// hooks/useStudentMutation.js
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";
import { obtenerPerfilGrupo, obtenerRecompensasPerfil } from "../../../services/studentService";
import { canjearRecompensa } from "../../../services/storeService";

const manejarErrores = (error) => {
  const errores = Array.isArray(error) ? error : [error.message];
  errores.forEach((msg) => Toast.notificarError(msg));
};

export const usePerfilGrupo = (grupoId, isProfesor) => {
  return useQuery({
    queryKey: ["perfilGrupo", grupoId],
    queryFn: () => obtenerPerfilGrupo(grupoId),
    enabled: !isProfesor,
    onError: manejarErrores,
  });
};

export const useRecompensasPerfil = (perfilId) => {
  return useQuery({
    queryKey: ["recompensasPerfil", perfilId],
    queryFn: () => obtenerRecompensasPerfil(perfilId),
    enabled: !!perfilId,
    onError: manejarErrores,
  });
};

export const useClaimReward = (perfilId, recompensaId, isProfesor) => {
  const queryClient = useQueryClient();

  if (isProfesor) return { mutate: () => {}, isLoading: false };

  return useMutation({
    mutationFn: () => canjearRecompensa({ perfilId, recompensaId }),
    onSuccess: () => {
      Toast.notificarExito("¡Recompensa canjeada exitosamente!");
      queryClient.invalidateQueries(["perfilGrupo", perfilId]);
    },
    onError: manejarErrores,
  });
};

// hooks/useStudentMutation.js
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";
import {
  obtenerPerfilGrupo,
  obtenerRecompensasPerfil,
  obtenerBarraProgresoPerfil,
  definirMetaCalificacion,
  obtenerInventarioAvatar,
  guardarAvatarPersonalizado,
  asignarKudo,
} from "../../../services/studentService";
import { canjearRecompensa } from "../../../services/storeService";
import { obtenerImagenPerfil } from "../../../services/imagesService";

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

export const useImagenPerfil = (perfilId) => {
  return useQuery({
    queryKey: ["imagenPerfil", perfilId],
    queryFn: () => obtenerImagenPerfil(perfilId),
    enabled: !!perfilId,
    onError: manejarErrores,
    retry: (failureCount, error) => {
      if (error?.response?.status === 404) return false;
      return failureCount < 1; // si querés limitar otros errores
    },
  });
};

export const useBarraProgresoPerfil = (perfilId) => {
  return useQuery({
    queryKey: ["barraProgresoPerfil", perfilId],
    queryFn: () => obtenerBarraProgresoPerfil(perfilId),
    enabled: !!perfilId,
    onError: manejarErrores,
  });
};

export const useDefinirMetaCalificacion = (perfilId) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (metaCalificacion) => definirMetaCalificacion({ perfilEstudianteId: perfilId, metaCalificacion }),
    onSuccess: () => {
      Toast.notificarExito("Meta de calificación actualizada.");
      queryClient.invalidateQueries(["perfilGrupo", perfilId]);
      queryClient.invalidateQueries(["barraProgresoPerfil", perfilId]);
    },
    onError: manejarErrores,
  });
};

export const useInventarioAvatar = (idPerfilEstudiante, enabled = true) => {
  return useQuery({
    queryKey: ["inventario-avatar", idPerfilEstudiante],
    queryFn: () => obtenerInventarioAvatar(idPerfilEstudiante),
    enabled: !!idPerfilEstudiante && enabled,
  });
};

export const useGuardarAvatar = (idPerfilEstudiante) => {
  return useMutation({
    mutationFn: ({ avatarDto, jpegBlob }) => guardarAvatarPersonalizado(idPerfilEstudiante, avatarDto, jpegBlob),
    onSuccess: () => {
      Toast.notificarExito("¡Avatar guardado correctamente!");
    },
    onError: manejarErrores,
  });
};

export const useAsignarKudo = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ idPerfilEstudianteRecibe, idPerfilEstudianteEmisor, kudo }) => asignarKudo({ idPerfilEstudianteRecibe, idPerfilEstudianteEmisor, kudo }),
    onSuccess: (_, { idPerfilEstudianteRecibe }) => {
      Toast.notificarExito("¡Kudo asignado exitosamente!");
      queryClient.invalidateQueries(["perfilGrupo", idPerfilEstudianteRecibe]);
    },
    onError: manejarErrores,
  });
};

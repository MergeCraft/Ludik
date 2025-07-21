import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";
import {
  crearGrupo,
  editarGrupo,
  eliminarGrupo,
  reiniciarLogrosGrupo,
  obtenerGrupos,
  obtenerGrupo,
  obtenerAlumnosGrupo,
  obtenerSolicitudesUnion,
  solicitarUnirseGrupo,
  aceptarSolicitud,
  rechazarSolicitud,
  asignarMedalla,
  eliminarMedalla,
} from "../../../services/groupService";

import { obtenerRecompensasTienda } from "../../../services/storeService";

import { obtenerRankings, crearRanking, eliminarRanking, obtenerRankingPorId } from "../../../services/rankingsService";

const manejarErrores = (error) => {
  const errores = Array.isArray(error) ? error : [error.message];
  errores.forEach((msg) => Toast.notificarError(msg));
};

export const useCrearGrupo = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Grupo creado exitosamente.");
      queryClient.invalidateQueries(["grupos"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEditarGrupo = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: editarGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Grupo editado correctamente.");
      queryClient.invalidateQueries(["grupo", data.id]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEliminarGrupo = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: eliminarGrupo,
    onSuccess: (_, id) => {
      Toast.notificarExito("Grupo eliminado correctamente.");
      queryClient.invalidateQueries(["grupos"]);
      if (onSuccessCallback) onSuccessCallback(id);
    },
    onError: manejarErrores,
  });
};

export const useReiniciarLogrosGrupo = (onSuccessCallback) => {
  return useMutation({
    mutationFn: reiniciarLogrosGrupo,
    onSuccess: () => {
      Toast.notificarExito("Logros reiniciados correctamente.");
      if (onSuccessCallback) onSuccessCallback();
    },
    onError: manejarErrores,
  });
};

export const useGruposPorRol = (rol) => {
  return useQuery({
    queryKey: ["grupos", rol],
    queryFn: () => obtenerGrupos(rol),
    enabled: !!rol, // solo si el rol está definido
    onError: manejarErrores,
  });
};

export const useGrupo = (id) => {
  return useQuery({
    queryKey: ["grupo", id],
    queryFn: () => obtenerGrupo(id),
    enabled: !!id,
    onError: manejarErrores,
  });
};

export const useAlumnosGrupo = (id) => {
  return useQuery({
    queryKey: ["alumnos", id],
    queryFn: () => obtenerAlumnosGrupo(id),
    enabled: !!id,
    onError: manejarErrores,
  });
};

export const useSolicitudesUnion = (id) => {
  return useQuery({
    queryKey: ["solicitudesUnion", id],
    queryFn: () => obtenerSolicitudesUnion(id),
    enabled: !!id,
    onError: manejarErrores,
  });
};

export const useSolicitarUnirseGrupo = (onSuccessCallback) => {
  return useMutation({
    mutationFn: solicitarUnirseGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Solicitud de unión creada.");
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useAceptarSolicitud = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: aceptarSolicitud,
    onSuccess: (data) => {
      Toast.notificarExito("Solicitud aceptada.");
      queryClient.invalidateQueries(["solicitudesUnion"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useRechazarSolicitud = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: rechazarSolicitud,
    onSuccess: (data) => {
      Toast.notificarExito("Solicitud rechazada.");
      queryClient.invalidateQueries(["solicitudesUnion"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

//Asignacion de medallas
export const useAsignarMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: asignarMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla asignada exitosamente.");
      queryClient.invalidateQueries(["alumnos"]); // podrías parametrizar por grupo si lo deseas
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEliminarMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: eliminarMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla eliminada exitosamente.");
      queryClient.invalidateQueries(["alumnos"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

//Tienda

export const useRecompensasTienda = (tiendaId) => {
  return useQuery({
    queryKey: ["recompensas", tiendaId],
    queryFn: () => obtenerRecompensasTienda(tiendaId),
    enabled: !!tiendaId,
    onError: manejarErrores,
  });
};

//Rankings

export const useRankings = () => {
  return useQuery({
    queryKey: ["rankings"],
    queryFn: obtenerRankings,
    onError: manejarErrores,
  });
};

export const useCrearRanking = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ grupoId, ranking }) => crearRanking(grupoId, ranking),
    onSuccess: (data) => {
      Toast.notificarExito("Ranking creado correctamente.");
      queryClient.invalidateQueries(["rankings"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEliminarRanking = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id) => eliminarRanking(id),
    onSuccess: () => {
      Toast.notificarExito("Ranking eliminado correctamente.");
      queryClient.invalidateQueries(["rankings"]);
    },
    onError: manejarErrores,
  });
};

export const useRankingPorId = (id) => {
  return useQuery({
    queryKey: ["ranking", id],
    queryFn: () => obtenerRankingPorId(id),
    enabled: !!id,
    onError: manejarErrores,
  });
};

//configs

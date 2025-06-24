import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";
import {
  crearGrupo,
  obtenerGruposProfesor,
  obtenerGrupo,
  obtenerAlumnosGrupo,
  obtenerSolicitudesUnion,
  solicitarUnirseGrupo,
  aceptarSolicitud,
  rechazarSolicitud,
  asignarMedalla,
} from "../../../services/groupService";

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

export const useGruposProfesor = () => {
  return useQuery({
    queryKey: ["grupos", "profesor"],
    queryFn: obtenerGruposProfesor,
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

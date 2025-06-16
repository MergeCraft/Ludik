import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import {
  crearGrupo,
  obtenerGruposProfesor,
  obtenerGrupo,
  obtenerAlumnosGrupo,
  obtenerSolicitudesUnion,
  solicitarUnirseGrupo,
  aceptarSolicitud,
  rechazarSolicitud,
} from "../../../services/groupService.js";
import * as Toast from "../../../lib/toastify";

export const useCrearGrupo = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: crearGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Grupo creado exitosamente.");
      queryClient.invalidateQueries(["grupos"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      // Manejo de errores centralizado
      const raw = error?.response?.data;
      const msg = raw?.mensaje || raw?.message || raw?.error || JSON.stringify(raw) || error.message || "No se pudo crear el grupo.";
      Toast.notificarError(msg);
    },
  });
};

export const useGruposProfesor = () => {
  return useQuery({
    queryKey: ["grupos", "profesor"],
    queryFn: obtenerGruposProfesor,
    onError: (error) => {
      Toast.notificarError(error.message || "Error al cargar grupos.");
    },
  });
};

export const useGrupo = (id) => {
  return useQuery({
    queryKey: ["grupo", id],
    queryFn: () => obtenerGrupo(id),
    onError: (error) => {
      Toast.notificarError(error.message);
    },
    enabled: !!id,
  });
};

export const useAlumnosGrupo = (id) => {
  return useQuery({
    queryKey: ["alumnos", id],
    queryFn: () => obtenerAlumnosGrupo(id),
    onError: (error) => {
      Toast.notificarError(error.message);
    },
    enabled: !!id,
  });
};

export const useSolicitudesUnion = (id) => {
  return useQuery({
    queryKey: ["solicitudesUnion", id],
    queryFn: () => obtenerSolicitudesUnion(id),
    onError: (error) => {
      Toast.notificarError(error.message);
    },
    enabled: !!id,
  });
};

export const useSolicitarUnirseGrupo = (onSuccessCallback) => {
  return useMutation({
    mutationFn: solicitarUnirseGrupo,
    onSuccess: (data) => {
      Toast.notificarExito("Solicitud de unión creada.");
      if (onSuccessCallback) onSuccessCallback(data);
      // opcional: invalidar grupos
      // queryClient.invalidateQueries(["grupos"]);
    },
    onError: (error) => {
      const msg = error?.message || "Error al solicitar unirte al grupo.";
      Toast.notificarError(msg);
    },
  });
};

export const useAceptarSolicitud = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: aceptarSolicitud,
    onSuccess: (data) => {
      Toast.notificarExito("Solicitud aceptada.");
      queryClient.invalidateQueries(["solicitudesUnion"]); // o según el groupId si lo deseas
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      Toast.notificarError(error.message);
    },
  });
};

export const useRechazarSolicitud = (onSuccessCallback) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: rechazarSolicitud,
    onSuccess: (data) => {
      Toast.notificarExito("Solicitud rechazada.");
      queryClient.invalidateQueries(["solicitudesUnion"]); // o según el groupId si lo deseas
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: (error) => {
      Toast.notificarError(error.message);
    },
  });
};

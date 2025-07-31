// hooks/useGrupo.js
import { useMutation, useQueryClient, useQuery } from "@tanstack/react-query";
import * as Toast from "../../../lib/toastify";

// === Servicios ===
import {
  crearGrupo,
  editarGrupo,
  eliminarGrupo,
  reiniciarLogrosGrupo,
  obtenerGrupos,
  obtenerGrupo,
  obtenerAlumnosGrupo,
  obtenerSolicitudesUnion,
  eliminarMedalla,
  asignarMedalla,
  solicitarUnirseGrupo,
  aceptarSolicitud,
  rechazarSolicitud,
  crearPac,
  obtenerPacsGrupo,
  obtenerUmbralesMedallas,
  crearUmbralMedalla,
  editarUmbralMedalla,
  eliminarUmbralMedalla
} from "../../../services/groupService";
import { obtenerTiposKudo } from "../../../services/medalService";
import { obtenerRecompensasTienda, crearRecompensa } from "../../../services/storeService";
import { obtenerRankings, crearRanking, eliminarRanking, obtenerRankingPorId } from "../../../services/rankingsService";

// === Utilidades ===
const manejarErrores = (error) => {
  const errores = Array.isArray(error) ? error : [error.message];
  errores.forEach((msg) => Toast.notificarError(msg));
};

// ─────────────────────────────────────────────
// 🧩 GRUPOS
// ─────────────────────────────────────────────

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
    enabled: !!rol,
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

// ─────────────────────────────────────────────
// 📩 SOLICITUDES DE UNIÓN
// ─────────────────────────────────────────────

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

// ─────────────────────────────────────────────
// 🏅 MEDALLAS
// ─────────────────────────────────────────────

export const useAsignarMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: asignarMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Medalla asignada exitosamente.");
      queryClient.invalidateQueries(["alumnos"]);
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

// ─────────────────────────────────────────────
// 🛍️ TIENDA
// ─────────────────────────────────────────────

export const useRecompensasTienda = (tiendaId) => {
  return useQuery({
    queryKey: ["recompensas", tiendaId],
    queryFn: () => obtenerRecompensasTienda(tiendaId),
    enabled: !!tiendaId,
    onError: manejarErrores,
  });
};

export const useCrearRecompensa = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: crearRecompensa,
    onSuccess: (data) => {
      Toast.notificarExito("Recompensa creada correctamente.");
      queryClient.invalidateQueries(["recompensas"]); // Ajusta la key según cómo cargues recompensas
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

// ─────────────────────────────────────────────
// 📊 RANKINGS
// ─────────────────────────────────────────────

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

// ─────────────────────────────────────────────
// 🧱 PAC (Proyectos de Aula Colaborativos)
// ─────────────────────────────────────────────

export const useCrearPac = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: crearPac,
    onSuccess: (data) => {
      Toast.notificarExito("PAC creado correctamente.");
      queryClient.invalidateQueries(["pacs"]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const usePacsGrupo = (grupoId) => {
  return useQuery({
    queryKey: ["pacs", grupoId],
    queryFn: () => obtenerPacsGrupo(grupoId),
    enabled: !!grupoId,
    onError: manejarErrores,
  });
};

// ─────────────────────────────────────────────
// 🧱 Umbrales para las medallas obtenias por kudos
// ─────────────────────────────────────────────

export const useUmbralesMedallas = (grupoId) => {
  return useQuery({
    queryKey: ["umbralesMedallas", grupoId],
    queryFn: () => obtenerUmbralesMedallas(grupoId),
    enabled: !!grupoId,
    onError: manejarErrores,
  });
};

export const useCrearUmbralMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: crearUmbralMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Umbral creado correctamente.");
      queryClient.invalidateQueries(["umbralesMedallas", data.grupoId]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEditarUmbralMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: editarUmbralMedalla,
    onSuccess: (data) => {
      Toast.notificarExito("Umbral editado correctamente.");
      queryClient.invalidateQueries(["umbralesMedallas", data.grupoId]);
      if (onSuccessCallback) onSuccessCallback(data);
    },
    onError: manejarErrores,
  });
};

export const useEliminarUmbralMedalla = (onSuccessCallback) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: eliminarUmbralMedalla,
    onSuccess: (data, id) => {
      Toast.notificarExito("Umbral eliminado correctamente.");
      queryClient.invalidateQueries(["umbralesMedallas"]); // invalida cache
      if (onSuccessCallback) onSuccessCallback(id);
    },
    onError: manejarErrores,
  });
};

// ─────────────────────────────────────────────
// 🧱 Kudos
// ─────────────────────────────────────────────

export const useTiposKudo = () => {
  return useQuery({
    queryKey: ["tiposKudo"],
    queryFn: obtenerTiposKudo,
    enabled: true,
    onError: manejarErrores,
  });
};

import React from "react";
import PropTypes from "prop-types";
import styles from "./MedalRequests.module.css";
import { useSolicitudesMedallas } from "../../hooks/useGrupoMutation";
import BarLoader from "../../../generics/BarLoader";
import MedalRequestItem from "./MedalRequestItem";

const MOCK_SOLICITUDES = [
  {
    id: 1,
    perfilEstudianteId: 101,
    medallaId: 5,
    descripcion: "Buen trabajo en el proyecto",
    fecha: "2025-09-01T07:55:31.420Z",
    estado: "Pendiente",
  },
  {
    id: 2,
    perfilEstudianteId: 102,
    medallaId: 3,
    descripcion: "Excelente participación en clase",
    fecha: "2025-09-01T08:10:00.000Z",
    estado: "Aprobada",
  },
];

const MedalRequests = ({ grupoId, medallas }) => {
  const { data: solicitudes, isLoading } = useSolicitudesMedallas(grupoId);

  const displaySolicitudes = solicitudes && solicitudes.length > 0 ? solicitudes : MOCK_SOLICITUDES;

  return (
    <div className={styles.medalRequests}>
      {isLoading ? (
        <BarLoader />
      ) : !displaySolicitudes || displaySolicitudes.length === 0 ? (
        <p>No hay solicitudes de medallas</p>
      ) : (
        displaySolicitudes.map((solicitud) => <MedalRequestItem key={solicitud.id} solicitud={solicitud} medallas={medallas} />)
      )}
    </div>
  );
};

MedalRequests.propTypes = {
  grupoId: PropTypes.number.isRequired,
  medallas: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      nombre: PropTypes.string.isRequired,
      nombreIcono: PropTypes.string.isRequired,
      descripcion: PropTypes.string.isRequired,
      cantidadMedallasBrinda: PropTypes.number.isRequired,
    })
  ).isRequired,
};

export default MedalRequests;

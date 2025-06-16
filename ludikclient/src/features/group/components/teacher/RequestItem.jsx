import React from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import styles from "./ApplicationRequests.module.css";

import { useAceptarSolicitud, useRechazarSolicitud } from "../../hooks/useGrupoMutation";

const RequestItem = ({ solicitud }) => {
  const { mutate: aceptar, isLoading: isAccepting } = useAceptarSolicitud();
  const { mutate: rechazar, isLoading: isRejecting } = useRechazarSolicitud();

  return (
    <div className={styles.request}>
      <span title={solicitud.idEstudiante}>{solicitud.idEstudiante}</span>
      <button disabled={isAccepting} aria-label="Aceptar solicitud" className="button-secondary" onClick={() => aceptar(solicitud.id)}>
        <FontAwesomeIcon icon="fa-solid fa-user-check" size="xl" />
      </button>
      <button disabled={isRejecting} aria-label="Rechazar solicitud" className="button-tertiary" onClick={() => rechazar(solicitud.id)}>
        <FontAwesomeIcon icon="fa-solid fa-user-xmark" size="xl" />
      </button>
    </div>
  );
};

RequestItem.propTypes = {
  solicitud: PropTypes.shape({
    id: PropTypes.number.isRequired,
    idEstudiante: PropTypes.string.isRequired,
    nombre: PropTypes.string.isRequired,
    fecha: PropTypes.shape({
      year: PropTypes.number.isRequired,
      month: PropTypes.number.isRequired,
      day: PropTypes.number.isRequired,
    }).isRequired,
  }).isRequired,
};

export default RequestItem;

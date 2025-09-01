import React from "react";
import PropTypes from "prop-types";
import styles from "./RequestView.module.css";
import ApplicationRequests from "./ApplicationRequests";
import MedalRequests from "./MedalRequests";

const RequestView = ({ grupo, medallas }) => {
  return (
    <div className={styles.requestView}>
      <div>
        <h3>Solicitudes de unión</h3>
        <ApplicationRequests grupo={grupo?.id} link={grupo?.urlCompleta} />
      </div>
      <div>
        <h3>Solicitudes de medalla</h3>
        <MedalRequests grupoId={grupo?.id} medallas={medallas} />
      </div>
    </div>
  );
};

RequestView.propTypes = {
  grupo: PropTypes.shape({
    id: PropTypes.oneOfType([PropTypes.number, PropTypes.string]).isRequired,
    urlCompleta: PropTypes.string.isRequired,
  }),
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

export default RequestView;

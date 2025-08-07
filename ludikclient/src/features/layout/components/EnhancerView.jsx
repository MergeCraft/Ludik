import React from "react";
import PropTypes from "prop-types";
import styles from "./EnhancerView.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { PulseLoader } from "../../generics/BarLoader";

const EnhancerView = ({ enhancerX, enhancerTime, isLoading }) => {
  return (
    <div
      className={styles.enhancerContainer}
      title={`Potenciador de monedas. Actualmente tienes un potenciador que multiplica tus monedas obtenidas por ${enhancerX} y dispones de ${enhancerTime} minutos para aprovecharlo`}
    >
      {isLoading ? (
        <PulseLoader />
      ) : (
        <div className={styles.enhancerContent}>
          <span>
            <FontAwesomeIcon icon="fa-solid fa-angles-up" bounce /> x{enhancerX}
          </span>
          <span>
            <FontAwesomeIcon icon="fa-solid fa-stopwatch" shake /> {enhancerTime}
          </span>
        </div>
      )}
    </div>
  );
};

EnhancerView.propTypes = {
  enhancerX: PropTypes.number.isRequired, // Por ejemplo: 1.5, 2, etc.
  enhancerTime: PropTypes.string.isRequired, // Por ejemplo: "3 min", "1h 20m"
  isLoading: PropTypes.bool.isRequired,
};

export default EnhancerView;

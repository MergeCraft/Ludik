import React from "react";
import PropTypes from "prop-types";
import styles from "./PacItem.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const estadoLabel = (estado) => {
  return ["Pendiente", "En curso", "Finalizado"][estado] || "Desconocido";
};

const PacItem = ({ pac }) => {
  const totalNiveles = pac.cantidadMedallasNecesarias;

  // Determina cuál es el último tramo alcanzado
  const ultimoAlcanzadoIndex = Math.min(pac.totalContribuciones, pac.cantidadMedallasNecesarias) - 1;

  return (
    <div className={styles.card}>
      <h3 className={styles.title}>
        <strong>Recompensa</strong> {pac.nombre}
      </h3>

      <div className={styles.progressBarContainer}>
        <p>Progreso</p>
        <div className={styles.progressBar}>
          {Array.from({ length: totalNiveles }).map((_, index) => {
            const alcanzado = pac.totalContribuciones > index;
            const esUltimoAlcanzado = index === ultimoAlcanzadoIndex;

            return (
              <label key={`nivel-${index}`} className={`${styles.label} ${alcanzado ? styles.alcanzado : styles.noAlcanzado}`}>
                {esUltimoAlcanzado && (
                  <span className={`${styles.circle}`}>
                    <FontAwesomeIcon icon="fa-solid fa-star" className={styles.icono} />
                  </span>
                )}
              </label>
            );
          })}
        </div>
      </div>

      <div className={styles.infoProgressBar}>
        <p>
          <strong>Contribuciones</strong> {pac.totalContribuciones}
        </p>
        <p>
          <strong>Medallas Necesarias</strong> {pac.cantidadMedallasNecesarias}
        </p>
      </div>
    </div>
  );
};

PacItem.propTypes = {
  pac: PropTypes.shape({
    id: PropTypes.number.isRequired,
    grupoId: PropTypes.number,
    nombre: PropTypes.string.isRequired,
    visual: PropTypes.number,
    cantidadMedallasNecesarias: PropTypes.number,
    totalContribuciones: PropTypes.number,
    recompensaClaseId: PropTypes.number,
    estado: PropTypes.number,
  }).isRequired,
};

export default PacItem;

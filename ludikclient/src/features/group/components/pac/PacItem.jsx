import React from "react";
import PropTypes from "prop-types";
import styles from "./PacItem.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const estadoLabel = (estado) => {
  return ["Pendiente", "En curso", "Finalizado"][estado] || "Desconocido";
};

const PacItem = ({ pac }) => {
  const totalNiveles = pac.visual + 1;
  const medallasPorNivel = pac.cantidadMedallasNecesarias / totalNiveles;

  pac.totalContribuciones = 11;

  return (
    <div className={styles.card}>
      <h3>Recompensa: {pac.nombre}</h3>
      <div className={styles.progressBarContainer}>
        <p>Progreso</p>
        <div className={styles.progressBar}>
          {Array.from({ length: totalNiveles }).map((_, index) => {
            const umbral = medallasPorNivel * (index + 1);
            const alcanzado = pac.totalContribuciones >= umbral;

            return (
              <label key={`nivel-${index}`} className={`${styles.label} ${alcanzado ? styles.alcanzado : styles.noAlcanzado}`}>
                <span className={`${styles.circle} ${alcanzado ? styles.circleAlcanzado : styles.circleNoAlcanzado}`}>
                  {alcanzado ? <FontAwesomeIcon icon="fa-solid fa-star" className={styles.icono} /> : Math.round(umbral)}
                </span>
              </label>
            );
          })}
        </div>
      </div>
      <p>
        <strong>Medallas necesarias:</strong> {pac.cantidadMedallasNecesarias}
      </p>
      <p>
        <strong>Total contribuciones:</strong> {pac.totalContribuciones}
      </p>
      <p>
        <strong>Estado:</strong> {estadoLabel(pac.estado)}
      </p>
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

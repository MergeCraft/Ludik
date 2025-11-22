// PacItem.jsx
import React, { useState } from "react";
import PropTypes from "prop-types";
import styles from "./PacItem.module.css";

const PacItem = ({ pac }) => {
  const [showInfo, setShowInfo] = useState(false);

  const totalNiveles = pac.cantidadMedallasNecesarias || 0;
  const totalContribuciones = pac.totalContribuciones || 0;

  // Calcular porcentaje total de progreso (0–100)
  const porcentaje =
    totalNiveles > 0 ? Math.min((totalContribuciones / totalNiveles) * 100, 100) : 0;

  const hitos = [25, 50, 75, 100];

  // Calcula cuánto se llena cada segmento
  const getSegmentFill = (percent, index) => {
    const start = index === 0 ? 0 : hitos[index - 1];
    const end = hitos[index];

    if (percent <= start) return 0;
    if (percent >= end) return 100;

    return ((percent - start) / (end - start)) * 100;
  };

  return (
    <div className={styles.card}>
      <h3 className={styles.title}>
        <strong>Recompensa</strong> {pac.nombre}
      </h3>

      <div className={styles.progressBarContainer}>
        <p className={styles.progressTitle}>Progreso del desafío</p>

        <div className={styles.progressBarWrapper}>
          <div className={styles.progressSegments}>
            {hitos.map((hito, index) => {
              const fill = getSegmentFill(porcentaje, index);
              const isCompleted = fill >= 100;
              const isActive = fill > 0 && fill < 100;

              return (
                <div
                  key={hito}
                  className={`
                    ${styles.segment}
                    ${isCompleted ? styles.segmentCompleted : ""}
                    ${isActive ? styles.segmentActive : ""}
                  `}
                >
                  <div
                    className={styles.segmentFill}
                    style={{ width: `${fill}%` }}
                  />
                  <span className={styles.segmentLabel}>{hito}%</span>
                </div>
              );
            })}
          </div>
        </div>
      </div>

      <div className={styles.infoProgressBar}>
        <p>
          <strong>
            Contribuciones
            <span
              className={styles.infoIcon}
              role="button"
              aria-label="¿Qué es una contribución?"
              onClick={(e) => {
                e.stopPropagation();
                setShowInfo((v) => !v);
              }}
            >
              ?
              {showInfo && (
                <span className={styles.tooltip}>
                  Cada vez que un estudiante aporta medallas a este desafío, suma 1
                  contribución.
                </span>
              )}
            </span>
          </strong>
          {totalContribuciones}
        </p>
        <p>
          <strong>Medallas necesarias</strong> {totalNiveles}
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

import React from "react";
import PropTypes from "prop-types";
import styles from "./PacItem.module.css";

const visualizarNivel = (nivel) => {
  return ["Básico", "Medio", "Avanzado"][nivel] || "Desconocido";
};

const estadoLabel = (estado) => {
  return ["Pendiente", "En curso", "Finalizado"][estado] || "Desconocido";
};

const PacItem = ({ pac }) => {
  return (
    <div className={styles.card}>
      <h3 className={styles.title}>{pac.nombre}</h3>
      <p><strong>Nivel visual:</strong> {visualizarNivel(pac.visual)}</p>
      <p><strong>Medallas necesarias:</strong> {pac.cantidadMedallasNecesarias}</p>
      <p><strong>Total contribuciones:</strong> {pac.totalContribuciones}</p>
      <p><strong>ID recompensa:</strong> {pac.recompensaClaseId}</p>
      <p><strong>Estado:</strong> {estadoLabel(pac.estado)}</p>
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

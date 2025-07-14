// features/group/components/rankings/RankingItem.jsx
import React from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./RankingItem.module.css";
import { useEliminarRanking } from "../../hooks/useGrupoMutation";

const RankingItem = ({ tabla, showTeacherOptions, onView }) => {
  const eliminarRanking = useEliminarRanking();
  const handleDelete = (e) => {
    e.stopPropagation();
    const confirmacion = window.confirm(`¿Estás seguro de que deseas eliminar el ranking "${tabla.nombre}"?`);
    if (confirmacion) {
      eliminarRanking.mutate(tabla.id);
    }
  };

  return (
    <div className={styles.rankingCard} onClick={() => onView(tabla.id)}>
      <h3>{tabla.nombre}</h3>
      <ul className={styles.participantes}>
        {tabla.participantes.map((p, i) => (
          <li key={p.perfilEstudianteId} className={styles.participante}>
            <span className={styles.posicion}>#{i + 1}</span>
            <span className={styles.nombre}>{p.nombreEstudiante}</span>
            <span className={styles.medallas}>{p.cantidadMedallas} medallas</span>
          </li>
        ))}
      </ul>
      {showTeacherOptions && (
        <div className={styles.accionesRanking} onClick={(e) => e.stopPropagation()}>
          <button className="button-tertiary" onClick={handleDelete}>
            <FontAwesomeIcon icon="fa-solid fa-trash" size="xl" />
          </button>
        </div>
      )}
    </div>
  );
};

RankingItem.propTypes = {
  tabla: PropTypes.shape({
    id: PropTypes.number.isRequired,
    nombre: PropTypes.string.isRequired,
    participantes: PropTypes.arrayOf(
      PropTypes.shape({
        perfilEstudianteId: PropTypes.number.isRequired,
        nombreEstudiante: PropTypes.string.isRequired,
        cantidadMedallas: PropTypes.number.isRequired,
      })
    ).isRequired,
  }).isRequired,
  showTeacherOptions: PropTypes.bool.isRequired,
  onView: PropTypes.func.isRequired,
};

export default RankingItem;

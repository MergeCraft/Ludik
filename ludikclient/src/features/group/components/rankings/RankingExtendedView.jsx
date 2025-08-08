// features/group/components/rankings/RankingExtendedView.jsx
import React from "react";
import PropTypes from "prop-types";
import { useRankingPorId } from "../../hooks/useGrupoMutation";
import BarLoader from "../../../generics/BarLoader";
import styles from "./RankingExtendedView.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const RankingExtendedView = ({ id }) => {
  const { data: ranking, isLoading } = useRankingPorId(id);

  if (isLoading) return <BarLoader />;
  if (!ranking) return <p>No se encontró el ranking.</p>;

  return (
    <div className={styles.extendedView}>
      <h2>{ranking.nombre}</h2>
      <p>
        <FontAwesomeIcon icon="fa-solid fa-medal" /> Medalla asociada: <strong>{ranking.medallaAsociadaNombre}</strong>
      </p>

      <ul className={styles.participantes}>
        {ranking.participantes.map((p, i) => (
          <li key={p.perfilEstudianteId}>
            <span className={styles.posicion}>#{i + 1}</span>
            <span>{p.nombreEstudiante}</span>
            <span>{p.cantidadMedallas} medallas</span>
          </li>
        ))}
      </ul>
    </div>
  );
};

RankingExtendedView.propTypes = {
  id: PropTypes.number.isRequired,
};

export default RankingExtendedView;

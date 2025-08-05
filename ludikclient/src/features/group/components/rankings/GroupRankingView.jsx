import React from "react";
import PropTypes from "prop-types";
import { useRankings } from "../../hooks/useGrupoMutation";
import BarLoader from "../../../generics/BarLoader";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./GroupRankingView.module.css";
import RankingCreateForm from "./RankingCreateForm";
import RankingItem from "./RankingItem.jsx";
import RankingExtendedView from "./RankingExtendedView";

const GroupRankingView = ({ setModalContent, setModalTitle, setShowModal, groupId, showTeacherOptions }) => {
  const { data: rankings, isLoading } = useRankings(groupId);

  const abrirModalCrearRanking = () => {
    setModalTitle("Crear nuevo Ranking");
    setModalContent(<RankingCreateForm groupId={groupId} onClose={() => setShowModal(false)} />);
    setShowModal(true);
  };

  const abrirDetalleRanking = (rankingId) => {
    setModalTitle("Detalle del Ranking");
    setModalContent(<RankingExtendedView id={rankingId} />);
    setShowModal(true);
  };

  if (isLoading) return <BarLoader />;

  return (
    <div className={styles.container}>
      {showTeacherOptions && (
        <button className={styles.agregarRanking} onClick={abrirModalCrearRanking}>
          <FontAwesomeIcon icon="fa-solid fa-plus" size="xl" />
          Crear nueva tabla de clasificacion
        </button>
      )}

      {!rankings || rankings.length === 0 ? (
        <p className={styles.sinRankings}>No hay rankings disponibles.</p>
      ) : (
        rankings.map((tabla) => <RankingItem key={tabla.id} tabla={tabla} showTeacherOptions={showTeacherOptions} onView={abrirDetalleRanking} />)
      )}
    </div>
  );
};
GroupRankingView.propTypes = {
  groupId: PropTypes.number.isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
  setShowModal: PropTypes.func.isRequired,
  showTeacherOptions: PropTypes.bool.isRequired,
};

export default GroupRankingView;

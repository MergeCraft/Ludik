import React from "react";
import styles from "./GroupPacView.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import PropTypes from "prop-types";

import CrearPacForm from "./CrearPacForm";
import PacItem from "./PacItem";
import BarLoader from "../../../generics/BarLoader";

// Hook para obtener PACs del grupo (debes implementarlo según tu backend y hooks)
import { usePacsGrupo } from "../../hooks/useGrupoMutation";

const GroupPacView = ({ recompensas, setModalContent, setModalTitle, setShowModal, groupId, showTeacherOptions }) => {
  const { data: pacs, isLoading, isError } = usePacsGrupo(groupId);

  console.log(recompensas);

  const handleOpenPacCreateForm = () => {
    setModalContent(<CrearPacForm groupId={groupId} recompensas={recompensas} onClose={() => setShowModal(false)} />);
    setModalTitle("Crear nuevo Proyecto Colaborativo");
    setShowModal(true);
  };

  return (
    <div className={styles.pacContainer}>
      <h4>Desafío grupal</h4>
      {showTeacherOptions && (
        <button className={styles.newPacButton} onClick={handleOpenPacCreateForm}>
          <FontAwesomeIcon icon="fa-solid fa-handshake" size="2xl" />
          Crear Nuevo Desafío
        </button>
      )}

      <section className={styles.pacsList}>
        {isLoading && <BarLoader />}
        {!isLoading && !isError && pacs?.length === 0 && <p>No hay desafíos para este grupo.</p>}
        {!isLoading && !isError && pacs?.map((pac) => <PacItem key={pac.id} pac={pac} />)}
      </section>
    </div>
  );
};

GroupPacView.propTypes = {
  recompensas: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      nombre: PropTypes.string.isRequired,
      precio: PropTypes.number.isRequired,
      tipo: PropTypes.string.isRequired,
      datos: PropTypes.shape({
        $type: PropTypes.string.isRequired,
        nombreIcono: PropTypes.string.isRequired,
      }).isRequired,
    })
  ).isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
  setShowModal: PropTypes.func.isRequired,
  groupId: PropTypes.number.isRequired,
  showTeacherOptions: PropTypes.bool,
};

export default GroupPacView;

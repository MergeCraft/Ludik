import React from "react";
import PropTypes from "prop-types";
import styles from "./ThresholdItem.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ThresholdCreateForm from "./ThresholdCreateForm";
import DefaultMedalImage1 from "../../../../assets/DefaultMedal.png";
import DefaultMedalImage2 from "../../../../assets/DefaultMedal2.png";
import DefaultMedalImage3 from "../../../../assets/DefaultMedal3.png";

const ThresholdItem = ({ threshold, setModalContent, setModalTitle, setShowModal, groupId, medallas, tiposKudo, showTeacherOptions }) => {
  const { cantidadKudos, medallaNombre, rutaIconoMedalla, tipoKudoNombre } = threshold;

  console.log(rutaIconoMedalla);

  const defaultMedalImages = [DefaultMedalImage1, DefaultMedalImage2, DefaultMedalImage3];

  const handleEditClick = () => {
    setModalTitle("Editar Umbral");
    setModalContent(<ThresholdCreateForm grupoId={groupId} medallas={medallas} tiposKudo={tiposKudo} initialValues={threshold} onClose={() => setShowModal(false)} />);
    setShowModal(true);
  };

  return (
    <div className={styles.thresholdCard}>
      <img src={defaultMedalImages[Math.floor(Math.random() * defaultMedalImages.length)]} alt={`Medalla: ${medallaNombre}`} className={styles.medalIcon} />
      <div className={styles.info}>
        <h4>{medallaNombre}</h4>
        <p className={styles.info}>
          <strong>{cantidadKudos}</strong> kudos de tipo <strong>{tipoKudoNombre}</strong>
        </p>
      </div>
      {showTeacherOptions && (
        <button className={styles.editButton} onClick={handleEditClick}>
          <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" />
        </button>
      )}
    </div>
  );
};

ThresholdItem.propTypes = {
  threshold: PropTypes.object.isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
  setShowModal: PropTypes.func.isRequired,
  groupId: PropTypes.number.isRequired,
  medallas: PropTypes.array.isRequired,
  tiposKudo: PropTypes.array.isRequired,
  showTeacherOptions: PropTypes.bool.isRequired,
};

export default ThresholdItem;

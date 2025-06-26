import React from "react";
import styles from "./RewardItem.module.css";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const RewardItem = ({ reward }) => {
  return (
    <div className={styles.rewardCard}>
      <h4>{reward.nombre}</h4>
      <div className={styles.iconContainer}>
        <FontAwesomeIcon icon={`fa-solid fa-${reward.rutaImagenCompleta}`} />
      </div>
      <p className={styles.monedas}>
        <FontAwesomeIcon icon="fa-solid fa-coins" />
        {reward.precio}
      </p>
      <button className={styles.editarRecompensa}>
        <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
      </button>
    </div>
  );
};

RewardItem.propTypes = {
  reward: PropTypes.shape({
    nombre: PropTypes.string.isRequired,
    rutaImagenCompleta: PropTypes.string.isRequired,
    rutaImagenMiniatura: PropTypes.string.isRequired,
    precio: PropTypes.number.isRequired,
  }).isRequired,
};

export default RewardItem;

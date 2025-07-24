import React from "react";
import styles from "./RewardItem.module.css";
import PropTypes from "prop-types";
import { useSelector } from "react-redux";
import { selectUserRole } from "../../auth/hooks/userSlice";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useClaimReward } from "../hooks/useStudentMutation";

const RewardItem = ({ reward, redeemed, perfilId }) => {
  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";

  const { mutate: claimReward, isLoading: isClaiming } = useClaimReward(perfilId, reward.id, isProfesor);

  const handleClaimReward = () => {
    if (isProfesor || !perfilId) return;

    claimReward(perfilId, reward.id);
  };

  return (
    <div className={styles.rewardCard}>
      <h4>{reward.nombre}</h4>
      <div className={styles.iconContainer}>
        {reward.requiereImagen ? <img src={`${reward.rutaImagenCompleta}`} alt={`Recompensa ${reward.nombre}`} /> : <FontAwesomeIcon icon={`fa-solid fa-${reward.rutaImagenCompleta}`} />}
      </div>

      {!redeemed && (
        <>
          <p className={styles.monedas}>
            <FontAwesomeIcon icon="fa-solid fa-coins" />
            {reward.precio}
          </p>
          {isProfesor ? (
            <button className={styles.editarRecompensa}>
              <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
            </button>
          ) : (
            <button className={styles.canjearRecompensa} onClick={handleClaimReward} disabled={isClaiming}>
              <FontAwesomeIcon icon="fa-solid fa-cart-shopping" />
            </button>
          )}
        </>
      )}
    </div>
  );
};

RewardItem.propTypes = {
  reward: PropTypes.shape({
    id: PropTypes.number.isRequired,
    nombre: PropTypes.string.isRequired,
    rutaImagenCompleta: PropTypes.string.isRequired,
    rutaImagenMiniatura: PropTypes.string.isRequired,
    precio: PropTypes.number.isRequired,
    requiereImagen: PropTypes.bool.isRequired, // nuevo campo
  }).isRequired,
  redeemed: PropTypes.bool.isRequired,
  perfilId: PropTypes.number.isRequired,
};

export default RewardItem;

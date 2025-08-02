import React from "react";
import styles from "./RewardItem.module.css";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useClaimReward } from "../../group/hooks/useStudentMutation";


const RewardItem = ({ reward, redeemed, perfilId, showProfesorOptions, storeView, onEdit }) => {
  const { mutate: claimReward, isLoading: isClaiming } = useClaimReward(perfilId, reward.id, showProfesorOptions);

  const handleClaimReward = () => {
    if (showProfesorOptions || !perfilId) return;
    claimReward(perfilId, reward.id);
  };

  const handleEditClick = () => {
    if (onEdit) onEdit(reward);
  };

  return (
    <div className={`${styles.rewardCard} ${storeView && styles.storeViewCard}`}>
      <h4>{reward.nombre}</h4>
      <div className={styles.iconContainer}>
        {reward.requiereImagen ? (
          <img src={`${reward.rutaImagenCompleta}`} alt={`Recompensa ${reward.nombre}`} />
        ) : (
          <FontAwesomeIcon icon={`fa-solid fa-${reward.rutaImagenCompleta ? reward.rutaImagenCompleta : "trophy"}`} />
        )}
      </div>

      {!redeemed && (
        <>
          <p className={styles.monedas}>
            <FontAwesomeIcon icon="fa-solid fa-coins" />
            {reward.precio}
          </p>
          {showProfesorOptions ? (
            <button className={`${styles.editarRecompensa} ${storeView && styles.storeViewButton}`} onClick={handleEditClick} type="button" aria-label={`Editar recompensa ${reward.nombre}`}>
              <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
            </button>
          ) : (
            <button className={styles.canjearRecompensa} onClick={handleClaimReward} disabled={isClaiming} type="button" aria-label={`Canjear recompensa ${reward.nombre}`}>
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
    requiereImagen: PropTypes.bool.isRequired,
  }).isRequired,
  redeemed: PropTypes.bool.isRequired,
  perfilId: PropTypes.number.isRequired,
  showProfesorOptions: PropTypes.bool.isRequired,
  storeView: PropTypes.bool.isRequired,
  onEdit: PropTypes.func,
};

RewardItem.defaultProps = {
  onEdit: null,
};

export default RewardItem;

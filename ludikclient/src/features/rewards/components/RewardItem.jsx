import React from "react";
import styles from "./RewardItem.module.css";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useClaimReward } from "../../group/hooks/useStudentMutation";

const RewardItem = ({ reward, redeemed, perfilId, showProfesorOptions, storeView, onEdit }) => {
  const { mutate: claimReward, isLoading: isClaiming } = useClaimReward(perfilId, reward.id, showProfesorOptions);

  const representacion = reward.representacion || reward.datos || {};
  const nombreIcono = representacion?.nombreIcono;
  const urlMiniatura = representacion?.urlMiniatura;

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
        {reward.tipo === "Imagen" ? <img src={`${urlMiniatura}`} alt={`Recompensa ${reward?.nombre}`} /> : <FontAwesomeIcon icon={`fa-solid fa-${nombreIcono ? nombreIcono : "trophy"}`} />}
      </div>

      {!redeemed && (
        <>
          <p className={styles.monedas}>
            <FontAwesomeIcon icon="fa-solid fa-coins" />
            {reward.precio}
          </p>
          {showProfesorOptions ? (
            !storeView && (
              <button className={`${styles.editarRecompensa} ${storeView && styles.storeViewButton}`} onClick={handleEditClick} type="button" aria-label={`Editar recompensa ${reward.nombre}`}>
                <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
              </button>
            )
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
    precio: PropTypes.number.isRequired,
    tipo: PropTypes.string.isRequired, // por ejemplo: "Icono"
    representacion: PropTypes.shape({
      nombreIcono: PropTypes.string, // puede variar según el tipo
    }).isRequired,
    datos: PropTypes.shape({
      urlMiniatura: PropTypes.string, // puede variar según el tipo
      urlCompleta: PropTypes.string, // puede variar según el tipo
    }).isRequired,
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

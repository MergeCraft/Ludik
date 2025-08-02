import React from "react";
import PropTypes from "prop-types";
import style from "./StoreGroupView.module.css";
import BarLoader from "../../../generics/BarLoader.jsx";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import RewardItem from "../../../rewards/components/RewardItem.jsx";
import RewardAsignationForm from "./RewardAsignationForm.jsx";

const StoreGroupView = ({ recompensas, isLoading, isProfesor, perfil, grupoId, setShowModal, setModalContent, setModalTitle }) => {
  const handleAsignNewReward = () => {
    setModalTitle("Asignar recompensa a grupos");
    setModalContent(
      <RewardAsignationForm
        grupoId={grupoId} // solo pasamos este id
        gruposProfesor={[]} // sin grupos, array vacío
        onClose={() => setShowModal(false)}
      />
    );
    setShowModal(true);
  };

  if (isLoading) return <BarLoader />;

  return (
    <div className={style.storeContent}>
      <div>
        {isProfesor && (
          <button className={style.addRewardButton} onClick={handleAsignNewReward}>
            <FontAwesomeIcon icon="plus" size="2xl" />
          </button>
        )}

        {recompensas?.length > 0 &&
          recompensas.map((reward) => <RewardItem key={reward.id + reward.nombre} reward={reward} redeemed={false} perfilId={perfil?.id} showProfesorOptions={isProfesor} storeView={true} />)}
      </div>
      <div className={style.mensaje}>{recompensas?.length == 0 && <p>No hay recompensas disponibles.</p>}</div>
    </div>
  );
};

StoreGroupView.propTypes = {
  recompensas: PropTypes.array,
  isLoading: PropTypes.bool,
  isProfesor: PropTypes.bool,
  perfil: PropTypes.object,
  grupoId: PropTypes.number, // <-- aquí la prop
  setShowModal: PropTypes.func.isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
};

export default StoreGroupView;

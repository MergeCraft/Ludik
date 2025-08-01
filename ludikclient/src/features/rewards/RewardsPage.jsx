// src/features/reward/RewardsPage.jsx
import React, { useState } from "react";
import styles from "./RewardsPage.module.css";
import { useSelector } from "react-redux";
import { selectUserRole } from "../auth/hooks/userSlice";
import { useRecompensasProfesor } from "./hooks/useRewardMutation";
import BaseManagerPage from "../generics/BaseManagerPage";
import RewardItem from "./components/RewardItem";
import RewardCreateForm from "./components/RewardCreateForm";
import BarLoader from "../generics/BarLoader";

const RewardsPage = () => {
  const [showModal, setShowModal] = useState(false);
  const [rewardToEdit, setRewardToEdit] = useState(null);
  const [search, setSearch] = useState("");

  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";

  const { data: recompensas, isLoading } = useRecompensasProfesor();

  const handleOpenCreate = () => {
    setRewardToEdit(null); // para crear, no hay recompensa a editar
    setShowModal(true);
  };

  const handleOpenEdit = (reward) => {
    // ahora recibe el objeto reward completo
    setRewardToEdit(reward);
    setShowModal(true);
  };


  const modalContent = <RewardCreateForm onClose={() => setShowModal(false)} reward={rewardToEdit} />;

  const actions = (
    <button className="button-secondary" onClick={handleOpenCreate}>
      Crear recompensa
    </button>
  );

  const filteredRewards = recompensas?.filter((r) => r.nombre.toLowerCase().includes(search.toLowerCase())) || [];

  const items = isLoading ? (
    <BarLoader />
  ) : (
    <div className={styles.rewardsGrid}>
      {filteredRewards.map((reward) => (
        <RewardItem
          key={reward.id}
          reward={reward}
          redeemed={false}
          perfilId={0}
          onEdit={() => handleOpenEdit(reward)} // paso el objeto reward completo aquí
          showProfesorOptions={isProfesor}
        />
      ))}
    </div>
  );

  return (
    <BaseManagerPage
      actions={actions}
      modalTitle={rewardToEdit ? "Editar recompensa" : "Crear nueva recompensa"}
      modalContent={modalContent}
      items={items}
      searchPlaceholder="recompensa"
      showModal={showModal}
      setShowModal={setShowModal}
      searchValue={search}
      onSearchChange={setSearch}
    />
  );
};

export default RewardsPage;

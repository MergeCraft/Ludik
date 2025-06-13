import React, { useState } from "react";
import styles from "./MedalManagerPage.module.css";
import genericStyles from "../generics/BaseManagerPage.module.css";
import MedalCard from "./components/MedalCard";
import MedalCreateForm from "./components/MedalCreateForm.jsx";
import { BarLoader } from "react-spinners";
import BaseManagerPage from "../generics/BaseManagerPage";
import PropTypes from "prop-types";
import { useMedallasProfesor } from "./hooks/useMedalMutation.js";

const MedalCreateModal = ({ onClose }) => <MedalCreateForm onClose={onClose} />;

MedalCreateModal.propTypes = {
  onClose: PropTypes.func.isRequired,
};

const MedalManagerPage = () => {
  const [showModal, setShowModal] = useState(false);
  const [search, setSearch] = useState("");

  const { data: medallas, isLoading } = useMedallasProfesor();

  const handleOpenModal = () => {
    setShowModal(true);
  };

  const modalContent = <MedalCreateModal onClose={() => setShowModal(false)} />;

  const actions = (
    <button className="button-secondary" onClick={handleOpenModal}>
      Crear medalla
    </button>
  );

  const filteredMedallas = medallas?.filter((m) => m.nombre.toLowerCase().includes(search.toLowerCase())) || [];

  const items = isLoading ? (
    <div className={genericStyles.barLoaderContainer}>
      <BarLoader color="var(--blanco-secundario)" size={10} />
    </div>
  ) : (
    <div className={styles.medalsContainer}>
      {filteredMedallas.map((medalla) => (
        <MedalCard
          key={medalla.id}
          nombre={medalla.nombre}
          descripcion={medalla.descripcion}
          urlImagen={medalla.urlImagen}
          cantidadMedallasBrinda={medalla.cantidadMedallasBrinda}
          esAsignacionMutua={medalla.esAsignacionMutua}
        />
      ))}
    </div>
  );

  return (
    <BaseManagerPage
      actions={actions}
      modalTitle="Crear nueva medalla"
      modalContent={modalContent}
      items={items}
      searchPlaceholder="medalla"
      showModal={showModal}
      setShowModal={setShowModal}
      searchValue={search}
      onSearchChange={setSearch}
    />
  );
};

export default MedalManagerPage;

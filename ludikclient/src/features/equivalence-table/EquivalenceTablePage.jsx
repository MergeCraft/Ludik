import React, { useState } from "react";
import styles from "./EquivalenceTablePage.module.css";

import { BarLoader } from "react-spinners";

import BaseManagerPage from "../generics/BaseManagerPage";

import EquivalenceTableItem from "./components/EquivalenceTableItem";

import EquivalenceTableCreateModal from "./components/EquivalenceTableCreateModal";

import { useTablasEquivalencia } from "./hooks/useEquivalenceTableMutation";

const EquivalenceTablePage = () => {
  const [showModal, setShowModal] = useState(false);
  const [modalTipo, setModalTipo] = useState(null);
  const [search, setSearch] = useState(""); // 🔍 estado de búsqueda

  // Carga de las tablas de equivalencia
  const { data: equivalences, isLoading } = useTablasEquivalencia();
  // Filtra según el nombre o la descripción
  const equivalencesFiltradas = (equivalences ?? []).filter((item) => (item.nombre ?? "").toLowerCase().includes(search.toLowerCase()));

  const handleOpenModal = (tipo) => {
    setModalTipo(tipo);
    setShowModal(true);
  };

  const modalContent = modalTipo === "crear" ? <EquivalenceTableCreateModal onClose={() => setShowModal(false)} /> : null;

  const actions = (
    <button className="button-secondary" onClick={() => handleOpenModal("crear")}>
      Crear equivalencia
    </button>
  );

  const items = isLoading ? (
    <div className={styles.loaderContainer}>
      <BarLoader color="var(--blanco-secundario)" size={10} />
    </div>
  ) : (
    <div className={styles.tablesContainer}>
      {equivalencesFiltradas.map((item) => (
        <EquivalenceTableItem key={item.id} item={item} />
      ))}
    </div>
  );

  return (
    <BaseManagerPage
      actions={actions}
      modalTitle={modalTipo === "crear" ? "Crear equivalencia" : "Editar equivalencia"}
      modalContent={modalContent}
      items={items}
      searchPlaceholder="equivalencia"
      showModal={showModal}
      setShowModal={setShowModal}
      searchValue={search}
      onSearchChange={setSearch}
    />
  );
};

export default EquivalenceTablePage;

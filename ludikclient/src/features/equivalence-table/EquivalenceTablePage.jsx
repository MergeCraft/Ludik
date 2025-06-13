import React, { useState } from "react";
import styles from "./EquivalenceTablePage.module.css";

import { BarLoader } from "react-spinners";

import BaseManagerPage from "../generics/BaseManagerPage";

// (Puedes crear un nuevo Item si así lo deseas)
import EquivalenceTableItem from "./components/EquivalenceTableItem";

// Mock de prueba — luego se va a sustituir por el fetch de la API
const mockEquivalences = [
  { id: 1, nombre: "Equivalencia 1", descripcion: "descripcion 1" },
  { id: 2, nombre: "Equivalencia 2", descripcion: "descripcion 2" },
  { id: 3, nombre: "Equivalencia 3", descripcion: "descripcion 3" },
  // Agregar más si deseas...
];

// (Puedes crear nuevos modales según tus necesidades)
import EquivalenceTableCreateModal from "./components/EquivalenceTableCreateModal";

const EquivalenceTablePage = () => {
  const [showModal, setShowModal] = useState(false);
  const [modalTipo, setModalTipo] = useState(null);
  const [search, setSearch] = useState(""); // 🔍 estado de búsqueda

  // Loading simulado
  const isLoading = false;

  // Filtra según el nombre o la descripción
  const equivalencesFiltradas = mockEquivalences.filter((item) => item.nombre.toLowerCase().includes(search.toLowerCase()) || item.descripcion.toLowerCase().includes(search.toLowerCase()));

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
    equivalencesFiltradas.map((item) => <EquivalenceTableItem key={item.id} item={item} />)
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

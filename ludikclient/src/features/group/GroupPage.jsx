import React, { useState } from "react";

import styles from "../generics/BaseManagerPage.module.css";

import selfStyle from "./GroupPage.module.css";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { BarLoader } from "react-spinners";

import BaseManagerPage from "../generics/BaseManagerPage";

import StudentItem from "./components/StudentItem";

import { useGrupo, useAlumnosGrupo } from "./hooks/useGrupoMutation";

import { useParams } from "react-router-dom";

import ApplicationRequests from "./components/teacher/ApplicationRequests";

const GroupPage = () => {
  // Obtenemos el id de la URL
  const { id } = useParams();
  const groupId = Number(id);

  const [search, setSearch] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState(null);
  const [modalTitle, setModalTitle] = useState("");

  // Carga de información del grupo
  const { data: group, isLoading: isLoadingGroup } = useGrupo(groupId);

  // Carga de estudiantes del grupo
  const { data: students, isLoading: isLoadingStudents } = useAlumnosGrupo(groupId);

  // Filtra según nombre o cualquier otro campo
  const studentsFiltrados = students?.filter(
    (item) => item.estudianteId.toLowerCase().includes(search.toLowerCase()) // o el nombre si estuviese incluido
  );

  const handleOpenApplicationRequests = () => {
    setModalContent(<ApplicationRequests groupId={groupId} link={group.urlCompleta} />);
    setModalTitle(`Solicitudes de unión del Grupo ${group.institucion.toUpperCase()} - ${group.nombre.toUpperCase()}`);
    setShowModal(true);
  };

  const actions = (
    <div className={selfStyle.acciones}>
      <button>
        <FontAwesomeIcon icon="fa-solid fa-gear" size="2xl" />
      </button>
      <button onClick={handleOpenApplicationRequests}>
        <FontAwesomeIcon icon="fa-solid fa-user-plus" size="2xl" />
      </button>
      <button>
        <FontAwesomeIcon icon="fa-solid fa-eye" size="2xl" />
      </button>
    </div>
  );

  const items = isLoadingGroup ? (
    <div className={styles.barLoaderContainer}>
      <BarLoader color="var(--blanco-secundario)" size={10} />
    </div>
  ) : (
    <div className={selfStyle.groupContainer}>
      <div className={selfStyle.infoGrupo}>
        <h2>
          <FontAwesomeIcon icon="fa-solid fa-book-bookmark" />
          {group.materia.toUpperCase()}
        </h2>
        <h2>
          <FontAwesomeIcon icon="fa-solid fa-school" />
          {group.institucion.toUpperCase()} - {group.nombre.toUpperCase()}
        </h2>
      </div>

      {isLoadingStudents ? (
        <div className={styles.barLoaderContainer}>
          <BarLoader color="var(--blanco-secundario)" size={10} />
        </div>
      ) : (
        <div className={selfStyle.studentsContainer}>{studentsFiltrados && studentsFiltrados.map((item) => <StudentItem key={item.id} student={item} />)}</div>
      )}
    </div>
  );

  return (
    <BaseManagerPage
      actions={actions}
      modalTitle={modalTitle}
      modalContent={modalContent}
      items={items}
      searchPlaceholder="alumno"
      showModal={showModal}
      setShowModal={setShowModal}
      searchValue={search}
      onSearchChange={setSearch}
    />
  );
};

export default GroupPage;

// GroupPage.jsx
import React, { useState } from "react";
import styles from "../generics/BaseManagerPage.module.css";
import selfStyle from "./GroupPage.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { BarLoader } from "react-spinners";
import BaseManagerPage from "../generics/BaseManagerPage";
import StudentItem from "./components/StudentItem";
import RewardItem from "./components/teacher/RewardItem.jsx";
import RewardCreateForm from "./components/teacher/RewardCreateForm.jsx";
import { useGrupo, useAlumnosGrupo, useRecompensasTienda } from "./hooks/useGrupoMutation";
import { useMedallasProfesor } from "../medals/hooks/useMedalMutation";
import { useParams } from "react-router-dom";
import ApplicationRequests from "./components/teacher/ApplicationRequests";

const GroupPage = () => {
  const { id } = useParams();
  const groupId = Number(id);

  const [search, setSearch] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState(null);
  const [modalTitle, setModalTitle] = useState("");
  const [showStore, setShowStore] = useState(false);

  const { data: group, isLoading: isLoadingGroup } = useGrupo(groupId);
  const { data: students, isLoading: isLoadingStudents } = useAlumnosGrupo(groupId);
  const { data: medals, isLoading: isLoadingMedals } = useMedallasProfesor();
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasTienda(group?.idTienda);

  const studentsFiltrados = students?.filter((item) => item.nombreEstudiante.toLowerCase().includes(search.toLowerCase()));

  const handleOpenApplicationRequests = () => {
    setModalContent(<ApplicationRequests groupId={groupId} link={group.urlCompleta} />);
    setModalTitle(`Solicitudes de unión del Grupo ${group.institucion.toUpperCase()} - ${group.nombre.toUpperCase()}`);
    setShowModal(true);
  };

  const handleOpenRewardCreateForm = () => {
    setModalContent(<RewardCreateForm groupId={groupId} />);
    setModalTitle("Crear nueva recompensa");
    setShowModal(true);
  };

  const handleToggleStore = () => {
    setShowStore((prev) => !prev);
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
      <button onClick={handleToggleStore}>
        <FontAwesomeIcon icon="fa-solid fa-store" size="2xl" />
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
        <h3>
          <FontAwesomeIcon icon="fa-solid fa-book-bookmark" />
          {group.materia.toUpperCase()}
        </h3>
        <h3>
          <FontAwesomeIcon icon="fa-solid fa-school" />
          {group.institucion.toUpperCase()} - {group.nombre.toUpperCase()}
        </h3>
      </div>

      <div className={selfStyle.studentsContainer}>
        {showStore ? (
          isLoadingRecompensas ? (
            <div className={styles.barLoaderContainer}>
              <BarLoader color="var(--blanco-secundario)" size={10} />
            </div>
          ) : (
            <div className={selfStyle.storeContent}>
              {recompensas?.map((reward) => (
                <RewardItem key={reward.id + reward.nombre} reward={reward} />
              ))}
              <button className={selfStyle.addRewardButton} onClick={handleOpenRewardCreateForm}>
                <FontAwesomeIcon icon="fa-solid fa-plus" size="2xl" />
              </button>
            </div>
          )
        ) : isLoadingStudents || isLoadingMedals ? (
          <div className={styles.barLoaderContainer}>
            <BarLoader color="var(--blanco-secundario)" size={10} />
          </div>
        ) : (
          studentsFiltrados?.map((item) => <StudentItem key={item.id} student={item} medals={medals} />)
        )}
      </div>
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

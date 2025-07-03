// GroupPage.jsx
import React, { useState } from "react";
import styles from "../generics/BaseManagerPage.module.css";
import selfStyle from "./GroupPage.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { BarLoader } from "react-spinners";
import { useSelector } from "react-redux";
import { selectUserRole } from "../auth/hooks/userSlice";
import BaseManagerPage from "../generics/BaseManagerPage";
import StudentItem from "./components/StudentItem";
import RewardItem from "./components/teacher/RewardItem.jsx";
import RewardCreateForm from "./components/teacher/RewardCreateForm.jsx";
import GroupProfileView from "./components/student/GroupProfileView.jsx";
import { useGrupo, useAlumnosGrupo, useRecompensasTienda } from "./hooks/useGrupoMutation";
import { usePerfilGrupo } from "./hooks/useStudentMutation.js";

import { useMedallasProfesor } from "../medals/hooks/useMedalMutation";
import { useParams } from "react-router-dom";
import ApplicationRequests from "./components/teacher/ApplicationRequests";

// ...imports
const GroupPage = () => {
  const { id } = useParams();
  const groupId = Number(id);

  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";

  const [search, setSearch] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [modalContent, setModalContent] = useState(null);
  const [modalTitle, setModalTitle] = useState("");

  const [selectedView, setSelectedView] = useState("alumnos"); // alumnos | tienda | solicitudes

  const { data: group, isLoading: isLoadingGroup } = useGrupo(groupId);
  const { data: students, isLoading: isLoadingStudents } = useAlumnosGrupo(groupId);
  const { data: medals, isLoading: isLoadingMedals } = useMedallasProfesor(isProfesor);
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasTienda(group?.idTienda);
  const { data: perfil, isLoadingPerfil } = usePerfilGrupo(groupId, isProfesor);

  const studentsFiltrados = students?.filter((item) => item.nombreEstudiante.toLowerCase().includes(search.toLowerCase()));

  const handleOpenRewardCreateForm = () => {
    setModalContent(<RewardCreateForm groupId={groupId} />);
    setModalTitle("Crear nueva recompensa");
    setShowModal(true);
  };

  const handleOpenApplicationRequests = () => {
    setModalContent(<ApplicationRequests groupId={groupId} link={group.urlCompleta} />);
    setModalTitle(`Solicitudes de unión del Grupo ${group.institucion.toUpperCase()} - ${group.nombre.toUpperCase()}`);
    setShowModal(true);
  };

  const actions = (
    <div className={selfStyle.acciones}>
      {!isProfesor && (
        <label>
          <input type="radio" value="perfil" checked={selectedView === "perfil"} onChange={() => setSelectedView("perfil")} />
          <FontAwesomeIcon icon="fa-solid fa-user" size="xl" />
        </label>
      )}

      <label>
        <input type="radio" value="alumnos" checked={selectedView === "alumnos"} onChange={() => setSelectedView("alumnos")} />
        <FontAwesomeIcon icon="fa-solid fa-users" size="xl" />
      </label>

      <label>
        <input type="radio" value="tienda" checked={selectedView === "tienda"} onChange={() => setSelectedView("tienda")} />
        <FontAwesomeIcon icon="fa-solid fa-store" size="xl" />
      </label>

      {isProfesor && (
        <label>
          <input type="radio" value="solicitudes" checked={selectedView === "solicitudes"} onChange={handleOpenApplicationRequests} />
          <FontAwesomeIcon icon="fa-solid fa-user-plus" size="xl" />
        </label>
      )}
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
          <FontAwesomeIcon icon="fa-solid fa-book-bookmark" /> {group.materia.toUpperCase()}
        </h3>
        <h3>
          <FontAwesomeIcon icon="fa-solid fa-school" /> {group.institucion.toUpperCase()} - {group.nombre.toUpperCase()}
        </h3>
      </div>

      <div className={selfStyle.itemsContainer}>
        {!isProfesor &&
          (isLoadingPerfil || !perfil ? (
            <div className={styles.barLoaderContainer}>
              <BarLoader color="var(--blanco-secundario)" size={10} />
            </div>
          ) : (
            selectedView !== "perfil" && (
              <div className={selfStyle.resumen}>
                <div>
                  <p>
                    <FontAwesomeIcon icon="fa fa-bullseye" />
                    {perfil.metaCalificacion}
                  </p>
                </div>
                <div>
                  <p>
                    <FontAwesomeIcon icon="fa fa-award" />
                    {perfil.medallas.length}
                  </p>
                </div>

                <div>
                  <p>
                    <FontAwesomeIcon icon="fa fa-coins" />
                    {perfil.monedas}
                  </p>
                </div>
              </div>
            )
          ))}

        {selectedView === "tienda" ? (
          isLoadingRecompensas ? (
            <div className={styles.barLoaderContainer}>
              <BarLoader color="var(--blanco-secundario)" size={10} />
            </div>
          ) : (
            <div className={selfStyle.storeContent}>
              {recompensas?.map((reward) => (
                <RewardItem key={reward.id + reward.nombre} reward={reward} redeemed={false} perfilId={perfil?.id} />
              ))}
              <button className={selfStyle.addRewardButton} onClick={handleOpenRewardCreateForm}>
                <FontAwesomeIcon icon="fa-solid fa-plus" size="2xl" />
              </button>
            </div>
          )
        ) : selectedView === "alumnos" ? (
          isLoadingStudents || isLoadingMedals ? (
            <div className={styles.barLoaderContainer}>
              <BarLoader color="var(--blanco-secundario)" size={10} />
            </div>
          ) : (
            <div className={selfStyle.studentsContainer}>
              {studentsFiltrados?.map((item) => (
                <StudentItem key={item.id} student={item} medals={medals} />
              ))}
            </div>
          )
        ) : selectedView === "perfil" ? (
          <GroupProfileView perfil={perfil} isLoading={isLoadingPerfil} />
        ) : null}
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

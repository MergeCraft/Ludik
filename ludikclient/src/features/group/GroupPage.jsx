// GroupPage.jsx
import React, { useState } from "react";
import style from "./GroupPage.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BarLoader from "../generics/BarLoader.jsx";
import { useSelector } from "react-redux";
import { selectUserRole } from "../auth/hooks/userSlice";
import BaseManagerPage from "../generics/BaseManagerPage";
import StudentItem from "./components/StudentItem";
import RewardItem from "./components/RewardItem.jsx";
import RewardCreateForm from "./components/teacher/RewardCreateForm.jsx";
import GroupProfileView from "./components/student/GroupProfileView.jsx";
import GroupConfigView from "./components/configs/GroupConfigView.jsx";
import { useGrupo, useAlumnosGrupo, useRecompensasTienda } from "./hooks/useGrupoMutation";
import { usePerfilGrupo } from "./hooks/useStudentMutation.js";

import { useMedallasProfesor } from "../medals/hooks/useMedalMutation";
import { useParams } from "react-router-dom";
import ApplicationRequests from "./components/teacher/ApplicationRequests";
import GroupRankingView from "./components/rankings/GroupRankingView.jsx";

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

  const getLabelClass = (view) => (selectedView === view ? style.activeLabel : "");

  const actions = (
    <div className={style.acciones}>
      {!isProfesor && (
        <label className={getLabelClass("perfil")}>
          <input type="radio" value="perfil" checked={selectedView === "perfil"} onChange={() => setSelectedView("perfil")} />
          <FontAwesomeIcon icon="fa-solid fa-user" size="xl" />
        </label>
      )}

      <label className={getLabelClass("alumnos")}>
        <input type="radio" value="alumnos" checked={selectedView === "alumnos"} onChange={() => setSelectedView("alumnos")} />
        <FontAwesomeIcon icon="fa-solid fa-users" size="xl" />
      </label>

      <label className={getLabelClass("tienda")}>
        <input type="radio" value="tienda" checked={selectedView === "tienda"} onChange={() => setSelectedView("tienda")} />
        <FontAwesomeIcon icon="fa-solid fa-store" size="xl" />
      </label>

      <label className={getLabelClass("rankings")}>
        <input type="radio" value="rankings" checked={selectedView === "rankings"} onChange={() => setSelectedView("rankings")} />
        <FontAwesomeIcon icon="fa-solid fa-ranking-star" size="xl" />
      </label>

      {isProfesor && (
        <>
          <label className={getLabelClass("solicitudes")}>
            <input type="radio" value="solicitudes" checked={selectedView === "solicitudes"} onChange={handleOpenApplicationRequests} />
            <FontAwesomeIcon icon="fa-solid fa-user-plus" size="xl" />
          </label>
          <label className={getLabelClass("configs")}>
            <input type="radio" value="configs" checked={selectedView === "configs"} onChange={() => setSelectedView("configs")} />
            <FontAwesomeIcon icon="fa-solid fa-gear" size="xl" />
          </label>
        </>
      )}
    </div>
  );

  const items = isLoadingGroup ? (
    <BarLoader />
  ) : (
    <div className={style.groupContainer}>
      <div className={style.infoGrupo}>
        <h3>
          <FontAwesomeIcon icon="fa-solid fa-book-bookmark" /> {group.materia.toUpperCase()}
        </h3>
        <h3>
          <FontAwesomeIcon icon="fa-solid fa-school" /> {group.institucion.toUpperCase()} - {group.nombre.toUpperCase()}
        </h3>
      </div>

      <div className={style.itemsContainer}>
        {!isProfesor &&
          (isLoadingPerfil || !perfil ? (
            <BarLoader />
          ) : (
            selectedView !== "perfil" && (
              <div className={style.resumen}>
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
            <BarLoader />
          ) : (
            <div className={style.storeContent}>
              {isProfesor && (
                <button className={style.addRewardButton} onClick={handleOpenRewardCreateForm}>
                  <FontAwesomeIcon icon="fa-solid fa-plus" size="2xl" />
                </button>
              )}

              {recompensas?.map((reward) => (
                <RewardItem key={reward.id + reward.nombre} reward={reward} redeemed={false} perfilId={perfil?.id} />
              ))}
            </div>
          )
        ) : selectedView === "alumnos" ? (
          isLoadingStudents || isLoadingMedals ? (
            <BarLoader />
          ) : (
            <div className={style.studentsContainer}>
              {studentsFiltrados?.map((item) => (
                <StudentItem key={item.id} student={item} medals={medals} />
              ))}
            </div>
          )
        ) : selectedView === "perfil" ? (
          <GroupProfileView perfil={perfil} isLoading={isLoadingPerfil} />
        ) : selectedView === "rankings" ? (
          <GroupRankingView setModalContent={setModalContent} setModalTitle={setModalTitle} setShowModal={setShowModal} groupId={groupId} showTeacherOptions={isProfesor} />
        ) : selectedView === "configs" ? (
          <GroupConfigView id={groupId} group={group} setModalContent={setModalContent} setModalTitle={setModalTitle} setShowModal={setShowModal} />
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

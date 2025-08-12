// GroupPage.jsx
import React, { useState } from "react";
import style from "./GroupPage.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BarLoader from "../generics/BarLoader.jsx";
import { useSelector } from "react-redux";
import { selectUserRole } from "../auth/hooks/userSlice";
import BaseManagerPage from "../generics/BaseManagerPage";
import StudentItem from "./components/StudentItem";
import GroupProfileView from "./components/student/GroupProfileView.jsx";
import GroupConfigView from "./components/configs/GroupConfigView.jsx";
import GroupPacView from "./components/pac/GroupPacView.jsx";
import MedalThresholdView from "./components/medalThreshold/MedalThresholdView.jsx";
import StoreGroupView from "./components/store/StoreGroupView"; // ajusta la ruta si es necesario

import { useGrupo, useAlumnosGrupo, useAlumnosGrupoParaEstudiante, useRecompensasTienda, useTiposKudo } from "./hooks/useGrupoMutation";
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

  // Cargar datos del grupo, alumnos, medallas y recompensas
  // Usar hooks personalizados para obtener los datos necesarios
  const { data: group, isLoading: isLoadingGroup } = useGrupo(groupId);
  const { data: students, isLoading: isLoadingStudents } = isProfesor ? useAlumnosGrupo(groupId) : useAlumnosGrupoParaEstudiante(groupId);
  const { data: medals, isLoading: isLoadingMedals } = useMedallasProfesor(isProfesor);
  const { data: tiposKudo, isLoading: isLoadingKudos } = useTiposKudo();
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasTienda(group?.idTienda);
  const { data: perfil, isLoadingPerfil } = usePerfilGrupo(groupId, isProfesor);

  const studentsFiltrados = students?.filter((item) => item.nombreEstudiante.toLowerCase().includes(search.toLowerCase()));

  const handleOpenApplicationRequests = () => {
    setModalContent(<ApplicationRequests groupId={groupId} link={group.urlCompleta} />);
    setModalTitle(`Solicitudes de unión del Grupo ${group.institucion.toUpperCase()} - ${group.nombre.toUpperCase()}`);
    setShowModal(true);
  };

  const getLabelClass = (view) => {
    const base = selectedView === view ? style.activeLabel : "";
    const specific =
      selectedView === view
        ? style[`active${view.charAt(0).toUpperCase() + view.slice(1)}`] // genera `activeAlumnos`, `activeTienda`, etc.
        : "";
    return `${base} ${specific}`;
  };

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
        <FontAwesomeIcon icon="fa-solid fa-people-group" size="xl" />
      </label>

      <label className={getLabelClass("tienda")}>
        <input type="radio" value="tienda" checked={selectedView === "tienda"} onChange={() => setSelectedView("tienda")} />
        <FontAwesomeIcon icon="fa-solid fa-store" size="xl" />
      </label>

      <label className={getLabelClass("rankings")}>
        <input type="radio" value="rankings" checked={selectedView === "rankings"} onChange={() => setSelectedView("rankings")} />
        <FontAwesomeIcon icon="fa-solid fa-ranking-star" size="xl" />
      </label>

      <label className={getLabelClass("pac")}>
        <input type="radio" value="pac" checked={selectedView === "pac"} onChange={() => setSelectedView("pac")} />
        <FontAwesomeIcon icon="fa-solid fa-handshake" size="xl" />
      </label>

      <label className={getLabelClass("threshold")}>
        <input type="radio" value="threshold" checked={selectedView === "threshold"} onChange={() => setSelectedView("threshold")} />
        <FontAwesomeIcon icon="fa-solid fa-chart-bar" size="xl" />
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
        {isProfesor && (
          <h3>
            <FontAwesomeIcon icon="fa-solid fa-school" /> {group.institucion.toUpperCase()} - {group.nombre.toUpperCase()}
          </h3>
        )}
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
          <StoreGroupView
            recompensas={recompensas}
            isLoading={isLoadingRecompensas}
            isProfesor={isProfesor}
            perfil={perfil}
            setShowModal={setShowModal}
            setModalContent={setModalContent}
            setModalTitle={setModalTitle}
            grupoId={groupId}
          />
        ) : selectedView === "alumnos" ? (
          isLoadingStudents || isLoadingMedals ? (
            <BarLoader />
          ) : students?.length === 0 || students === undefined ? (
            <div className={style.noStudentsMessage}>
              <p>
                {isProfesor ? (
                  <>
                    El grupo aún no tiene alumnos. Dirígete a la sección de <strong onClick={() => setSelectedView("configs")}> configuración </strong> o
                    <strong onClick={handleOpenApplicationRequests}> solicitudes </strong> para compartir el código de unión con tus estudiantes.
                  </>
                ) : (
                  "Aún no hay mas compañeros en este grupo"
                )}
              </p>
            </div>
          ) : (
            <div className={style.studentsContainer}>
              {studentsFiltrados?.map((item) => (
                <StudentItem key={item.id} perfilEmisorId={perfil?.id} student={item} medals={medals} kudos={tiposKudo} isLoadingKudos={isLoadingKudos} showProfesorOptions={isProfesor} />
              ))}
            </div>
          )
        ) : selectedView === "perfil" ? (
          <GroupProfileView perfil={perfil} isLoading={isLoadingPerfil} setModalContent={setModalContent} setShowModal={setShowModal} setModalTitle={setModalTitle} />
        ) : selectedView === "rankings" ? (
          <GroupRankingView
            setModalContent={setModalContent}
            setModalTitle={setModalTitle}
            setShowModal={setShowModal}
            groupId={groupId}
            showTeacherOptions={isProfesor}
            idPerfilEstudiante={perfil.id}
          />
        ) : selectedView === "pac" ? (
          <GroupPacView recompensas={recompensas} setModalContent={setModalContent} setModalTitle={setModalTitle} setShowModal={setShowModal} groupId={groupId} showTeacherOptions={isProfesor} />
        ) : selectedView === "threshold" ? (
          <MedalThresholdView setModalContent={setModalContent} setModalTitle={setModalTitle} setShowModal={setShowModal} groupId={groupId} showTeacherOptions={isProfesor} />
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
      searchPlaceholder={isProfesor ? "alumno" : "compañero"}
      showModal={showModal}
      setShowModal={setShowModal}
      searchValue={search}
      onSearchChange={setSearch}
    />
  );
};

export default GroupPage;

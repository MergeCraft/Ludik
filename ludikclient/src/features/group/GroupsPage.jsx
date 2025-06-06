import React, { useState } from "react";
import { useSelector } from "react-redux";
import styles from "./GroupPage.module.css";
import genericGroupImage from "../../assets/genericGroupImage.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { BarLoader } from "react-spinners";
import { selectUserRole } from "../auth/hooks/userSlice";
import { useGruposProfesor } from "./hooks/useGrupoMutation.js";

import GroupItem from "./components/GroupItem.jsx";
import Modal from "../generics/Modal.jsx";
import GroupCreateModal from "./components/teacher/GroupCreateForm.jsx";
import GroupUnionLinkModal from "./components/student/GroupUnionLinkForm.jsx";

export const GroupsPage = () => {
  const role = useSelector(selectUserRole); // ✅ obtiene el rol desde Redux
  const isProfesor = role === "Profesor";

  const [showModal, setShowModal] = useState(false);
  const [modalTipo, setModalTipo] = useState(null);

  const { data: grupos, isLoading } = useGruposProfesor();

  const gruposFormateados =
    grupos?.map((g) => ({
      name: g.nombre,
      grade: g.materia,
      students: "-", // si tu backend no envía cantidad
      imgSrc: genericGroupImage,
    })) || [];

  return (
    <div className={styles.studentGroups}>
      <div className={styles.acciones}>
        {isProfesor ? (
          <button
            className={`${styles.accionPrincipal} button-secondary`}
            onClick={() => {
              setModalTipo("crear");
              setShowModal(true); // <- FALTA ESTA LÍNEA
            }}
          >
            Crear grupo
          </button>
        ) : (
          <button
            className={`${styles.accionPrincipal} button-secondary`}
            onClick={() => {
              setModalTipo("unir");
              setShowModal(true); // <- FALTA ESTA LÍNEA
            }}
          >
            Unirse a un grupo
          </button>
        )}

        <div className={styles.accionesBusqueda}>
          <input type="text" placeholder="Buscar grupo" className={styles.buscador} />
          <FontAwesomeIcon className={styles.filtros} icon="fa-solid fa-filter" size="2xl" />
        </div>
      </div>

      {isLoading ? (
        <div className={styles.loaderContainer}>
          <BarLoader color="var(--blanco-secundario)" size={10} />
        </div>
      ) : (
        gruposFormateados.map((group, index) => <GroupItem key={index} {...group} />)
      )}

      {showModal && (
        <Modal
          onClose={() => setShowModal(false)}
          modalTitle={modalTipo === "crear" ? "Crea un nuevo grupo" : "Unete a un grupo"}
          content={modalTipo === "crear" ? <GroupCreateModal onClose={() => setShowModal(false)} /> : <GroupUnionLinkModal onClose={() => setShowModal(false)} />}
        />
      )}
    </div>
  );
};

export default GroupsPage;

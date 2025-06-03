import React, { useState } from "react";
import { useSelector } from "react-redux";
import GroupStudentItem from "./components/student/GroupStudentItem.jsx";
import styles from "./GroupPage.module.css";
import genericGroupImage from "../../assets/genericGroupImage.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { selectUserRole } from "../auth/hooks/userSlice";
import GroupCreateModal from "./components/teacher/GroupCreateModal.jsx";

const groupData = [
  { name: "Liceo 13", grade: "2° C", students: 32, imgSrc: genericGroupImage },
  { name: "Liceo 11", grade: "4° C", students: 30, imgSrc: genericGroupImage },
  { name: "UTU 15", grade: "1° A", students: 23, imgSrc: genericGroupImage },
  { name: "Liceo 1", grade: "4° A", students: 26, imgSrc: genericGroupImage },
  { name: "Liceo 5", grade: "3° B", students: 28, imgSrc: genericGroupImage },
  { name: "UTU 8", grade: "2° D", students: 19, imgSrc: genericGroupImage },
  { name: "Liceo 9", grade: "1° C", students: 35, imgSrc: genericGroupImage },
  { name: "Liceo 7", grade: "5° A", students: 27, imgSrc: genericGroupImage },
  { name: "UTU 10", grade: "3° A", students: 22, imgSrc: genericGroupImage },
  { name: "Liceo 12", grade: "6° B", students: 25, imgSrc: genericGroupImage },
  { name: "Liceo 2", grade: "2° B", students: 31, imgSrc: genericGroupImage },
  { name: "UTU 6", grade: "4° D", students: 24, imgSrc: genericGroupImage },
];

export const GroupsPage = () => {
  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";

  const [showModal, setShowModal] = useState(false);

  return (
    <div className={styles.studentGroups}>
      <div className={styles.acciones}>
        {isProfesor && (
          <button className={`${styles.crearGrupo} button-secondary`} onClick={() => setShowModal(true)}>
            Crear grupo
          </button>
        )}

        <div className={styles.accionesBusqueda}>
          <input type="text" placeholder="Buscar grupo" className={styles.buscador} />
          <FontAwesomeIcon className={styles.filtros} icon="fa-solid fa-filter" size="2xl" />
        </div>
      </div>

      {groupData.map((group, index) => (
        <GroupStudentItem key={index} {...group} />
      ))}

      {showModal && <GroupCreateModal onClose={() => setShowModal(false)} />}
    </div>
  );
};

export default GroupsPage;

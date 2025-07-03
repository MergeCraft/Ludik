// components/StudentItem.jsx
import React, { useState } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useSelector } from "react-redux";
import { selectUserRole } from "../../auth/hooks/userSlice";
import genericProfileImage from "../../../assets/genericStudentAvatar.png";
import styles from "./StudentItem.module.css";

import { useAsignarMedalla } from "../hooks/useGrupoMutation";

const StudentItem = ({ student, medals }) => {
  const [selectedMedal, setSelectedMedal] = useState(""); // <- estado para controlar el valor del select
  const [medalAsignationOption, setMedalAsignationOption] = useState(true);
  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";
  const { mutate } = useAsignarMedalla();

  const handleMedalChange = (e) => {
    const medallaId = Number(e.target.value);
    if (!medallaId) return;

    mutate(
      { perfilId: student.id, medallaId },
      {
        onSuccess: () => setSelectedMedal(""), // <- resetea el select al éxito
      }
    );
  };

  return (
    <div className={styles.card}>
      <img src={student.enlaceAvatarMiniatura || genericProfileImage} alt="avatar" className={styles.avatar} />
      <div className={styles.centrales}>
        <p>{student.nombreEstudiante}</p>
        <div className={styles.actionsContainer}>
          {isProfesor && (
            <>
              <button className={styles.opcionBorrado} onClick={() => setMedalAsignationOption((prev) => !prev)}>
                <FontAwesomeIcon icon="fa-solid fa-trash" size="lg" />
                <FontAwesomeIcon icon="fa-solid fa-arrow-right-arrow-left" size="2xs" />
                <FontAwesomeIcon icon="fa-solid fa-plus" size="lg" />
              </button>
              <div className={styles.asignarMedalla}>
                <p>{medalAsignationOption ? "Asignación de medallas" : "Eliminar medallas"}</p>

                {medalAsignationOption ? (
                  <select
                    className={`button ${styles.medallas}`}
                    name="medallas"
                    value={selectedMedal}
                    onChange={(e) => {
                      setSelectedMedal(e.target.value); // actualizar UI
                      handleMedalChange(e); // ejecutar mutación
                    }}
                  >
                    <option value="">Selecciona medalla</option>
                    {Array.isArray(medals) &&
                      medals.map((medalla) => (
                        <option key={medalla.id} value={medalla.id}>
                          {medalla.nombre}
                        </option>
                      ))}
                  </select>
                ) : (
                  <select
                    className={`button ${styles.medallas}`}
                    name="medallas"
                    value={selectedMedal}
                    onChange={(e) => {
                      setSelectedMedal(e.target.value); // actualizar UI
                      handleMedalChange(e); // ejecutar mutación
                    }}
                  >
                    <option value="">Selecciona medalla</option>
                    {Array.isArray(medals) &&
                      medals.map((medalla) => (
                        <option key={medalla.id} value={medalla.id}>
                          {medalla.nombre}
                        </option>
                      ))}
                  </select>
                )}
              </div>
            </>
          )}
        </div>
      </div>

      <FontAwesomeIcon icon="fa-solid fa-arrow-right-from-bracket" size="lg" />
    </div>
  );
};

StudentItem.propTypes = {
  student: PropTypes.shape({
    id: PropTypes.number.isRequired,
    avatarGrupoId: PropTypes.number,
    enlaceAvatarCompleto: PropTypes.string,
    enlaceAvatarMiniatura: PropTypes.string,
    metaCalificacion: PropTypes.number,
    estudianteId: PropTypes.string.isRequired,
    nombreEstudiante: PropTypes.string.isRequired,
    monedas: PropTypes.number,
    grupoId: PropTypes.number,
    nombreGrupo: PropTypes.string,
    calificacionActual: PropTypes.number,
  }).isRequired,
  medals: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      nombre: PropTypes.string.isRequired,
    })
  ).isRequired,
};

export default StudentItem;

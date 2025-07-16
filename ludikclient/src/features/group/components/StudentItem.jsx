// components/StudentItem.jsx
import React, { useState } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useSelector } from "react-redux";
import { selectUserRole } from "../../auth/hooks/userSlice";
import genericProfileImage from "../../../assets/genericStudentAvatar.png";
import styles from "./StudentItem.module.css";

import { useAsignarMedalla, useEliminarMedalla } from "../hooks/useGrupoMutation";

const StudentItem = ({ student, medals }) => {
  const [selectedMedal, setSelectedMedal] = useState(""); // <- estado para controlar el valor del select
  const [medalAsignationOption, setMedalAsignationOption] = useState(true);
  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";
  
  const { mutate: asignar } = useAsignarMedalla();
  const { mutate: eliminar } = useEliminarMedalla();

  const handleMedalChange = (e) => {
    const medallaId = Number(e.target.value);
    if (!medallaId) return;

    const mutationFn = medalAsignationOption ? asignar : eliminar;

    mutationFn(
      { perfilId: student.id, medallaId },
      {
        onSuccess: () => setSelectedMedal(""),
      }
    );
  };

  return (
    <div className={styles.card}>
      <img src={student.enlaceAvatarMiniatura || genericProfileImage} alt="avatar" className={styles.avatar} />
      <div className={styles.centrales}>
        <p className={styles.nombreEstudiante}>{student.nombreEstudiante}</p>
        <div className={styles.actionsContainer}>
          {isProfesor && (
            <>
              <button className={styles.opcionBorrado} onClick={() => setMedalAsignationOption((prev) => !prev)}>
                <FontAwesomeIcon icon="fa-solid fa-arrow-right-arrow-left" size="l" />
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
                    <option value="">Asgina Medalla</option>
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
                    disabled={!Array.isArray(student.medallas) || student.medallas.length === 0}
                  >
                    <option value="">{Array.isArray(student.medallas) && student.medallas.length > 0 ? "Elimina medalla" : "Alumno sin medallas"}</option>
                    {Array.isArray(medals) &&
                      student.medallas.map((medalla) => (
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
    avatarGrupoId: PropTypes.number.isRequired,
    enlaceAvatarCompleto: PropTypes.string.isRequired,
    enlaceAvatarMiniatura: PropTypes.string.isRequired,
    metaCalificacion: PropTypes.number.isRequired,
    estudianteId: PropTypes.string.isRequired,
    nombreEstudiante: PropTypes.string.isRequired,
    monedas: PropTypes.number.isRequired,
    grupoId: PropTypes.number.isRequired,
    nombreGrupo: PropTypes.string.isRequired,
    calificacionActual: PropTypes.number.isRequired,
    medallas: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number.isRequired,
        nombre: PropTypes.string.isRequired,
        urlImagen: PropTypes.string.isRequired,
        descripcion: PropTypes.string.isRequired,
        cantidadMedallasBrinda: PropTypes.number.isRequired,
        esAsignacionMutua: PropTypes.bool.isRequired,
      })
    ).isRequired,
  }).isRequired,
  medals: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      nombre: PropTypes.string.isRequired,
    })
  ).isRequired,
};

export default StudentItem;

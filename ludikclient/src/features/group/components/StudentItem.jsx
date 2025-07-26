// components/StudentItem.jsx
import React, { useState } from "react";
import { useSelector } from "react-redux";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import genericProfileImage from "../../../assets/genericStudentAvatar.png";
import styles from "./StudentItem.module.css";

import { useAsignarMedalla, useEliminarMedalla } from "../hooks/useGrupoMutation";
import { useAsignarKudo } from "../hooks/useStudentMutation";

const StudentItem = ({ perfilEmisorId, student, medals, showProfesorOptions }) => {
  // Estado para manejar la selección de medalla
  const [selectedMedal, setSelectedMedal] = useState("");
  const [medalAsignationOption, setMedalAsignationOption] = useState(true);

  // Estado para manejar la selección de Kudo
  const [selectedKudo, setSelectedKudo] = useState("");
  const [kudoAsignationOption, setKudoAsignationOption] = useState(true);

  // Mutaciones para asignar o eliminar medallas
  const { mutate: asignar } = useAsignarMedalla();
  const { mutate: eliminar } = useEliminarMedalla();

  // Mutación para asignar Kudo
  const { mutate: asignarKudo } = useAsignarKudo();

  // Función para manejar el cambio de medalla
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

  // Función para manejar el cambio de Kudo
  const handleKudoChange = (e) => {
    const kudoId = Number(e.target.value);
    if (!kudoId) return;

    const kudo = medals.find((k) => k.id === kudoId);
    if (!kudo) return;

    asignarKudo({
      idPerfilEstudianteRecibe: student.id,
      idPerfilEstudianteEmisor: perfilEmisorId,
      kudo,
    });

    setSelectedKudo(""); // limpiar selección
  };

  return (
    <div className={styles.card}>
      <img src={student.enlaceAvatarMiniatura || genericProfileImage} alt="avatar" className={styles.avatar} />
      <div className={styles.centrales}>
        <p className={styles.nombreEstudiante}>{student.nombreEstudiante}</p>
        <div className={styles.actionsContainer}>
          {showProfesorOptions ? (
            <>
              <button className={styles.opcionBorrado} onClick={showProfesorOptions ? () => setMedalAsignationOption((prev) => !prev) : () => setKudoAsignationOption((prev) => !prev)}>
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
          ) : (
            <select
              className={`button ${styles.medallas}`}
              name="kudos"
              value={selectedKudo}
              onChange={(e) => {
                setSelectedKudo(e.target.value); // actualizar UI
                handleKudoChange(e); // ejecutar mutación
              }}
            >
              <option value="">Asgina Kudo</option>
              <option value="">No hay tales kudos bro, ai te bes rey</option>
              {/* {Array.isArray(medals) &&
                medals.map((medalla) => (
                  <option key={medalla.id} value={medalla.id}>
                    {medalla.nombre}
                  </option>
                ))} */}
            </select>
          )}
        </div>
      </div>

      <FontAwesomeIcon icon="fa-solid fa-arrow-right-from-bracket" size="lg" />
    </div>
  );
};

StudentItem.propTypes = {
  perfilEmisorId: PropTypes.number.isRequired,
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
  showProfesorOptions: PropTypes.bool.isRequired,
};

export default StudentItem;

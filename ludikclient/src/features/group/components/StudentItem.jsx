// components/StudentItem.jsx
import React, { useState } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import genericProfileImage from "../../../assets/genericStudentAvatar.png";
import styles from "./StudentItem.module.css";

import { useAsignarMedalla, useEliminarMedalla } from "../hooks/useGrupoMutation";
import { useAsignarKudo } from "../hooks/useStudentMutation";

const StudentItem = ({ perfilEmisorId, student, medals, showProfesorOptions }) => {
  const [selectedMedal, setSelectedMedal] = useState("");
  const [medalAsignationOption, setMedalAsignationOption] = useState(true);
  const [selectedKudo, setSelectedKudo] = useState("");
  const [kudoAsignationOption, setKudoAsignationOption] = useState(true);

  const { mutate: asignar } = useAsignarMedalla();
  const { mutate: eliminar } = useEliminarMedalla();
  const { mutate: asignarKudo } = useAsignarKudo();

  const [isMedalLoading, setIsMedalLoading] = useState(false);
  const [isKudoLoading, setIsKudoLoading] = useState(false);

  const handleMedalChange = (e) => {
    const medallaId = Number(e.target.value);
    if (!medallaId) return;

    setIsMedalLoading(true);
    const mutationFn = medalAsignationOption ? asignar : eliminar;

    mutationFn(
      { perfilId: student.id, medallaId },
      {
        onSuccess: () => {
          setSelectedMedal("");
          setIsMedalLoading(false);
        },
        onError: () => setIsMedalLoading(false),
      }
    );
  };

  const handleKudoChange = (e) => {
    const kudoId = Number(e.target.value);
    if (!kudoId) return;

    const kudo = medals.find((k) => k.id === kudoId);
    if (!kudo) return;

    setIsKudoLoading(true);

    asignarKudo(
      {
        idPerfilEstudianteRecibe: student.id,
        idPerfilEstudianteEmisor: perfilEmisorId,
        kudo,
      },
      {
        onSuccess: () => {
          setSelectedKudo("");
          setIsKudoLoading(false);
        },
        onError: () => setIsKudoLoading(false),
      }
    );
  };

  return (
    <div className={styles.card}>
      <img src={student.enlaceAvatarMiniatura || genericProfileImage} alt="avatar" className={styles.avatar} />
      <div className={styles.centrales}>
        <p className={styles.nombreEstudiante}>{student.nombreEstudiante}</p>
        <div className={styles.actionsContainer}>
          {showProfesorOptions ? (
            <>
              <button className={styles.opcionBorrado} onClick={() => setMedalAsignationOption((prev) => !prev)}>
                <FontAwesomeIcon icon="fa-solid fa-arrow-right-arrow-left" size="l" />
              </button>

              <div className={styles.asignarMedalla}>
                <p>{medalAsignationOption ? "Asignación de medallas" : "Eliminar medallas"}</p>

                <select
                  className={`button ${styles.medallas}`}
                  name="medallas"
                  value={selectedMedal}
                  onChange={(e) => {
                    setSelectedMedal(e.target.value);
                    handleMedalChange(e);
                  }}
                  disabled={!medalAsignationOption && (!Array.isArray(student.medallas) || student.medallas.length === 0)}
                >
                  {isMedalLoading ? (
                    <option value="">{medalAsignationOption ? "Asignando Medalla..." : "Eliminando Medalla..."}</option>
                  ) : (
                    <>
                      <option value="">{medalAsignationOption ? "Asigna Medalla" : Array.isArray(student.medallas) && student.medallas.length > 0 ? "Elimina medalla" : "Alumno sin medallas"}</option>
                      {(medalAsignationOption ? medals : student.medallas || []).map((medalla) => (
                        <option key={medalla.id} value={medalla.id}>
                          {medalla.nombre}
                        </option>
                      ))}
                    </>
                  )}
                </select>
              </div>
            </>
          ) : (
            <div className={styles.asignarMedalla}>
              <p>{kudoAsignationOption ? "Asignación de Kudos" : "Eliminar Kudos"}</p>
              <select
                className={`button ${styles.medallas}`}
                name="kudos"
                value={selectedKudo}
                onChange={(e) => {
                  setSelectedKudo(e.target.value);
                  handleKudoChange(e);
                }}
              >
                {isKudoLoading ? (
                  <option value="">Asignando Kudo...</option>
                ) : (
                  <>
                    <option value="">Asigna Kudo</option>
                    <option value="">No hay tales kudos bro, ai te bes rey</option>
                  </>
                )}
              </select>
            </div>
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

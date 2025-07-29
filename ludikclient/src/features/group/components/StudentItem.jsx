// components/StudentItem.jsx
import React, { useState } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import genericProfileImage from "../../../assets/genericStudentAvatar.png";
import styles from "./StudentItem.module.css";

import { useAsignarMedalla, useEliminarMedalla } from "../hooks/useGrupoMutation";
import { useAsignarKudo } from "../hooks/useStudentMutation";

import MedalActionMenu from "./MedalActionMenu.jsx";

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
              <div className={styles.asignarMedalla}>
                {/* Menú para asignar medallas */}
                <div className={styles.menuSection}>
                  <MedalActionMenu
                    items={medals}
                    isAssign={true}
                    isLoading={isMedalLoading}
                    onConfirm={(medallaId, cantidad) => {
                      setIsMedalLoading(true);
                      for (let i = 0; i < cantidad; i++) {
                        asignar(
                          { perfilId: student.id, medallaId },
                          {
                            onSuccess: () => setIsMedalLoading(false),
                            onError: () => setIsMedalLoading(false),
                          }
                        );
                      }
                    }}
                  />
                </div>

                {/* Menú para eliminar medallas */}
                <div className={styles.menuSection}>
                  <MedalActionMenu
                    items={student.medallas || []}
                    isAssign={false}
                    isLoading={isMedalLoading}
                    onConfirm={(medallaId, cantidad) => {
                      setIsMedalLoading(true);
                      for (let i = 0; i < cantidad; i++) {
                        eliminar(
                          { perfilId: student.id, medallaId },
                          {
                            onSuccess: () => setIsMedalLoading(false),
                            onError: () => setIsMedalLoading(false),
                          }
                        );
                      }
                    }}
                  />
                </div>
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

// GroupProfileView.jsx
import React from "react";
import PropTypes from "prop-types";
import BarLoader from "../../../generics/BarLoader";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useRecompensasPerfil, useImagenPerfil, useBarraProgresoPerfil, useDefinirMetaCalificacion } from "../../hooks/useStudentMutation";
import RewardItem from "../RewardItem";
import MedalCard from "../../../medals/components/MedalCard";
import StudentAvatarEditor from "./StudentAvatarEditor";

import styles from "./GroupProfileView.module.css";

const GroupProfileView = ({ perfil, isLoading, setModalContent, setModalTitle, setShowModal }) => {
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasPerfil(perfil?.id);
  const { data: imagenPerfil, isLoading: isLoadingImagen, isError: isErrorImagen } = useImagenPerfil(perfil?.id);
  const { data: barraProgreso, isLoading: isLoadingBarra } = useBarraProgresoPerfil(perfil?.id);

  const { mutate: setMeta } = useDefinirMetaCalificacion(perfil?.id);

  const avatarUrl = imagenPerfil?.urlCompleta;

  return isLoading ? (
    <BarLoader />
  ) : (
    <div className={styles.container}>
      <section>
        <h3>Avatar</h3>
        {isLoadingImagen && !imagenPerfil && !isErrorImagen ? (
          <BarLoader />
        ) : (
          <img src={avatarUrl || "https://cdn-icons-png.flaticon.com/512/847/847969.png"} alt={`Avatar de ${perfil.nombreEstudiante}`} className={styles.avatar} />
        )}
        <button
          className={styles.editIconContainer}
          onClick={() => {
            setModalContent(<StudentAvatarEditor idPerfil={perfil.id} />);
            setModalTitle("Personalizar avatar");
            setShowModal(true);
          }}
        >
          <FontAwesomeIcon icon="fa fa-pen-to-square" />
        </button>
      </section>

      <section>
        <h3>Nombre</h3>
        <p>{perfil.nombreEstudiante}</p>
        <button className={styles.editIconContainer}>
          <FontAwesomeIcon icon="fa fa-pen-to-square" />
        </button>
      </section>

      <section>
        <h3>Resumen</h3>
        <div className={styles.resumen}>
          <div>
            <p>
              <FontAwesomeIcon icon="fa fa-coins" /> {perfil.monedas}
            </p>
            <p>Monedas disponibles</p>
          </div>
          <div>
            <p>
              <FontAwesomeIcon icon="fa fa-award" /> {perfil.medallas.length}
            </p>
            <p>Medallas obtenidas</p>
          </div>
          <div>
            <p>
              <FontAwesomeIcon icon="fa fa-bullseye" /> {perfil.metaCalificacion}
            </p>
            <p>Meta personal</p>
          </div>
        </div>
        <div className={styles.progressBarContainer}>
          <p>Progreso hacia la próxima calificación</p>
          {!isLoadingBarra && barraProgreso && (
            <div className={styles.progressBar}>
              {Array.from({ length: barraProgreso.calificacionMaxima }, (_, index) => {
                const numero = index + barraProgreso.calificacionMinima;
                const alcanzado = numero <= barraProgreso.calificacionActual;
                const esMeta = numero === perfil.metaCalificacion;

                return (
                  <label key={`progreso-${numero}`} htmlFor={`progreso-${numero}`} className={`${styles.label} ${alcanzado ? styles.alcanzado : styles.noAlcanzado}`}>
                    <input type="radio" id={`progreso-${numero}`} name="progreso" checked={esMeta} onChange={() => setMeta(numero)} />
                    {esMeta && <FontAwesomeIcon icon="fa fa-bullseye" className={styles.icono} />}
                  </label>
                );
              })}
            </div>
          )}
        </div>
      </section>

      <section>
        <h3>Inventario de Medallas</h3>
        <div className={styles.medallasGrid}>
          {perfil.medallas.length === 0 ? (
            <p>No tienes medallas aún.</p>
          ) : (
            perfil.medallas.map((medalla) => (
              <MedalCard
                key={medalla.medallaId + medalla.nombre}
                nombre={medalla.nombre}
                descripcion={medalla.descripcion}
                urlImagen={medalla.urlImagen || "https://cdn-icons-png.flaticon.com/512/2583/2583341.png"} // reemplazalo si no tenés imagen
                cantidadMedallasBrinda={medalla.cantidad}
                esAsignacionMutua={false}
                onEdit={() => {}}
                showEditOption={false}
              />
            ))
          )}
        </div>
      </section>

      <section>
        <h3>Recompensas</h3>
        {isLoadingRecompensas ? (
          <BarLoader />
        ) : (
          <div className={styles.recompensasGrid}>
            {recompensas?.length > 0 ? recompensas.map((reward) => <RewardItem key={reward.nombre + reward.id} reward={reward} redeemed={true} />) : <p>No tienes recompensas aún.</p>}
          </div>
        )}
      </section>
    </div>
  );
};

GroupProfileView.propTypes = {
  perfil: PropTypes.shape({
    id: PropTypes.number.isRequired,
    avatarGrupoId: PropTypes.number.isRequired,
    enlaceAvatar: PropTypes.string.isRequired,
    metaCalificacion: PropTypes.number.isRequired,
    estudianteId: PropTypes.string.isRequired,
    nombreEstudiante: PropTypes.string.isRequired,
    monedas: PropTypes.number.isRequired,
    grupoId: PropTypes.number.isRequired,
    nombreGrupo: PropTypes.string.isRequired,
    medallas: PropTypes.arrayOf(
      PropTypes.shape({
        medallaId: PropTypes.number.isRequired,
        nombre: PropTypes.string.isRequired,
        icono: PropTypes.string.isRequired,
        descripcion: PropTypes.string.isRequired,
        monedasOtorgadas: PropTypes.number.isRequired,
        cantidad: PropTypes.number.isRequired,
      })
    ).isRequired,
  }).isRequired,
  isLoading: PropTypes.bool.isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
  setShowModal: PropTypes.func.isRequired,
};

export default GroupProfileView;

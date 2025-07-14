// GroupProfileView.jsx
import React from "react";
import PropTypes from "prop-types";
import BarLoader from "../../../generics/BarLoader";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useRecompensasPerfil, useImagenPerfil, useBarraProgresoPerfil } from "../../hooks/useStudentMutation";
import RewardItem from "../RewardItem";

import styles from "./GroupProfileView.module.css";

const GroupProfileView = ({ perfil, isLoading }) => {
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasPerfil(perfil?.id);
  const { data: imagenPerfil, isLoading: isLoadingImagen } = useImagenPerfil(perfil?.id);
  const { data: barraProgreso, isLoading: isLoadingBarra } = useBarraProgresoPerfil(perfil?.id);

  console.log(barraProgreso);

  const avatarUrl = imagenPerfil?.urlCompleta;

  return isLoading ? (
    <BarLoader />
  ) : (
    <div className={styles.container}>
      <section>
        <h3>Avatar</h3>
        {isLoadingImagen ? (
          <BarLoader />
        ) : (
          <img
            src={avatarUrl || "https://www.researchgate.net/publication/341068087/figure/fig3/AS:11431281104224771@1669979151092/Figura-2-Avatar-que-aparece-por-defecto-en-Facebook.png"}
            alt={`Avatar de ${perfil.nombreEstudiante}`}
            className={styles.avatar}
          />
        )}
      </section>

      <section>
        <h3>Nombre</h3>
        <p>{perfil.nombreEstudiante}</p>
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
          <div className={styles.progressBar}>
            <label htmlFor="progreso-1">
              <input type="radio" id="progreso-1" name="progreso" />
            </label>
            <label htmlFor="progreso-2">
              <input type="radio" id="progreso-2" name="progreso" />
            </label>
            <label htmlFor="progreso-3">
              <input type="radio" id="progreso-3" name="progreso" />
            </label>
            <label htmlFor="progreso-4">
              <input type="radio" id="progreso-4" name="progreso" />
            </label>
            <label htmlFor="progreso-5">
              <input type="radio" id="progreso-5" name="progreso" />
            </label>
            <label htmlFor="progreso-6">
              <input type="radio" id="progreso-6" name="progreso" />
            </label>
            <label htmlFor="progreso-7">
              <input type="radio" id="progreso-7" name="progreso" />
            </label>
          </div>
        </div>
      </section>

      <section>
        <h3>Inventario de Medallas</h3>
        <div className={styles.medallasGrid}>
          {perfil.medallas.length === 0 ? (
            <p>No tienes medallas aún.</p>
          ) : (
            perfil.medallas.map((medalla) => (
              <div key={medalla.medallaId} className={styles.medallaItem}>
                <FontAwesomeIcon icon={["fa", medalla.icono]} size="2x" />
                <p>{medalla.nombre}</p>
                <small>{medalla.descripcion}</small>
                <span>x{medalla.cantidad}</span>
              </div>
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
            {recompensas?.length > 0 ? recompensas.map((reward) => <RewardItem key={reward.nombre} reward={reward} redeemed={true} />) : <p>No tienes recompensas aún.</p>}
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
};

export default GroupProfileView;

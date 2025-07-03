import React from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { BarLoader } from "react-spinners";
import { useRecompensasPerfil } from "../../hooks/useStudentMutation";
import RewardItem from "../teacher/RewardItem";

import styles from "./GroupProfileView.module.css";

const GroupProfileView = ({ perfil, isLoading }) => {
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasPerfil(perfil?.id);

  console.log(perfil);

  return isLoading ? (
    <div className={styles.barLoaderContainer}>
      <BarLoader color="var(--blanco-secundario)" size={10} />
    </div>
  ) : (
    <div className={styles.container}>
      <section>
        <h3>Avatar</h3>
        <img
          src="https://www.researchgate.net/publication/341068087/figure/fig3/AS:11431281104224771@1669979151092/Figura-2-Avatar-que-aparece-por-defecto-en-Facebook.png"
          alt={`Avatar de ${perfil.nombreEstudiante}`}
        />
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
        <div>
          <p>
            <FontAwesomeIcon icon="fa fa-bullseye" /> {perfil.metaCalificacion}
          </p>
          <p>Meta personal</p>
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
          <div className={styles.barLoaderContainer}>
            <BarLoader color="var(--blanco-secundario)" size={10} />
          </div>
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

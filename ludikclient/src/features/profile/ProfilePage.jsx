import React from "react";
import { useSelector } from "react-redux";
import { selectUserRole } from "../auth/hooks/userSlice";
import styles from "./ProfilePage.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { usePerfilUsuario } from "./hooks/useUserProfileMutation";
import BarLoader from "../generics/BarLoader";

const ProfilePage = () => {
  const role = useSelector(selectUserRole);
  const isProfesor = role === "Profesor";
  const { data: usuario, isLoading, error } = usePerfilUsuario();

  console.log(usuario);

  if (isLoading) return <BarLoader />;
  if (error) return <p>Error al cargar datos.</p>;

  return (
    <main className={styles.gestionDePerfil}>
      <section className={styles.userInfo}>
        <div className={styles.userBox}>
          <div className={styles.userBoxContent}>
            <p className={styles.userText}>{usuario.userName}</p>
            <span className={styles.userLabel}>NOMBRE DE USUARIO</span>
          </div>
          {/* <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" className={styles.editIcon} /> */}
        </div>

        {isProfesor && (
          <div className={styles.userBox}>
            <div className={styles.userBoxContent}>
              <p className={styles.userText}>{usuario.correo}</p>
              <span className={styles.userLabel}>CORREO ELECTRONICO</span>
            </div>
            {/* <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" className={styles.editIcon} /> */}
          </div>
        )}

        <div className={styles.userBox}>
          <div className={styles.userBoxContent}>
            <p className={styles.userText}>{usuario.roles[0]}</p>
            <span className={styles.userLabel}>ROL</span>
          </div>
          {/* <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" className={styles.editIcon} /> */}
        </div>

        <div className={styles.userBox}>
          <div className={styles.userBoxContent}>
            <p className={styles.userText}>************</p>
            <span className={styles.userLabel}>CONTRASEÑA</span>
          </div>
          <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" className={styles.editIcon} />
        </div>
      </section>
    </main>
  );
};

export default ProfilePage;

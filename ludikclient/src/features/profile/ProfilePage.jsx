import React from "react";
import styles from "./ProfilePage.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const ProfilePage = () => {
  return (
    <main className={styles.gestionDePerfil}>
      <section className={styles.userInfo}>
        <div className={styles.userBox}>
          <div className={styles.userBoxContent}>
            <p className={styles.userText}>Lucas Giusiano</p>
            <span className={styles.userLabel}>NOMBRE</span>
          </div>
          <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" className={styles.editIcon} />
        </div>

        <div className={styles.userBox}>
          <div className={styles.userBoxContent}>
            <p className={styles.userText}>uncorreo@gmail.com</p>
            <span className={styles.userLabel}>CORREO ELECTRONICO</span>
          </div>
          <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" className={styles.editIcon} />
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

import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"; // Importar FontAwesomeIcon
import styles from "./FloatingButton.module.css";

const FloatingButton = () => {
  const [showOptions, setShowOptions] = useState(false);

  const toggleOptions = () => {
    setShowOptions(!showOptions);
  };

  return (
    <div className={styles.floatingButtonContainer}>
      <button className={styles.floatingButton} onClick={toggleOptions}>
        <FontAwesomeIcon icon="fa-solid fa-bars" /> {/* Ícono de "fa-bars" */}
      </button>

      <div className={`${styles.optionsMenu} ${showOptions ? styles.show : ""}`}>
        <ul>
          <li>
            <FontAwesomeIcon icon="fa-solid fa-user" /> Ver perfil
          </li>
          <li>
            <FontAwesomeIcon icon="fa-solid fa-users" /> Ver grupos
          </li>
          <li>
            <FontAwesomeIcon icon="fa-solid fa-cogs" /> Configuraciones
          </li>
        </ul>
      </div>
    </div>
  );
};

export default FloatingButton;

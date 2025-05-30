import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./HeaderMenu.module.css";
import { cerrarSesion } from "../../auth/auth.js";

const HeaderMenu = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [showOptions, setShowOptions] = useState(false);

  const toggleOptions = () => {
    setShowOptions(!showOptions);
  };

  const closeMenu = () => {
    setShowOptions(false);
  };

  const handleLogout = () => {
    cerrarSesion(dispatch);
    navigate("/login"); // Redirige al login
  };

  return (
    <>
      <button className={styles.menuButton} onClick={toggleOptions}>
        <FontAwesomeIcon icon="fa-solid fa-bars" />
      </button>

      {/* Fondo semitransparente */}
      <div className={`${styles.overlay} ${showOptions ? styles.show : ""}`} onClick={toggleOptions} />

      <div className={`${styles.sidebarMenu} ${showOptions ? styles.show : ""}`}>
        <ul className=".sidebarOptions">
          <div>
            <li>
              <FontAwesomeIcon icon="fa-solid fa-user" /> Ver perfil
            </li>
            <li>
              <FontAwesomeIcon icon="fa-solid fa-users" /> Ver grupos
            </li>
            <li>
              <FontAwesomeIcon icon="fa-solid fa-cogs" /> Configuraciones
            </li>
          </div>
          <div>
            <li onClick={handleLogout}>
              <FontAwesomeIcon icon="fa-solid fa-arrow-right-from-bracket" /> Cerrar Sesion
            </li>
          </div>
        </ul>
      </div>
    </>
  );
};

export default HeaderMenu;

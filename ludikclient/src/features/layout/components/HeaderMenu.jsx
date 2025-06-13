import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate, useLocation } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./HeaderMenu.module.css";
import { cerrarSesion } from "../../auth/hooks/auth.js";

const HeaderMenu = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation(); // nuevo

  const [showOptions, setShowOptions] = useState(false);

  const toggleOptions = () => {
    setShowOptions(!showOptions);
  };

  const handleLogout = () => {
    cerrarSesion(dispatch);
    goTo("/login");
  };

  const goTo = (path) => {
    navigate(path);
    setShowOptions(false);
  };

  // Helper para marcar el ítem activo
  const isActive = (path) => location.pathname === path;

  return (
    <>
      <button className={styles.menuButton} onClick={toggleOptions}>
        <FontAwesomeIcon icon="fa-solid fa-bars" />
      </button>

      <div className={`${styles.overlay} ${showOptions ? styles.show : ""}`} onClick={toggleOptions} />

      <div className={`${styles.sidebarMenu} ${showOptions ? styles.show : ""}`}>
        <ul className="sidebarOptions">
          <div>
            <li onClick={() => goTo("/profile")} className={isActive("/profile") ? styles.activeProfile : ""}>
              <FontAwesomeIcon icon="fa-solid fa-user" size="lg" /> Perfil
            </li>

            <li onClick={() => goTo("/groups")} className={isActive("/groups") ? styles.activeGroups : ""}>
              <FontAwesomeIcon icon="fa-solid fa-users" size="lg" /> Grupos
            </li>

            <li onClick={() => goTo("/medals")} className={isActive("/medals") ? styles.activeMedallas : ""}>
              <FontAwesomeIcon icon="fa-solid fa-award" size="lg" /> Medallas
            </li>

            <li onClick={() => goTo("/rubricas")} className={isActive("/rubricas") ? styles.activeRubricas : ""}>
              <FontAwesomeIcon icon="fa-solid fa-clipboard-list" size="lg" /> Rubricas
            </li>

            <li onClick={() => goTo("/rankings")} className={isActive("/rankings") ? styles.activeRankings : ""}>
              <FontAwesomeIcon icon="fa-solid fa-ranking-star" size="lg" /> Rankings
            </li>

            <li onClick={() => goTo("/configuraciones")} className={isActive("/configuraciones") ? styles.activeConfiguraciones : ""}>
              <FontAwesomeIcon icon="fa-solid fa-cogs" size="lg" /> Configuraciones
            </li>
          </div>
          <div className={styles.cerrarSesion}>
            <li onClick={handleLogout}>
              <FontAwesomeIcon icon="fa-solid fa-arrow-right-from-bracket" size="lg" /> Cerrar Sesion
            </li>
          </div>
        </ul>
      </div>
    </>
  );
};

export default HeaderMenu;

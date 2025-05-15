// src/components/Layout.js
import React from "react";
import { Outlet, useNavigate, useLocation } from "react-router-dom";
import styles from "./Layout.module.css";
import FloatingButton from "./FloatingButton";

function Layout() {
  const navigate = useNavigate();
  const location = useLocation(); // Usamos useLocation para obtener la ruta actual

  // Verificamos si la ruta actual es "/login" para mostrar "Registrarse"
  const isLoginPage = location.pathname === "/login";

  return (
    <div className={styles.layoutContainer}>
      <header className={styles.header}>
        <div className={styles.logoArea}>
          <img
            src="./assets/logo.png"
            alt="Ludik Logo"
            className={styles.logoImage}
            onClick={() => navigate("/")} // Redirige al home
            style={{ cursor: "pointer" }} // Cambia el cursor a mano para indicar que es clickeable
          />
        </div>
        <button className="button" onClick={() => navigate(isLoginPage ? "/signup" : "/login")}>
          {isLoginPage ? "Registrarse" : "Iniciar sesión"}
        </button>
      </header>

      <FloatingButton />
      <Outlet />
    </div>
  );
}

export default Layout;

import React from "react";
import { Outlet, useNavigate, useLocation } from "react-router-dom";
import styles from "./Layout.module.css";
import FloatingButton from "./components/HeaderMenu";

function Layout() {
  const navigate = useNavigate();
  const location = useLocation();

  const isLoginPage = location.pathname === "/login";

  // Detectar si hay usuario logueado con sessionStorage y userData
  const userDataString = sessionStorage.getItem("userData");
  let isLoggedIn = false;
  if (userDataString) {
    try {
      const userData = JSON.parse(userDataString);
      if (userData.token && userData.email) {
        isLoggedIn = true;
      }
    } catch (error) {
      console.error("Error parsing userData from sessionStorage", error);
    }
  }

  return (
    <div className={styles.layoutContainer}>
      <header className={styles.header}>
        <div className={styles.logoArea}>
          <img
            src="./assets/logo.png"
            alt="Ludik Logo"
            className={styles.logoImage}
            onClick={() => navigate("/")}
            style={{ cursor: "pointer" }}
          />
        </div>

        {isLoggedIn && <FloatingButton />}
      </header>

      <Outlet />
    </div>
  );
}

export default Layout;

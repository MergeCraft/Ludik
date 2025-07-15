// src/features/layout/Layout.jsx
import React from "react";
import { Outlet, useNavigate } from "react-router-dom";
import styles from "./Layout.module.css";
import HeaderMenu from "./components/HeaderMenu";
import logo from "../../assets/logo.png";
import { useSelector } from "react-redux";
import { selectIsAuthenticated } from "../auth/hooks/userSlice";

function Layout() {
  const navigate = useNavigate();

  const isLoggedIn = useSelector(selectIsAuthenticated);

  return (
    <div className={styles.layoutContainer}>
      <header className={styles.header}>
        <div className={styles.logoArea}>
          <img src={logo} alt="Ludik Logo" className={styles.logoImage} onClick={() => navigate("/")} style={{ cursor: "pointer" }} />
        </div>

        {isLoggedIn && <HeaderMenu />}
      </header>

      <Outlet />
    </div>
  );
}

export default Layout;

import React from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import LoginForm from "./components/LoginForm";
import SignupForm from "./components/SignupForms";
import styles from "./AuthPage.module.css";

const AuthPage = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const isLogin = location.pathname === "/login";

  return (
    <main className={styles.container}>
      <div className={styles.cerrarIcono} onClick={() => navigate("/")}>
        <FontAwesomeIcon icon="fa-solid fa-xmark" size="xl" />
      </div>

      <div className={styles.logoArea}>
        <img src="./assets/logo.png" alt="Ludik Logo" className={styles.logoImage} />
      </div>

      <div className={styles.formBox}>{isLogin ? <LoginForm /> : <SignupForm />}</div>
    </main>
  );
};

export default AuthPage;

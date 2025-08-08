// components/LoginForm.js
import React, { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useDispatch } from "react-redux";
import { useLogin } from "../hooks/useAuthMutation.js";
import { logoutReset } from "../hooks/userSlice.js";
import * as Toast from "../../../lib/toastify.js";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "../AuthPage.module.css";

const LoginForm = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const message = location.state?.message;
  const path = location.pathname;

  const dispatch = useDispatch();

  const { mutateAsync: login } = useLogin();

  const [recordar, setRecordar] = useState(false);
  const [usuario, setUsuario] = useState("");
  const [contrasena, setContrasena] = useState("");
  const [verContrasena, setVerContrasena] = useState(false);

  const handleChange = (e) => {
    const { name, value, checked } = e.target;
    if (name === "usuario") setUsuario(value);
    if (name === "contrasena") setContrasena(value);
    if (name === "recuerdame") setRecordar(checked);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await login({ usuario, contrasena });
    } catch (error) {
      console.warn("Error: ", error.message);
    }
  };

  useEffect(() => {
    if (message) {
      Toast.notificarError(message);
    }
  }, [message]);

  useEffect(() => {
    dispatch(logoutReset());
  }, []);

  return (
    <form className={styles.formulario} onSubmit={handleSubmit}>
      {path == "/" && (
        <>
          <h2>¡Comenza ahora!</h2>
          <hr />
        </>
      )}

      {/* campos de usuario y contraseña */}
      <div className={styles.campo}>
        <label htmlFor="usuario" className={styles.etiqueta}>
          Usuario
        </label>
        <input type="text" id="usuario" name="usuario" className={styles.input} value={usuario} onChange={handleChange} />
      </div>

      <div className={styles.campo} style={{ position: "relative" }}>
        <label htmlFor="contrasena" className={styles.etiqueta}>
          Contraseña
        </label>
        <input type={verContrasena ? "text" : "password"} id="contrasena" name="contrasena" className={styles.input} value={contrasena} onChange={handleChange} />
        <button type="button" onClick={() => setVerContrasena((prev) => !prev)} className={styles.verContrasena} aria-label={verContrasena ? "Ocultar contraseña" : "Mostrar contraseña"}>
          <FontAwesomeIcon icon={verContrasena ? "eye-slash" : "eye"} size="lg" />
        </button>
      </div>

      <div className={styles.recordar}>
        <label className="switch">
          <input type="checkbox" id="recuerdame" name="recuerdame" checked={recordar} onChange={handleChange} />
          <span className="slider"></span>
        </label>
        <label htmlFor="recuerdame" className={`${styles.etiqueta} ${styles.recuerdame}`}>
          Recuérdame
        </label>
      </div>

      <button type="submit" className="button">
        Iniciar sesión
      </button>

      <div className={`${styles.acciones} ${styles.accionesLogin}`}>
        <a href="/passwordRecovery" className={styles.enlace}>
          ¿Olvidaste tu contraseña?
        </a>
        <hr />
        <a
          onClick={(e) => {
            e.preventDefault();
            navigate("/signup");
          }}
          className={`button-secondary ${styles.nuevaCuenta}`}
          href="/signup"
        >
          Crear nueva cuenta
        </a>
      </div>
    </form>
  );
};

export default LoginForm;

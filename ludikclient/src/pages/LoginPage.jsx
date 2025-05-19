import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { iniciarSesion } from "../features/auth/auth.js";
import * as Toast from "../lib/toastify.js";
import styles from "./LoginPage.module.css";

export const LoginPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [recordar, setRecordar] = useState(false);
  const [usuario, setUsuario] = useState("");
  const [contrasena, setContrasena] = useState("");

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    if (name === "usuario") setUsuario(value);
    if (name === "contrasena") setContrasena(value);
    if (name === "recuerdame") setRecordar(checked);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await iniciarSesion({ usuario, contrasena }, dispatch);
      navigate("/");
    } catch (error) {
      console.error("Se ejecutó notificarError con:", error.message);
      Toast.notificarError(error.message);
    }
  };

  return (
    <main className={styles.container}>
      <div className={styles.formBox}>
        <div className={styles.logoArea}>
          <img src="./assets/logo.png" alt="Ludik Logo" className={styles.logoImage} />
        </div>

        <form className={styles.formulario} onSubmit={handleSubmit}>
          <div className={styles.campo}>
            <label htmlFor="usuario" className={styles.etiqueta}>
              Usuario
            </label>
            <input type="text" id="usuario" name="usuario" className={styles.input} value={usuario} onChange={handleChange} />
          </div>

          <div className={styles.campo}>
            <label htmlFor="contrasena" className={styles.etiqueta}>
              Contraseña
            </label>
            <input type="password" id="contrasena" name="contrasena" className={styles.input} value={contrasena} onChange={handleChange} />
          </div>

          <div className={styles.recordar}>
            <label className={"switch"}>
              <input type="checkbox" id="recuerdame" name="recuerdame" checked={recordar} onChange={handleChange} />
              <span className={"slider"}></span>
            </label>
            <label htmlFor="recuerdame" className={styles.etiqueta}>
              Recuérdame
            </label>
          </div>

          <button type="submit" className={"button"}>
            Iniciar sesión
          </button>

          <div className={styles.acciones}>
            <a href="/restaurar" className={styles.enlace}>
              Restaurar contraseña
            </a>
            <a href="/signup" className={styles.enlace}>
              Crear nueva cuenta
            </a>
          </div>
        </form>
      </div>
    </main>
  );
};

export default LoginPage;

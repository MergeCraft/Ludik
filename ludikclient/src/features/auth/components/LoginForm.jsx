// components/LoginForm.js
import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { iniciarSesion } from "../auth.js";
import * as Toast from "../../../lib/toastify.js";
import styles from "../AuthPage.module.css";

const LoginForm = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [recordar, setRecordar] = useState(false);
  const [usuario, setUsuario] = useState("");
  const [contrasena, setContrasena] = useState("");

  const handleChange = (e) => {
    const { name, value, checked } = e.target;
    if (name === "usuario") setUsuario(value);
    if (name === "contrasena") setContrasena(value);
    if (name === "recuerdame") setRecordar(checked);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await iniciarSesion({ usuario, contrasena }, dispatch);
      setTimeout(() => {
        navigate("/studentGroups");
      }, 200);
    } catch (error) {
      Toast.notificarError(error.message);
    }
  };

  return (
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
        <label htmlFor="recuerdame" className={`${styles.etiqueta} ${styles.recuerdame}`}>
          Recuérdame
        </label>
      </div>

      <button type="submit" className={"button"}>
        Iniciar sesión
      </button>

      <div className={`${styles.acciones} ${styles.accionesLogin}`}>
        <a href="/restaurar" className={styles.enlace}>
          ¿Olvidaste tu contraseña?
        </a>
        <hr />
        <a
          onClick={(e) => {
            e.preventDefault();
            setTimeout(() => navigate("/signup"), 200);
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

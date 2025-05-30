import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import classNames from "classnames";
import styles from "./SignupPage.module.css";
import { registrarse } from "./auth.js";
import * as Toast from "../../lib/toastify.js";

export const SignupPage = () => {
  const navigate = useNavigate();
  const [isProfesor, setIsProfesor] = useState(false);

  const [profesorData, setProfesorData] = useState({
    usuario: "",
    correo: "",
    nombre: "",
    apellido: "",
    contrasena: "",
    repetirContrasena: "",
  });

  const [alumnoData, setAlumnoData] = useState({
    usuario: "",
    nombre: "",
    apellido: "",
    contrasena: "",
    repetirContrasena: "",
  });

  const handleSwitchChange = () => {
    setIsProfesor(!isProfesor);
  };

  const handleChangeProfesor = (e) => {
    const { name, value } = e.target;
    setProfesorData((prev) => ({ ...prev, [name]: value }));
  };

  const handleChangeAlumno = (e) => {
    const { name, value } = e.target;
    setAlumnoData((prev) => ({ ...prev, [name]: value }));
  };

  // VALIDADORES

  // 1. Correo (local-part solo a-z, 0-9, ., _, -; no ascii fuera)
  const validarCorreo = (correo) => {
    if (!correo) return;
    // regex para local-part (antes de @)
    const emailRegex = /^[a-z0-9._-]+@[a-z0-9.-]+\.[a-z]{2,}$/i;
    if (!emailRegex.test(correo)) {
      Toast.notificarWarning("Correo inválido. Solo letras, números, ., _ y - antes de @, sin caracteres especiales no ASCII.");
    }
  };

  // 2. Contraseña: min 8 caracteres, al menos 1 mayus, 1 minus, 1 número, 1 especial
  const validarContrasena = (contrasena) => {
    if (!contrasena) return;
    const passRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/;
    if (!passRegex.test(contrasena)) {
      Toast.notificarWarning("La contraseña debe tener mínimo 8 caracteres, incluyendo mayúscula, minúscula, número y carácter especial.");
    }
  };

  // 3. Nombre y Apellido: entre 3 y 20 caracteres
  const validarNombreApellido = (valor, campo) => {
    if (!valor) return;
    if (valor.length < 3 || valor.length > 20) {
      Toast.notificarWarning(`${campo} debe tener entre 3 y 20 caracteres.`);
    }
  };

  const validarContrasenasCoinciden = (contrasena, repetirContrasena) => {
    if (!contrasena || !repetirContrasena) return; // No mostrar si están vacíos
    if (contrasena !== repetirContrasena) {
      Toast.notificarWarning("Las contraseñas no coinciden.");
    }
  };

  const handleSubmit = async () => {
    try {
      if (!isProfesor) {
        const { usuario, correo, nombre, apellido, contrasena, repetirContrasena } = profesorData;
        if (contrasena !== repetirContrasena) {
          Toast.notificarError("Las contraseñas no coinciden.");
          return; // Bloquea el envío
        }

        // ... resto igual
        const data = {
          nombreUsuario: usuario,
          email: correo,
          nombre,
          apellido,
          contrasenia: contrasena,
        };

        await registrarse(data, "profesor");
      } else {
        const { usuario, nombre, apellido, contrasena, repetirContrasena } = alumnoData;
        if (contrasena !== repetirContrasena) {
          Toast.notificarError("Las contraseñas no coinciden.");
          return;
        }

        const data = {
          nombreUsuario: usuario,
          nombre,
          apellido,
          contrasenia: contrasena,
        };

        await registrarse(data, "alumno");
      }
      Toast.notificarExito("Registro exitoso");
      navigate("/login");
    } catch (error) {
      Toast.notificarError(error.message);
    }
  };

  return (
    <main className={styles.container}>
      <div className={styles.formBox}>
        <div className={styles.logoArea}>
          <img src="./assets/logo.png" alt="Ludik Logo" className={styles.logoImage} />
        </div>

        <div className={styles.switchContainer}>
          <label className={styles.switch}>
            <input type="checkbox" checked={isProfesor} onChange={handleSwitchChange} className={styles.switchInput} />
            <span className={styles.slider}>
              <span className={classNames(styles.switchLabel, styles.profesor)}>Profesor</span>
              <span className={classNames(styles.switchLabel, styles.alumno)}>Alumno</span>
            </span>
          </label>
        </div>

        <div className={styles.formSlider}>
          <div
            className={styles.formInner}
            style={{
              transform: isProfesor ? "translateX(-50%)" : "translateX(0)",
              height: isProfesor ? "425px" : "500px",
            }}
          >
            {/* Formulario Profesor */}
            <form className={classNames(styles.formulario, { [styles.oculto]: isProfesor })} onSubmit={(e) => e.preventDefault()}>
              <div className={styles.campo}>
                <label htmlFor="usuario_profesor" className={styles.etiqueta}>
                  Nombre de usuario
                </label>
                <input
                  type="text"
                  id="usuario_profesor"
                  name="usuario"
                  className={styles.input}
                  value={profesorData.usuario}
                  onChange={handleChangeProfesor}
                  onBlur={() => validarNombreApellido(profesorData.usuario, "Nombre de usuario del profesor")}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="correo_profesor" className={styles.etiqueta}>
                  Correo electrónico
                </label>
                <input
                  type="email"
                  id="correo_profesor"
                  name="correo"
                  className={styles.input}
                  value={profesorData.correo}
                  onChange={handleChangeProfesor}
                  onBlur={() => validarCorreo(profesorData.correo)}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="nombre_profesor" className={styles.etiqueta}>
                  Nombre
                </label>
                <input
                  type="text"
                  id="nombre_profesor"
                  name="nombre"
                  className={styles.input}
                  value={profesorData.nombre}
                  onChange={handleChangeProfesor}
                  onBlur={() => validarNombreApellido(profesorData.nombre, "Nombre del profesor")}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="apellido_profesor" className={styles.etiqueta}>
                  Apellido
                </label>
                <input
                  type="text"
                  id="apellido_profesor"
                  name="apellido"
                  className={styles.input}
                  value={profesorData.apellido}
                  onChange={handleChangeProfesor}
                  onBlur={() => validarNombreApellido(profesorData.apellido, "Apellido del profesor")}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="contrasena_profesor" className={styles.etiqueta}>
                  Contraseña
                </label>
                <input
                  type="password"
                  id="contrasena_profesor"
                  name="contrasena"
                  className={styles.input}
                  value={profesorData.contrasena}
                  onChange={handleChangeProfesor}
                  onBlur={() => validarContrasena(profesorData.contrasena)}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="repetirContrasena_profesor" className={styles.etiqueta}>
                  Repite la contraseña
                </label>
                <input
                  type="password"
                  id="repetirContrasena_profesor"
                  name="repetirContrasena"
                  className={styles.input}
                  value={profesorData.repetirContrasena}
                  onChange={handleChangeProfesor}
                  onBlur={() => validarContrasenasCoinciden(profesorData.contrasena, profesorData.repetirContrasena)}
                />
              </div>
            </form>

            {/* Formulario Alumno */}
            <form className={classNames(styles.formulario, { [styles.oculto]: !isProfesor })} onSubmit={(e) => e.preventDefault()}>
              <div className={styles.campo}>
                <label htmlFor="usuario_alumno" className={styles.etiqueta}>
                  Nombre de usuario
                </label>
                <input
                  type="text"
                  id="usuario_alumno"
                  name="usuario"
                  className={styles.input}
                  value={alumnoData.usuario}
                  onChange={handleChangeAlumno}
                  onBlur={() => validarNombreApellido(alumnoData.usuario, "Nombre de usuario del alumno")}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="nombre_alumno" className={styles.etiqueta}>
                  Nombre
                </label>
                <input
                  type="text"
                  id="nombre_alumno"
                  name="nombre"
                  className={styles.input}
                  value={alumnoData.nombre}
                  onChange={handleChangeAlumno}
                  onBlur={() => validarNombreApellido(alumnoData.nombre, "Nombre del alumno")}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="apellido_alumno" className={styles.etiqueta}>
                  Apellido
                </label>
                <input
                  type="text"
                  id="apellido_alumno"
                  name="apellido"
                  className={styles.input}
                  value={alumnoData.apellido}
                  onChange={handleChangeAlumno}
                  onBlur={() => validarNombreApellido(alumnoData.apellido, "Apellido del alumno")}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="contrasena_alumno" className={styles.etiqueta}>
                  Contraseña
                </label>
                <input
                  type="password"
                  id="contrasena_alumno"
                  name="contrasena"
                  className={styles.input}
                  value={alumnoData.contrasena}
                  onChange={handleChangeAlumno}
                  onBlur={() => validarContrasena(alumnoData.contrasena)}
                />
              </div>

              <div className={styles.campo}>
                <label htmlFor="repetirContrasena_alumno" className={styles.etiqueta}>
                  Repite la contraseña
                </label>
                <input
                  type="password"
                  id="repetirContrasena_alumno"
                  name="repetirContrasena"
                  className={styles.input}
                  value={alumnoData.repetirContrasena}
                  onChange={handleChangeAlumno}
                  onBlur={() => validarContrasenasCoinciden(alumnoData.contrasena, alumnoData.repetirContrasena)}
                />
              </div>
            </form>
          </div>
        </div>

        <button type="button" onClick={handleSubmit} className={classNames(styles.botonRegistro, "button")}>
          Registrarse
        </button>

        <div className={styles.acciones}>
          <a href="/login" className={styles.enlace}>
            Ya tengo una cuenta
          </a>
        </div>
      </div>
    </main>
  );
};

export default SignupPage;

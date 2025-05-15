import React, { useState } from "react";
import classNames from "classnames";
import styles from "./SignupPage.module.css";

export const SignupPage = () => {
  const [isProfesor, setIsProfesor] = useState(false);

  const handleSwitchChange = () => {
    setIsProfesor(!isProfesor);
  };

  return (
    <main className={styles.container}>
      <div className={styles.formBox}>
        <div className={styles.logoArea}>
          <img src="./assets/logo.png" alt="Ludik Logo" className={styles.logoImage} />
        </div>

        {/* Switch con Profesor/Alumno */}
        <div className={styles.switchContainer}>
          <label className={styles.switch}>
            <input type="checkbox" checked={isProfesor} onChange={handleSwitchChange} className={styles.switchInput} />
            <span className={styles.slider}>
              <span className={classNames(styles.switchLabel, styles.profesor)}>Profesor</span>
              <span className={classNames(styles.switchLabel, styles.alumno)}>Alumno</span>
            </span>
          </label>
        </div>

        {/* Contenedor de formularios */}
        <div className={styles.formSlider}>
          <div
            className={styles.formInner}
            style={{
              transform: isProfesor ? "translateX(-50%)" : "translateX(0)",
              height: isProfesor ? "495px" : "415px",
            }}
          >
            {/* Formulario Profesor */}
            <form className={classNames(styles.formulario, { [styles.oculto]: isProfesor })}>
              <div className={styles.campo}>
                <label htmlFor="usuario" className={styles.etiqueta}>
                  Nombre de usuario
                </label>
                <input type="text" id="usuario" name="usuario" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="correo" className={styles.etiqueta}>
                  Correo electrónico
                </label>
                <input type="email" id="correo" name="correo" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="contrasena" className={styles.etiqueta}>
                  Contraseña
                </label>
                <input type="password" id="contrasena" name="contrasena" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="repetirContrasena" className={styles.etiqueta}>
                  Repite la contraseña
                </label>
                <input type="password" id="repetirContrasena" name="repetirContrasena" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="fechaNacimiento" className={styles.etiqueta}>
                  Fecha de nacimiento
                </label>
                <input type="date" id="fechaNacimiento" name="fechaNacimiento" className={styles.input} />
              </div>
            </form>

            {/* Formulario Alumno */}
            <form className={classNames(styles.formulario, { [styles.oculto]: !isProfesor })}>
              <div className={styles.campo}>
                <label htmlFor="usuario" className={styles.etiqueta}>
                  Nombre de usuario
                </label>
                <input type="text" id="usuario" name="usuario" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="nombre" className={styles.etiqueta}>
                  Nombre
                </label>
                <input type="text" id="nombre" name="nombre" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="apellido" className={styles.etiqueta}>
                  Apellido
                </label>
                <input type="text" id="apellido" name="apellido" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="contrasena" className={styles.etiqueta}>
                  Contraseña
                </label>
                <input type="password" id="contrasena" name="contrasena" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="repetirContrasena" className={styles.etiqueta}>
                  Repite la contraseña
                </label>
                <input type="password" id="repetirContrasena" name="repetirContrasena" className={styles.input} />
              </div>

              <div className={styles.campo}>
                <label htmlFor="fechaNacimiento" className={styles.etiqueta}>
                  Fecha de nacimiento
                </label>
                <input type="date" id="fechaNacimiento" name="fechaNacimiento" className={styles.input} />
              </div>
            </form>
          </div>
        </div>

        <button type="submit" className={classNames(styles.botonRegistro, "button")}>
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

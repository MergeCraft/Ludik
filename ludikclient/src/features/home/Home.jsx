// src/pages/Home.js
import React from "react";
import classNames from "classnames";
import { Outlet, useNavigate } from "react-router-dom";
import styles from "./Home.module.css";

function Home() {
  const navigate = useNavigate();
  return (
    <>
      <main className={styles.heroSection}>
        <div>
          <div className={classNames("fade-up")}>
            <h1 className={styles.heroTitle}>Gamifica el aprendizaje</h1>
            <p className={styles.heroSubtitle}>
              Una bitácora educativa diseñada para motivar, recompensar y facilitar el seguimiento de los estudiantes.
            </p>
          </div>

          <div className={classNames(styles.menuComenzar, "fade-in", "delay-2")}>
            <button
              className={classNames("button", styles.botonComienzo)}
              onClick={() => {
                setTimeout(() => navigate("/signup"), 200);
              }}
            >
              Comenzar ahora
            </button>

            <button
              className={classNames("button", styles.botonComienzo, styles.botonCuenta)}
              onClick={() => {
                setTimeout(() => navigate("/login"), 200);
              }}
            >
              Ya tengo una cuenta
            </button>
          </div>
        </div>
      </main>

      <section className={styles.featuresSection}>
        <div className={classNames(styles.feature, "delay-3", "fade-in")}>
          <h3>🎖️ Sistema de Medallas</h3>
          <p>Recompensa la participación y el esfuerzo de forma visual y motivadora.</p>
        </div>
        <div className={classNames(styles.feature, "delay-4", "fade-in")}>
          <h3>📊 Seguimiento Visual</h3>
          <p>Conoce tu progreso con barras personalizadas y tablas de equivalencia.</p>
        </div>
        <div className={classNames(styles.feature, "delay-5", "fade-in")}>
          <h3>👨‍🏫 Herramienta Docente</h3>
          <p>Diseñada para simplificar la gestión de grupos y evaluaciones.</p>
        </div>
      </section>
    </>
  );
}

export default Home;

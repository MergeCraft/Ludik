import React from "react";
import classNames from "classnames";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import styles from "./Home.module.css";
import SistemadeMedallas from "../../assets/SistemadeMedallas.png";
import SeguimientoVisual from "../../assets/SeguimientoVisual.png";
import HerramientaDocente from "../../assets/HerramientaDocente.png";
import imagenDevs from "../../assets/genericGroupImage.png";

const developers = [
  { name: "Renato Ríos", role: "Backend", photo: imagenDevs },
  { name: "Manuel Martinez", role: "Backend", photo: imagenDevs },
  { name: "Lucas Giusiano", role: "Frontend y UX/UI", photo: imagenDevs },
];

function Home() {
  const navigate = useNavigate();

  useEffect(() => {
    const elements = document.querySelectorAll(`.${styles.featureAlt}, .${styles.devCard}`);
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            entry.target.classList.add(styles.visible);
          } else {
            entry.target.classList.remove(styles.visible); // 👈 Se quita la clase al salir
          }
        });
      },
      { threshold: 0.4 }
    );

    elements.forEach((el) => observer.observe(el));
    return () => elements.forEach((el) => observer.unobserve(el));
  }, []);

  return (
    <>
      <main className={styles.heroSection}>
        <div>
          <div className="fade-up">
            <h1 className={styles.heroTitle}>Gamifica el aprendizaje</h1>
            <p className={styles.heroSubtitle}>Una bitácora educativa diseñada para motivar, recompensar y facilitar el seguimiento de los estudiantes.</p>
          </div>
          <div className={classNames(styles.menuComenzar, "fade-in", "delay-2")}>
            <button className={classNames("button", styles.botonComienzo)} onClick={() => setTimeout(() => navigate("/signup"), 200)}>
              Comenzar ahora
            </button>
            <button className={classNames("button", styles.botonComienzo, styles.buttonWhite)} onClick={() => setTimeout(() => navigate("/login"), 200)}>
              Ya tengo una cuenta
            </button>
          </div>
        </div>
      </main>
      <section className={styles.featureSectionAlt}>
        <div className={classNames(styles.featureAlt, styles.leftSlide)}>
          <div className={styles.featureText}>
            <h2>🎖️ Sistema de Medallas</h2>
            <p>
              El sistema de medallas permite a los docentes premiar a los estudiantes por su desempeño, esfuerzo, puntualidad o cualquier criterio definido por el profesor. Las medallas son totalmente
              configurables y pueden implicar recompensas como monedas virtuales. Esta funcionalidad refuerza positivamente la participación y motiva al alumno a superarse.
            </p>
          </div>
          <div className={styles.featureImage}>
            <img src={SistemadeMedallas} alt="Sistema de medallas" />
          </div>
        </div>
      </section>

      <section className={styles.featureSectionAlt}>
        <div className={classNames(styles.featureAlt, styles.rightSlide)}>
          <div className={styles.featureText}>
            <h2>📊 Seguimiento Visual del Progreso</h2>
            <p>
              Cada estudiante tiene una barra de progreso visible, metas personalizadas y tablas de equivalencia. Esto permite que tanto docentes como alumnos visualicen fácilmente el desempeño
              académico y el avance hacia objetivos concretos. Todo está diseñado para que el seguimiento sea intuitivo, visual y motivador.
            </p>
          </div>
          <div className={styles.featureImage}>
            <img src={SeguimientoVisual} alt="Seguimiento visual del progreso" />
          </div>
        </div>
      </section>

      <section className={styles.featureSectionAlt}>
        <div className={classNames(styles.featureAlt, styles.leftSlide)}>
          <div className={styles.featureText}>
            <h2>👨‍🏫 Herramienta Docente</h2>
            <p>
              Ludik está diseñada para simplificar la labor docente: gestión de grupos, asignación de actividades, definición de recompensas, carga de calificaciones, y visualización clara del
              rendimiento grupal. Todo en un entorno moderno, funcional y con herramientas de gamificación integradas.
            </p>
          </div>
          <div className={styles.featureImage}>
            <img src={HerramientaDocente} alt="Herramienta docente" />
          </div>
        </div>
      </section>

      <section className={styles.devsSection}>
        <div className={styles.devsContainer}>
          <h2>Conoce al equipo</h2>
          <div className={styles.devsGrid}>
            {developers.map((dev, i) => (
              <div key={i} className={classNames(styles.devCard, `delay-${i + 1}`)}>
                <img src={dev.photo} alt={dev.name} />
                <h4>{dev.name}</h4>
                <span>{dev.role}</span>
              </div>
            ))}
          </div>
        </div>
      </section>

      <footer className={styles.footer}>
        <div className={styles.footerContainer}>
          <h2>Contacto</h2>
          <form onSubmit={(e) => e.preventDefault()}>
            <label htmlFor="name">Nombre</label>
            <input id="name" name="name" type="text" required />

            <label htmlFor="email">Correo electrónico</label>
            <input id="email" name="email" type="email" required />

            <label htmlFor="message">Mensaje</label>
            <textarea id="message" name="message" rows="4" required />

            <button type="submit" className="button-secondary">
              Enviar
            </button>
          </form>
        </div>
      </footer>
    </>
  );
}

export default Home;

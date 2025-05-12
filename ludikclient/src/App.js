import React from "react";
import "./App.css";

function App() {
  return (
    <div className="landing-container">
      <header className="landing-header">
        <div className="logo-section">
          <img src="./assets/logo.png" alt="Ludik Logo" className="logo" />
        </div>
        <button className="button">Iniciar sesión</button>
      </header>

      <main className="hero-section">
        <div className="hero-section-faded">
          <h1 className="hero-title">Gamifica el aprendizaje</h1>
          <p className="hero-subtitle">
            Una bitácora educativa diseñada para motivar, recompensar y facilitar el seguimiento de los estudiantes.
          </p>
          <button className="button">Comenzar ahora</button>
        </div>
      </main>

      <section className="features-section">
        <div className="feature fade-in delay-1">
          <h3>🎖️ Sistema de Medallas</h3>
          <p>Recompensa la participación y el esfuerzo de forma visual y motivadora.</p>
        </div>
        <div className="feature fade-in delay-2">
          <h3>📊 Seguimiento Visual</h3>
          <p>Conoce tu progreso con barras personalizadas y tablas de equivalencia.</p>
        </div>
        <div className="feature fade-in delay-3">
          <h3>👨‍🏫 Herramienta Docente</h3>
          <p>Diseñada para simplificar la gestión de grupos y evaluaciones.</p>
        </div>
      </section>
    </div>
  );
}

export default App;

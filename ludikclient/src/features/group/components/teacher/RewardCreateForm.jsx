// RewardCreateForm.jsx
import React, { useState } from "react";
import styles from "./RewardCreateForm.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const iconOptions = [
  { label: "Estrella", value: "star" },
  { label: "Regalo", value: "gift" },
  { label: "Corazón", value: "heart" },
  { label: "Medalla", value: "medal" },
  { label: "Monedas", value: "coins" },
  { label: "Trofeo", value: "trophy" },
  { label: "Fuego", value: "fire" },
  { label: "Banderín", value: "flag" },

  // Nuevos íconos agregados según tu librería
  { label: "Corona", value: "crown" },
  { label: "Caja", value: "box" },
  { label: "Dado", value: "dice" },
  { label: "Varita mágica", value: "wand-magic-sparkles" },
  { label: "Pincel", value: "paint-brush" },
  { label: "Paleta de colores", value: "palette" },
  { label: "Lápiz", value: "pencil" },
  { label: "Pluma", value: "pen-nib" },
  { label: "Bombilla", value: "lightbulb" },
  { label: "Fútbol", value: "futbol" },
  { label: "Baloncesto", value: "basketball" },
  { label: "Vóley", value: "volleyball" },
  { label: "Corriendo", value: "running" },
  { label: "Bicicleta", value: "bicycle" },
  { label: "Libro", value: "book" },
  { label: "Birrete", value: "graduation-cap" },
  { label: "Pizarra", value: "chalkboard" },
  { label: "Cerebro", value: "brain" },
  { label: "Lupa", value: "magnifying-glass" },
  { label: "Cara sonriente", value: "face-smile" },
  { label: "Cara riendo", value: "face-laugh-beam" },
  { label: "Cara con estrellas", value: "face-grin-stars" },
  { label: "Pulgar arriba", value: "thumbs-up" },
  { label: "Aplauso", value: "hands-clapping" },
  { label: "Gema", value: "gem" },
  { label: "Diamante", value: "diamond" },
  { label: "Billete", value: "money-bill" },
  { label: "Billetera", value: "wallet" },
  { label: "Robot", value: "robot" },
  { label: "Cohete", value: "rocket" },
  { label: "Martillo", value: "hammer" },
  { label: "Fantasma", value: "ghost" },
  { label: "Dragón", value: "dragon" },
  { label: "Helado", value: "ice-cream" },
  { label: "Porción de pizza", value: "pizza-slice" },
  { label: "Mapa", value: "map" },
  { label: "Brújula", value: "compass" },
  { label: "Binoculares", value: "binoculars" },
  { label: "Avión", value: "plane" },
  { label: "Pastel", value: "cake-candles" },
];

const RewardCreateForm = () => {
  const [recompensa, setRecompensa] = useState({
    id: 0,
    nombre: "",
    imagen: "",
    precio: 0,
  });

  const [showIconPicker, setShowIconPicker] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRecompensa((prev) => ({
      ...prev,
      [name]: name === "precio" ? parseInt(value) || 0 : value,
    }));
  };

  const handleIconSelect = (value) => {
    setRecompensa((prev) => ({ ...prev, imagen: value }));
    setShowIconPicker(false);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(recompensa);
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Nombre de la recompensa
        <input type="text" name="nombre" placeholder="Ej: Alfajor" value={recompensa.nombre} onChange={handleChange} required />
      </label>

      <label>
        Ícono representativo
        <button type="button" className={`button ${styles.iconSelectButton}`} onClick={() => setShowIconPicker((prev) => !prev)}>
          {recompensa.imagen ? <FontAwesomeIcon icon={`fa-solid fa-${recompensa.imagen}`} size="xl" /> : "Seleccionar ícono"}
        </button>
        {showIconPicker && (
          <div className={styles.iconGrid}>
            {iconOptions.map((icon) => (
              <button key={icon.value} type="button" className={`${styles.iconOption} ${recompensa.imagen === icon.value ? styles.iconSelected : ""}`} onClick={() => handleIconSelect(icon.value)}>
                <FontAwesomeIcon icon={`fa-solid fa-${icon.value}`} size="xl" />
              </button>
            ))}
          </div>
        )}
      </label>

      <label>
        Precio (en monedas)
        <input type="number" name="precio" placeholder="Ej: 150" value={recompensa.precio} onChange={handleChange} min={0} required />
      </label>

      <div className={styles.acciones}>
        <button type="submit" className={`${styles.btnSubmit} button-secondary`}>
          Crear recompensa
        </button>
        {recompensa.id !== 0 && (
          <button className={`${styles.btnDelete} button-tertiary`}>
            <FontAwesomeIcon icon="fa-solid fa-trash" />
          </button>
        )}
      </div>
    </form>
  );
};

export default RewardCreateForm;

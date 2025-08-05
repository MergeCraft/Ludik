import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import styles from "./RewardCreateForm.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useCrearRecompensa, useEditarRecompensa, useEliminarRecompensa } from "../hooks/useRewardMutation";
import * as Toast from "../../../lib/toastify.js";

const iconOptions = [
  { label: "Estrella", value: "star" },
  { label: "Regalo", value: "gift" },
  { label: "Corazón", value: "heart" },
  { label: "Medalla", value: "medal" },
  { label: "Monedas", value: "coins" },
  { label: "Trofeo", value: "trophy" },
  { label: "Fuego", value: "fire" },
  { label: "Banderín", value: "flag" },
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

const extractIconName = (ruta) => {
  if (!ruta) return "";
  const match = ruta.match(/\/([^/]+)\.svg$/);
  return match ? match[1] : "";
};

const RewardCreateForm = ({ reward, onClose }) => {
  const [recompensa, setRecompensa] = useState({
    id: 0,
    nombre: "",
    imagen: "",
    precio: 0,
  });

  const [showIconPicker, setShowIconPicker] = useState(false);

  useEffect(() => {
    if (reward) {
      const iconName = extractIconName(reward.rutaImagenCompleta);

      setRecompensa({
        id: reward.id,
        nombre: reward.nombre,
        imagen: iconName,
        precio: reward.precio,
      });
    }
  }, [reward]);

  const crearRecompensaMutation = useCrearRecompensa(() => {
    setRecompensa({ id: 0, nombre: "", imagen: "", precio: 0 });
    onClose?.();
  });

  const editarRecompensaMutation = useEditarRecompensa(() => {
    setRecompensa({ id: 0, nombre: "", imagen: "", precio: 0 });
    onClose?.();
  });

  const eliminarRecompensaMutation = useEliminarRecompensa(() => {
    setRecompensa({ id: 0, nombre: "", imagen: "", precio: 0 });
    onClose?.();
  });

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

    if (!recompensa.nombre.trim()) {
      Toast.notificarError("El nombre de la recompensa no puede estar vacío.");
      return;
    }

    if (!recompensa.imagen) {
      Toast.notificarError("Debes seleccionar un ícono representativo.");
      return;
    }

    if (recompensa.precio < 0) {
      Toast.notificarError("El precio no puede ser negativo.");
      return;
    }

    const rutaImagenCompleta = recompensa.imagen;
    const rutaImagenMiniatura = recompensa.imagen;

    const payload = {
      nombre: recompensa.nombre,
      rutaImagenCompleta,
      rutaImagenMiniatura,
      precio: recompensa.precio,
    };

    if (recompensa.id && recompensa.id !== 0) {
      // Editar
      editarRecompensaMutation.mutate({ recompensaId: recompensa.id, data: payload });
    } else {
      // Crear
      crearRecompensaMutation.mutate(payload);
    }
  };

  const handleDelete = () => {
    if (!recompensa.id) return;
    if (window.confirm("¿Estás seguro que deseas eliminar esta recompensa?")) {
      eliminarRecompensaMutation.mutate(recompensa.id);
    }
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Nombre de la recompensa
        <input type="text" name="nombre" placeholder="Ej: Alfajor" value={recompensa.nombre} onChange={handleChange} required />
      </label>

      <label className={styles.iconButtonContainer}>
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
          {recompensa.id ? "Guardar cambios" : "Crear recompensa"}
        </button>
        {recompensa.id !== 0 && (
          <button type="button" className={`${styles.btnDelete} button-tertiary`} onClick={handleDelete} aria-label={`Eliminar recompensa ${recompensa.nombre}`}>
            <FontAwesomeIcon icon="fa-solid fa-trash" />
          </button>
        )}
      </div>
    </form>
  );
};

RewardCreateForm.propTypes = {
  reward: PropTypes.shape({
    id: PropTypes.number,
    nombre: PropTypes.string,
    rutaImagenCompleta: PropTypes.string,
    precio: PropTypes.number,
  }),
  onClose: PropTypes.func,
};

export default RewardCreateForm;

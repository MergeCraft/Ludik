import React, { useState } from "react";
import styles from "./RewardCreateForm.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const RewardCreateForm = () => {
  const [recompensa, setRecompensa] = useState({
    id: 0,
    nombre: "",
    imagen: "",
    precio: 0,
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRecompensa((prev) => ({
      ...prev,
      [name]: name === "precio" ? parseInt(value) || 0 : value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Aquí hacés el POST a tu API, o usás React Query mutation
    console.log(recompensa); // o ejecutás tu lógica para guardar
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Nombre de la recompensa
        <input type="text" name="nombre" placeholder="Ej: Alfajor" value={recompensa.nombre} onChange={handleChange} required />
      </label>

      <label>
        Imagen (URL o nombre de archivo)
        <input type="file" accept="image/*" name="imagen" placeholder="Ej: alfajor.png" value={recompensa.imagen} onChange={handleChange} required />
      </label>

      <label>
        Precio (en monedas)
        <input type="number" name="precio" placeholder="Ej: 150" value={recompensa.precio} onChange={handleChange} min={0} required />
      </label>

      <div className={styles.acciones}>
        <button type="submit" className={`${styles.btnSubmit} button-secondary`}>
          Crear recompensa
        </button>
        {recompensa.id != 0 && (
          <button className={`${styles.btnDelete} button-tertiary`}>
            <FontAwesomeIcon icon="fa-solid fa-trash" />
          </button>
        )}
      </div>
    </form>
  );
};

export default RewardCreateForm;

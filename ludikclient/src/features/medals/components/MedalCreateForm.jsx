// MedalCreateForm.jsx
import React, { useState } from "react";
import styles from "./MedalCreateForm.module.css";
import * as Toast from "../../../lib/toastify.js";
import { useCrearMedalla } from "../hooks/useMedalMutation.js";

const MedalCreateForm = () => {
  const [medalla, setMedalla] = useState({
    urlImagen: "",
    nombre: "",
    descripcion: "",
    cantidadMonedasBrinda: "",
    esAsignacionMutua: false,
  });

  const { mutateAsync: crear } = useCrearMedalla();

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    const val = type === "checkbox" ? checked : value;
    setMedalla((prev) => ({ ...prev, [name]: val }));
  };

  const validar = () => {
    const campos = [
      ["Nombre de la medalla", medalla.nombre],
      ["Descripción", medalla.descripcion],
      ["URL de imagen", medalla.urlImagen],
    ];
    for (const [campo, valor] of campos) {
      if (!valor || valor.trim().length < 3) {
        Toast.notificarWarning(`El campo "${campo}" debe tener al menos 3 caracteres.`);
        return false;
      }
    }
    if (!medalla.cantidadMonedasBrinda || isNaN(medalla.cantidadMonedasBrinda)) {
      Toast.notificarWarning("Cantidad de monedas debe ser un número válido.");
      return false;
    }
    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validar()) return;

    const payload = {
      ...medalla,
      cantidadMonedasBrinda: Number(medalla.cantidadMonedasBrinda),
    };

    await crear(payload);
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Nombre
        <input type="text" name="nombre" value={medalla.nombre} onChange={handleChange} placeholder="Ej: Estrella de participación" />
      </label>

      <label>
        Descripción
        <input type="text" name="descripcion" value={medalla.descripcion} onChange={handleChange} placeholder="Ej: Se otorga por participar activamente" />
      </label>

      <label>
        URL de imagen
        <input type="text" name="urlImagen" value={medalla.urlImagen} onChange={handleChange} placeholder="https://..." />
      </label>

      <label>
        Monedas otorgadas
        <input type="number" name="cantidadMonedasBrinda" value={medalla.cantidadMonedasBrinda} onChange={handleChange} placeholder="Ej: 50" />
      </label>

      <div className={styles.asignacionMutua}>
        <label className="switch">
          <input type="checkbox" name="esAsignacionMutua" checked={medalla.esAsignacionMutua} onChange={handleChange} />
          <span className="slider"></span>
        </label>
        <label htmlFor="recuerdame" className={`${styles.recuerdame}`}>
          ¿Es de asignación mutua?
        </label>
      </div>

      <button type="submit" className={`button-secondary ${styles.btnSubmit}`}>
        Crear medalla
      </button>
    </form>
  );
};

export default MedalCreateForm;

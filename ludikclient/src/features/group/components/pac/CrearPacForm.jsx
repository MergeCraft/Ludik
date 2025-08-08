import React, { useState } from "react";
import PropTypes from "prop-types";
import { useCrearPac } from "../../hooks/useGrupoMutation";
import styles from "./CrearPacForm.module.css";

const CrearPacForm = ({ groupId, onClose }) => {
  const [form, setForm] = useState({
    nombre: "",
    visual: 0,
    cantidadMedallasNecesarias: 0,
    recompensaClaseId: 0,
  });

  const { mutate: crearPac, isLoading } = useCrearPac(onClose);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: name === "nombre" ? value : Number(value),
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    crearPac({ grupoId: groupId, pacData: form });
  };

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      <div className={styles.field}>
        <label className={styles.label}>Nombre del desafío</label>
        <input type="text" name="nombre" value={form.nombre} onChange={handleChange} required className={styles.input} />
      </div>

      <div className={styles.field}>
        <label className={styles.label}>Cantidad de Medallas Necesarias</label>
        <input type="number" name="cantidadMedallasNecesarias" min={0} value={form.cantidadMedallasNecesarias} onChange={handleChange} className={styles.input} />
      </div>

      <div className={styles.fechas}>
        <div className={styles.field}>
          <label className={styles.label}>Fecha de inicio del desafío</label>
          <input type="date" name="fechaInicio" value={form.fechaInicio} onChange={handleChange} className={styles.input} />
        </div>
        <div className={styles.field}>
          <label className={styles.label}>Fecha de fin del desafío</label>
          <input type="date" name="fechaFin" value={form.fechaFin} onChange={handleChange} className={styles.input} />
        </div>
      </div>

      <div className={styles.field}>
        <label className={styles.label}>ID Recompensa Clase</label>
        <input type="number" name="recompensaClaseId" min={0} value={form.recompensaClaseId} onChange={handleChange} className={styles.input} />
      </div>

      <button type="submit" disabled={isLoading} className={`button-secondary ${styles.submitButton}`}>
        {isLoading ? "Creando..." : "Crear desafío"}
      </button>
    </form>
  );
};

CrearPacForm.propTypes = {
  groupId: PropTypes.number.isRequired,
  onClose: PropTypes.func,
};

export default CrearPacForm;

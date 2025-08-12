import React, { useState } from "react";
import PropTypes from "prop-types";
import { useCrearPac } from "../../hooks/useGrupoMutation";
import styles from "./CrearPacForm.module.css";

const CrearPacForm = ({ groupId, recompensas, onClose }) => {
  const [form, setForm] = useState({
    nombre: "",
    visual: 0,
    cantidadMedallasNecesarias: 0,
    recompensaClaseId: 0,
    fechaInicio: "",
    fechaFin: "",
  });

  const { mutate: crearPac, isLoading } = useCrearPac(onClose);

  // Obtener fecha actual en formato YYYY-MM-DD para usar en min de inputs
  const hoy = new Date().toISOString().split("T")[0];

  // Para calcular la fecha mínima para fechaFin, que debe ser > fechaInicio
  // Sumamos un día a fechaInicio para que fechaFin no pueda ser igual ni menor
  const getFechaMinFin = () => {
    if (!form.fechaInicio) return hoy;
    const fechaInicioDate = new Date(form.fechaInicio);
    fechaInicioDate.setDate(fechaInicioDate.getDate() + 1);
    return fechaInicioDate.toISOString().split("T")[0];
  };

  const handleChange = (e) => {
    const { name, value } = e.target;

    setForm((prev) => ({
      ...prev,
      [name]: name === "nombre" || name === "fechaInicio" || name === "fechaFin" ? value : Number(value),
    }));

    // Opcional: si el usuario cambia fechaInicio y fechaFin es menor o igual, reseteamos fechaFin
    if (name === "fechaInicio" && form.fechaFin && value >= form.fechaFin) {
      setForm((prev) => ({ ...prev, fechaFin: "" }));
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const pacData = {
      ...form,
      fechaInicio: form.fechaInicio || null,
      fechaFin: form.fechaFin || null,
    };

    crearPac({ grupoId: groupId, pacData });
  };

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      {/* Nombre */}
      <div className={styles.field}>
        <label className={styles.label}>Nombre del desafío</label>
        <input type="text" name="nombre" value={form.nombre} onChange={handleChange} required className={styles.input} />
      </div>

      {/* Medallas necesarias */}
      <div className={styles.field}>
        <label className={styles.label}>Cantidad de Medallas Necesarias</label>
        <input type="number" name="cantidadMedallasNecesarias" min={0} value={form.cantidadMedallasNecesarias} onChange={handleChange} className={styles.input} />
      </div>

      {/* Fechas */}
      <div className={styles.fechas}>
        <div className={styles.field}>
          <label className={styles.label}>Fecha de inicio del desafío</label>
          <input
            type="date"
            name="fechaInicio"
            value={form.fechaInicio}
            onChange={handleChange}
            className={styles.input}
            min={hoy} // No permite fechas anteriores a hoy
          />
        </div>
        <div className={styles.field}>
          <label className={styles.label}>Fecha de fin del desafío</label>
          <input
            type="date"
            name="fechaFin"
            value={form.fechaFin}
            onChange={handleChange}
            className={styles.input}
            min={getFechaMinFin()} // No permite fechas antes o igual a inicio
            disabled={!form.fechaInicio} // Deshabilitado si no hay fecha de inicio
          />
        </div>
      </div>

      {/* Select de recompensa */}
      <div className={styles.field}>
        <label className={styles.label}>Recompensa de la clase</label>
        <select name="recompensaClaseId" value={form.recompensaClaseId} onChange={handleChange} className={styles.input} required>
          <option value={0}>-- Selecciona una recompensa --</option>
          {recompensas?.map((rec) => (
            <option key={rec.id} value={rec.id}>
              {rec.nombre} (💰 {rec.precio})
            </option>
          ))}
        </select>
      </div>

      {/* Botón */}
      <button type="submit" disabled={isLoading} className={`button-secondary ${styles.submitButton}`}>
        {isLoading ? "Creando..." : "Crear desafío"}
      </button>
    </form>
  );
};

CrearPacForm.propTypes = {
  groupId: PropTypes.number.isRequired,
  recompensas: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      nombre: PropTypes.string.isRequired,
      precio: PropTypes.number.isRequired,
      tipo: PropTypes.string.isRequired,
      datos: PropTypes.shape({
        $type: PropTypes.string.isRequired,
        nombreIcono: PropTypes.string.isRequired,
      }).isRequired,
    })
  ).isRequired,
  onClose: PropTypes.func,
};

export default CrearPacForm;

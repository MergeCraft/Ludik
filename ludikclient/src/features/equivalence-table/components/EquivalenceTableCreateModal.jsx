import React, { useState } from "react";
import PropTypes from "prop-types";

import styles from "./EquivalenceTableCreateModal.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { useMedallasProfesor } from "../../medals/hooks/useMedalMutation";
import { useCrearTablaEquivalencia } from "../hooks/useEquivalenceTableMutation";

const EquivalenceTableCreateModal = ({ onClose }) => {
  const [equivalencia, setEquivalencia] = useState({ nombre: "", equivalencias: [] });
  const { mutateAsync: crearTablaEquivalencia } = useCrearTablaEquivalencia();

  // Hook para obtener las medallas del profesor
  const { data: medallas, isLoading } = useMedallasProfesor();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEquivalencia((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleAddEquivalencia = () => {
    setEquivalencia((prev) => ({
      ...prev,
      equivalencias: [...prev.equivalencias, { nota: 0, medallasNecesarias: [] }],
    }));
  };

  const handleEquivalenciaChange = (index, field, value) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      newEquivalencias[index][field] = field === "nota" ? Number(value) : value;
      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  const handleAddMedalla = (equIndex, medallaId) => {
    // Busca la medalla en el listado
    const medalla = medallas.find((m) => m.id === medallaId);
    if (medalla) {
      setEquivalencia((prev) => {
        const newEquivalencias = [...prev.equivalencias];
        newEquivalencias[equIndex].medallasNecesarias.push(medalla);
        return { ...prev, equivalencias: newEquivalencias };
      });
    }
  };

  const handleRemoveMedalla = (equIndex, medIndex) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      newEquivalencias[equIndex].medallasNecesarias.splice(medIndex, 1);
      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  const handleSave = async (e) => {
    e.preventDefault();

    try {
      // Transformamos antes de guardar:
      const equivalenciaToSave = {
        nombre: equivalencia.nombre,
        equivalencias: equivalencia.equivalencias.map((item) => ({
          nota: item.nota,
          medallasNecesarias: item.medallasNecesarias.map((medalla) => ({
            id: medalla.id,
            nombre: medalla.nombre,
            urlImagen: medalla.urlImagen,
          })),
        })),
      };

      console.log("Enviando a la API", equivalenciaToSave);
      await crearTablaEquivalencia(equivalenciaToSave);
      onClose();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSave}>
      <label>
        Nombre de la tabla de equivalencia
        <input type="text" name="nombre" value={equivalencia.nombre} onChange={handleChange} placeholder="Ej: Criterios de evaluación" />
      </label>

      <h4>Equivalencias</h4>

      <div className={styles.equivalencias}>
        {equivalencia.equivalencias.map((item, i) => (
          <div key={i} className={styles.equivalencia}>
            <div className={styles.nota}>
              <label>Valor (nota)</label>
              <input type="number" min="1" value={item.nota} onChange={(e) => handleEquivalenciaChange(i, "nota", e.target.value)} placeholder="Ej: 5" />
            </div>

            <div className={styles.medallasNecesarias}>
              <h5>Medallas necesarias</h5>
              {/* Select flotante o picker*/}
              <select className={styles.selectMedalla} disabled={isLoading} onChange={(e) => handleAddMedalla(i, Number(e.target.value))}>
                <option value="">Seleccione una medalla</option>
                {medallas &&
                  medallas.map((medalla) => (
                    <option key={medalla.id} value={medalla.id}>
                      {medalla.nombre}
                    </option>
                  ))}
              </select>

              {/* Listado de Medallas ya Agregadas*/}
              <div className={styles.medallasSeleccionadas}>
                {item.medallasNecesarias.map((medalla, j) => (
                  <div key={j} className={styles.medalla}>
                    <h4>{medalla.nombre}</h4>
                    <img src={medalla.urlImagen} alt={medalla.nombre} />
                    <div className={styles.extraInfo}>
                      <p>{medalla.descripcion}</p>
                      <p>
                        <FontAwesomeIcon icon="fa-solid fa-coins" />
                        {medalla.cantidadMedallasBrinda}
                      </p>
                      <button className={`${styles.eliminarMedalla}`} onClick={() => handleRemoveMedalla(i, j)}>
                        <FontAwesomeIcon icon="fa-solid fa-trash" />
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className={styles.acciones}>
        <button type="button" className="button" onClick={handleAddEquivalencia}>
          Agregar Equivalencia
        </button>

        <button type="submit" className="button-secondary">
          Crear tabla
        </button>
      </div>
    </form>
  );
};

EquivalenceTableCreateModal.propTypes = {
  onClose: PropTypes.func.isRequired,
  onSave: PropTypes.func.isRequired,
};

export default EquivalenceTableCreateModal;

import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";

import styles from "./EquivalenceTableCreateModal.module.css";

import { useMedallasProfesor } from "../../medals/hooks/useMedalMutation";

import MedalCard from "../../medals/components/MedalCard";

const EquivalenceTableCreateModal = ({ onClose, onSave }) => {
  const [equivalencia, setEquivalencia] = useState({ nombre: "", equivalencias: [] });

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

  const handleSave = (e) => {
    e.preventDefault();

    console.log("Equivalencia a guardar!", equivalencia);
    onSave(equivalencia);
    onClose();
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSave}>
      <label>
        Nombre de la tabla de equivalencia
        <input type="text" name="nombre" value={equivalencia.nombre} onChange={handleChange} placeholder="Ej: Criterios de evaluación" />
      </label>

      <h4>Equivalencias</h4>

      {equivalencia.equivalencias.map((item, i) => (
        <div key={i} className={styles.equivalencia}>
          <div className={styles.nota}>
            <label>Valor (nota)</label>
            <input type="number" min="1" value={item.nota} onChange={(e) => handleEquivalenciaChange(i, "nota", e.target.value)} placeholder="Ej: 5" />
          </div>

          <div className={styles.medallasNecesarias}>
            <h5>Medallas necesarias</h5>

            {/* Listado de Medallas ya Agregadas*/}
            {item.medallasNecesarias.map((medalla, j) => (
              <div key={j} className={styles.medalla}>
                {/* Podrías reusear tu MedalCard aquí*/}
                <MedalCard
                  nombre={medalla.nombre}
                  descripcion={medalla.descripcion}
                  urlImagen={medalla.urlImagen}
                  cantidadMedallasBrinda={medalla.cantidadMedallasBrinda}
                  esAsignacionMutua={medalla.esAsignacionMutua}
                />
              </div>
            ))}

            {/* Botón para Agregar Medalla */}
            <button
              type="button"
              className="button-secondary"
              disabled={isLoading}
              onClick={() => {
                // aquí podrías implementar un modal picker
                // pero por simplicidad, un select:
                // el select permitirá que elijas el id de la medalla
              }}
            >
              Agregar Medalla
            </button>

            {/* Select flotante o picker*/}
            <select disabled={isLoading} onChange={(e) => handleAddMedalla(i, Number(e.target.value))}>
              <option value="">Seleccione una medalla</option>
              {medallas &&
                medallas.map((medalla) => (
                  <option key={medalla.id} value={medalla.id}>
                    {medalla.nombre}
                  </option>
                ))}
            </select>
          </div>
        </div>
      ))}

      <div className={styles.acciones}>
        <button type="button" className="button-secondary" onClick={handleAddEquivalencia}>
          Agregar Equivalencia
        </button>

        <button type="submit" className="button-secondary">
          Crear equivalencia
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

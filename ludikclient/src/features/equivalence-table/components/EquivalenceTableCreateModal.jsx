import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";

import styles from "./EquivalenceTableCreateModal.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { useMedallasProfesor } from "../../medals/hooks/useMedalMutation";
import { useCrearTablaEquivalencia, useEditarTablaEquivalencia } from "../hooks/useEquivalenceTableMutation";

const EquivalenceTableCreateModal = ({ onClose, onSave, table }) => {
  const [equivalencia, setEquivalencia] = useState({ nombre: "", equivalencias: [] });

  const { mutateAsync: crearTablaEquivalencia } = useCrearTablaEquivalencia();
  const { mutateAsync: editarTablaEquivalencia } = useEditarTablaEquivalencia();

  const { data: medallas, isLoading } = useMedallasProfesor();

  // Precargar datos si viene una tabla
  useEffect(() => {
    if (table) {
      setEquivalencia({
        nombre: table.nombre,
        equivalencias: table.equivalencias.map((eq) => ({
          nota: eq.nota,
          medallasNecesarias: eq.medallasNecesarias || [],
        })),
      });
    }
  }, [table]);

  const handleAddEquivalencia = () => {
    setEquivalencia((prev) => {
      const medallasAcumuladasIds = new Set();
      prev.equivalencias.forEach((equ) => equ.medallasNecesarias.forEach((med) => medallasAcumuladasIds.add(med.id)));
      const medallasAcumuladas = medallas ? medallas.filter((m) => medallasAcumuladasIds.has(m.id)) : [];

      return {
        ...prev,
        equivalencias: [...prev.equivalencias, { nota: 0, medallasNecesarias: medallasAcumuladas }],
      };
    });
  };

  const handleEquivalenciaChange = (index, field, value) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];

      if (field === "nota") {
        newEquivalencias[index].nota = Number(value);
      } else if (field === "medallasNecesarias") {
        newEquivalencias[index].medallasNecesarias = value;
        for (let i = index + 1; i < newEquivalencias.length; i++) {
          const currentMedIds = new Set(newEquivalencias[index].medallasNecesarias.map((m) => m.id));
          const nextMedIds = new Set(newEquivalencias[i].medallasNecesarias.map((m) => m.id));
          const unionMedIds = new Set([...nextMedIds, ...currentMedIds]);
          const nuevasMedallas = medallas ? medallas.filter((m) => unionMedIds.has(m.id)) : [];
          newEquivalencias[i].medallasNecesarias = nuevasMedallas;
        }
      }

      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  const handleAddMedalla = (equIndex, medallaId) => {
    if (!medallaId) return;
    const medalla = medallas.find((m) => m.id === medallaId);
    if (!medalla) return;

    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      const currentMedallas = newEquivalencias[equIndex].medallasNecesarias;
      if (currentMedallas.some((m) => m.id === medalla.id)) return prev;

      const updatedMedallas = [...currentMedallas, medalla];
      const tempEquivalencias = [...newEquivalencias];
      tempEquivalencias[equIndex].medallasNecesarias = updatedMedallas;

      for (let i = equIndex + 1; i < tempEquivalencias.length; i++) {
        const currentMedIds = new Set(tempEquivalencias[equIndex].medallasNecesarias.map((m) => m.id));
        const nextMedIds = new Set(tempEquivalencias[i].medallasNecesarias.map((m) => m.id));
        const unionMedIds = new Set([...nextMedIds, ...currentMedIds]);
        tempEquivalencias[i].medallasNecesarias = medallas ? medallas.filter((m) => unionMedIds.has(m.id)) : [];
      }

      return { ...prev, equivalencias: tempEquivalencias };
    });
  };

  const handleRemoveMedalla = (equIndex, medIndex) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      const currentMedallas = [...newEquivalencias[equIndex].medallasNecesarias];
      const medallaEliminada = currentMedallas[medIndex];
      if (!medallaEliminada) return prev;

      currentMedallas.splice(medIndex, 1);
      newEquivalencias[equIndex].medallasNecesarias = currentMedallas;

      for (let i = equIndex + 1; i < newEquivalencias.length; i++) {
        newEquivalencias[i].medallasNecesarias = newEquivalencias[i].medallasNecesarias.filter((m) => m.id !== medallaEliminada.id);
      }

      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEquivalencia((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSave = async (e) => {
    e.preventDefault();

    try {
      const idTabla = table?.id ?? 0;

      const equivalenciaToSave = {
        id: idTabla, // asegurate que esté el mismo ID que va en la ruta
        nombre: equivalencia.nombre,
        equivalencias: equivalencia.equivalencias.map((item, index) => ({
          id: table?.equivalencias?.[index]?.id ?? 0,
          nota: item.nota,
          medallasNecesarias: item.medallasNecesarias.map((medalla) => ({
            id: medalla.id,
            nombre: medalla.nombre,
            urlImagen: medalla.urlImagen,
            descripcion: medalla.descripcion ?? "",
            cantidadMedallasBrinda: medalla.cantidadMedallasBrinda ?? 0,
            esAsignacionMutua: medalla.esAsignacionMutua ?? false,
          })),
        })),
      };

      if (table && table.id) {
        // asegurate de que el ID esté en ambos lugares
        await editarTablaEquivalencia({
          id: idTabla,
          data: equivalenciaToSave,
        });
      } else {
        await crearTablaEquivalencia(equivalenciaToSave);
      }

      if (onSave) onSave(equivalenciaToSave);
      onClose();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSave}>
      <label>
        Nombre de la tabla de equivalencia
        <input type="text" name="nombre" value={equivalencia.nombre} onChange={handleChange} placeholder="Ej: Criterios de evaluación" required />
      </label>

      <h4>Equivalencias</h4>

      <div className={styles.equivalencias}>
        {equivalencia.equivalencias.map((item, i) => (
          <div key={i} className={styles.equivalencia}>
            <div className={styles.nota}>
              <label>Valor (nota)</label>
              <input type="number" min="1" value={item.nota} onChange={(e) => handleEquivalenciaChange(i, "nota", e.target.value)} placeholder="Ej: 5" required />
            </div>

            <div className={styles.medallasNecesarias}>
              <h5>Medallas necesarias</h5>
              <select
                className={styles.selectMedalla}
                disabled={isLoading}
                onChange={(e) => {
                  handleAddMedalla(i, Number(e.target.value));
                  e.target.value = "";
                }}
              >
                <option value="">Seleccione una medalla</option>
                {medallas &&
                  medallas.map((medalla) => (
                    <option key={medalla.id} value={medalla.id}>
                      {medalla.nombre}
                    </option>
                  ))}
              </select>

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
                      <button type="button" className={styles.eliminarMedalla} onClick={() => handleRemoveMedalla(i, j)}>
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
          {table ? "Guardar cambios" : "Crear tabla"}
        </button>
      </div>
    </form>
  );
};

EquivalenceTableCreateModal.propTypes = {
  onClose: PropTypes.func.isRequired,
  onSave: PropTypes.func,
  table: PropTypes.shape({
    id: PropTypes.number,
    nombre: PropTypes.string.isRequired,
    equivalencias: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number,
        nota: PropTypes.number.isRequired,
        medallasNecesarias: PropTypes.arrayOf(
          PropTypes.shape({
            id: PropTypes.number.isRequired,
            nombre: PropTypes.string.isRequired,
            urlImagen: PropTypes.string,
            descripcion: PropTypes.string,
            cantidadMedallasBrinda: PropTypes.number,
            esAsignacionMutua: PropTypes.bool,
          })
        ).isRequired,
      })
    ).isRequired,
  }),
};

export default EquivalenceTableCreateModal;

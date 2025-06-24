import React, { useState } from "react";
import PropTypes from "prop-types";

import styles from "./EquivalenceTableCreateModal.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { useMedallasProfesor } from "../../medals/hooks/useMedalMutation";
import { useCrearTablaEquivalencia } from "../hooks/useEquivalenceTableMutation";

const EquivalenceTableCreateModal = ({ onClose, onSave }) => {
  const [equivalencia, setEquivalencia] = useState({ nombre: "", equivalencias: [] });
  const { mutateAsync: crearTablaEquivalencia } = useCrearTablaEquivalencia();

  // Hook para obtener las medallas del profesor
  const { data: medallas, isLoading } = useMedallasProfesor();

  // Cuando se agrega una equivalencia nueva, debe tener todas las medallas
  // de las equivalencias anteriores (la union de medallas necesarias previas)
  const handleAddEquivalencia = () => {
    setEquivalencia((prev) => {
      // Obtener medallas acumuladas de todas las equivalencias anteriores
      const medallasAcumuladasIds = new Set();
      prev.equivalencias.forEach((equ) => equ.medallasNecesarias.forEach((med) => medallasAcumuladasIds.add(med.id)));

      // Extraer objetos medalla completos para esos IDs desde las medallas del profesor (para asegurar datos completos)
      const medallasAcumuladas = medallas ? medallas.filter((m) => medallasAcumuladasIds.has(m.id)) : [];

      return {
        ...prev,
        equivalencias: [...prev.equivalencias, { nota: 0, medallasNecesarias: medallasAcumuladas }],
      };
    });
  };

  // Actualiza una equivalencia (campo nota o medallasNecesarias)
  // Para las medallasNecesarias, si es agregado o removido, se sincroniza con las equivalencias siguientes
  // field puede ser 'nota' o 'medallasNecesarias' (solo usaremos medallasNecesarias para sync)
  const handleEquivalenciaChange = (index, field, value) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];

      if (field === "nota") {
        newEquivalencias[index].nota = Number(value);
      } else if (field === "medallasNecesarias") {
        // Aquí value es el nuevo array completo de medallasNecesarias para esa equivalencia
        newEquivalencias[index].medallasNecesarias = value;

        // Sincronizamos: para todas las equivalencias posteriores, actualizamos medallasNecesarias para que incluyan todas las medallas de esta equivalencia
        for (let i = index + 1; i < newEquivalencias.length; i++) {
          // Queremos que las siguientes tengan al menos las medallas de la equivalencia actual,
          // más las que ya tenían (sin duplicados)
          const currentMedIds = new Set(newEquivalencias[index].medallasNecesarias.map((m) => m.id));
          const nextMedIds = new Set(newEquivalencias[i].medallasNecesarias.map((m) => m.id));

          // Unir sin duplicados
          const unionMedIds = new Set([...nextMedIds, ...currentMedIds]);

          // Construir nuevo array de medallas completas desde medallas del profesor
          const nuevasMedallas = medallas ? medallas.filter((m) => unionMedIds.has(m.id)) : [];

          newEquivalencias[i].medallasNecesarias = nuevasMedallas;
        }
      }

      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  // Agregar medalla a una equivalencia específica (manteniendo sincronización)
  const handleAddMedalla = (equIndex, medallaId) => {
    if (!medallaId) return;
    const medalla = medallas.find((m) => m.id === medallaId);
    if (!medalla) return;

    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      const currentMedallas = newEquivalencias[equIndex].medallasNecesarias;

      // Si ya existe, no agregar
      if (currentMedallas.some((m) => m.id === medalla.id)) return prev;

      const updatedMedallas = [...currentMedallas, medalla];

      // Actualizamos esa equivalencia y sincronizamos hacia adelante
      // Usamos handleEquivalenciaChange para la sincronización
      const tempEquivalencias = [...newEquivalencias];
      tempEquivalencias[equIndex].medallasNecesarias = updatedMedallas;

      // Ahora sincronizamos las siguientes equivalencias:
      for (let i = equIndex + 1; i < tempEquivalencias.length; i++) {
        const currentMedIds = new Set(tempEquivalencias[equIndex].medallasNecesarias.map((m) => m.id));
        const nextMedIds = new Set(tempEquivalencias[i].medallasNecesarias.map((m) => m.id));
        const unionMedIds = new Set([...nextMedIds, ...currentMedIds]);
        tempEquivalencias[i].medallasNecesarias = medallas ? medallas.filter((m) => unionMedIds.has(m.id)) : [];
      }

      return { ...prev, equivalencias: tempEquivalencias };
    });
  };

  // Eliminar medalla de una equivalencia específica (sincronizando)
  const handleRemoveMedalla = (equIndex, medIndex) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      const currentMedallas = [...newEquivalencias[equIndex].medallasNecesarias];

      // Medalla a eliminar
      const medallaEliminada = currentMedallas[medIndex];
      if (!medallaEliminada) return prev;

      // Quitar la medalla de la equivalencia actual
      currentMedallas.splice(medIndex, 1);
      newEquivalencias[equIndex].medallasNecesarias = currentMedallas;

      // Para eliminar la medalla de las equivalencias siguientes también,
      // porque si se elimina en la actual, debe eliminarse en las siguientes que la tengan
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

      await crearTablaEquivalencia(equivalenciaToSave);
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
          Crear tabla
        </button>
      </div>
    </form>
  );
};

EquivalenceTableCreateModal.propTypes = {
  onClose: PropTypes.func.isRequired,
  onSave: PropTypes.func,
};

export default EquivalenceTableCreateModal;

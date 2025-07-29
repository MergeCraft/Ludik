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
  // Si no hay medallas, inicializamos con una equivalencia por defecto
  useEffect(() => {
    if (table) {
      // Carga al editar tabla existente (igual que antes)
      const equivalenciasConHeredadas = table.equivalencias.map((eq, index) => {
        const medallasActuales = eq.medallasNecesarias || [];

        const medallasPrevias = new Set();
        for (let i = 0; i < index; i++) {
          (table.equivalencias[i]?.medallasNecesarias || []).forEach((m) => medallasPrevias.add(m.id));
        }

        const medallasMarcadas = medallasActuales.map((m) => ({
          ...m,
          esHeredada: medallasPrevias.has(m.id),
        }));

        return {
          nota: eq.nota,
          medallasNecesarias: medallasMarcadas,
        };
      });

      setEquivalencia({
        nombre: table.nombre,
        equivalencias: equivalenciasConHeredadas,
      });
    } else if (medallas && medallas.length > 0) {
      // Caso creación nueva tabla: inicializamos con una equivalencia por defecto con nota 1 y la primera medalla asignada
      setEquivalencia({
        nombre: "",
        equivalencias: [
          {
            nota: 1,
            medallasNecesarias: [{ ...medallas[0], esHeredada: false }],
          },
        ],
      });
    } else {
      // Si no hay medallas cargadas aún, dejamos vacío
      setEquivalencia({
        nombre: "",
        equivalencias: [],
      });
    }
  }, [table, medallas]);

  // Agregar una nueva equivalencia con la última nota +1 y medallas heredadas
  // Si no hay medallas, el botón se deshabilita
  const handleAddEquivalencia = () => {
    setEquivalencia((prev) => {
      const medallasAcumuladasIds = new Set();
      prev.equivalencias.forEach((equ) => equ.medallasNecesarias.forEach((med) => medallasAcumuladasIds.add(med.id)));

      const medallasAcumuladas = medallas ? medallas.filter((m) => medallasAcumuladasIds.has(m.id)).map((m) => ({ ...m, esHeredada: true })) : [];

      // Obtener la última nota o 0 si no hay equivalencias
      const ultimaNota = prev.equivalencias.length > 0 ? prev.equivalencias[prev.equivalencias.length - 1].nota : 0;

      return {
        ...prev,
        equivalencias: [
          ...prev.equivalencias,
          {
            nota: ultimaNota + 1, // nota +1 respecto a la anterior
            medallasNecesarias: medallasAcumuladas,
          },
        ],
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

  // Agregar una medalla a una equivalencia específica
  // Si la medalla ya existe, no se agrega
  const handleAddMedalla = (equIndex, medallaId) => {
    if (!medallaId) return;
    const medalla = medallas.find((m) => m.id === medallaId);
    if (!medalla) return;

    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      const currentMedallas = newEquivalencias[equIndex].medallasNecesarias;

      if (currentMedallas.some((m) => m.id === medalla.id)) return prev;

      // Agregamos con esHeredada = false
      const nuevaMedalla = { ...medalla, esHeredada: false };
      const updatedMedallas = [...currentMedallas, nuevaMedalla];
      newEquivalencias[equIndex].medallasNecesarias = updatedMedallas;

      // Propagamos como heredada hacia abajo
      for (let i = equIndex + 1; i < newEquivalencias.length; i++) {
        const yaExiste = newEquivalencias[i].medallasNecesarias.some((m) => m.id === medalla.id);
        if (!yaExiste) {
          newEquivalencias[i].medallasNecesarias.push({ ...medalla, esHeredada: true });
        }
      }

      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  // Eliminar una medalla específica de una equivalencia
  // Si la medalla no es heredada, se elimina de todas las equivalencias siguientes
  const handleRemoveMedalla = (equIndex, medIndex) => {
    setEquivalencia((prev) => {
      const newEquivalencias = [...prev.equivalencias];
      const currentMedallas = [...newEquivalencias[equIndex].medallasNecesarias];
      const medallaEliminada = currentMedallas[medIndex];

      if (!medallaEliminada || medallaEliminada.esHeredada) return prev;

      // Eliminar
      currentMedallas.splice(medIndex, 1);
      newEquivalencias[equIndex].medallasNecesarias = currentMedallas;

      // Eliminar de las siguientes equivalencias si también no es heredada
      for (let i = equIndex + 1; i < newEquivalencias.length; i++) {
        newEquivalencias[i].medallasNecesarias = newEquivalencias[i].medallasNecesarias.filter((m) => m.id !== medallaEliminada.id);
      }

      return { ...prev, equivalencias: newEquivalencias };
    });
  };

  // Eliminar una equivalencia completa
  const handleRemoveEquivalencia = (index) => {
    setEquivalencia((prev) => {
      const nuevasEquivalencias = [...prev.equivalencias];
      nuevasEquivalencias.splice(index, 1);

      return {
        ...prev,
        equivalencias: nuevasEquivalencias,
      };
    });
  };

  // Manejo de cambios en los inputs
  const handleChange = (e) => {
    const { name, value } = e.target;
    setEquivalencia((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  // Guardar cambios o crear nueva tabla
  const handleSave = async (e) => {
    e.preventDefault();

    try {
      const idTabla = table?.id ?? 0;

      // Preparar datos para guardar
      const equivalenciaToSave = {
        id: idTabla,
        nombre: equivalencia.nombre,
        equivalencias: equivalencia.equivalencias.map((item, index) => ({
          id: table?.equivalencias?.[index]?.id ?? 0,
          nota: item.nota,
          medallasNecesarias: item.medallasNecesarias.map((medalla) => ({
            id: medalla.id,
            nombre: medalla.nombre,
            urlImagen: medalla.urlImagen || "",
            descripcion: medalla.descripcion ?? "",
            cantidadMedallasBrinda: medalla.cantidadMedallasBrinda ?? 0,
            esAsignacionMutua: medalla.esAsignacionMutua ?? false,
          })),
        })),
      };

      // Llamar al hook de mutación
      if (table && table.id) {
        await editarTablaEquivalencia({
          id: idTabla,
          data: equivalenciaToSave,
        });
      } else {
        await crearTablaEquivalencia(equivalenciaToSave);
      }

      // Notificar éxito y cerrar modal
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
            <button type="button" className={styles.eliminarEquivalencia} onClick={() => handleRemoveEquivalencia(i)} title="Eliminar equivalencia">
              <FontAwesomeIcon icon="fa-solid fa-xmark" />
            </button>
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
                    {!medalla.esHeredada && (
                      <button type="button" className={`${styles.eliminarMedalla} button-tertiary`} onClick={() => handleRemoveMedalla(i, j)} title="Eliminar medalla">
                        <FontAwesomeIcon icon="fa-solid fa-trash" />
                      </button>
                    )}
                  </div>
                ))}
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className={styles.acciones}>
        <button type="button" className="button" onClick={handleAddEquivalencia} disabled={isLoading || !medallas || medallas.length === 0}>
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

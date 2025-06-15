// GroupCreateModal.jsx
import React, { useState } from "react";
import styles from "./GroupCreateForm.module.css";
import * as Toast from "../../../../lib/toastify.js";
import PropTypes from "prop-types";

import { useCrearGrupo } from "../../hooks/useGrupoMutation.js";
import { useTablasEquivalencia } from "../../../equivalence-table/hooks/useEquivalenceTableMutation";

export const GroupCreateModal = ({ onClose }) => {
  const [grupo, setGrupo] = useState({ nombre: "", institucion: "", materia: "", tablaEquivalenciaId: "" });

  const { mutateAsync: crear } = useCrearGrupo();

  // Hook para obtener las tablas de equivalencia
  const { data: tablasEquivalencia, isLoading } = useTablasEquivalencia();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setGrupo((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const validar = () => {
    const campos = [
      ["Nombre del grupo", grupo.nombre],
      ["Institución", grupo.institucion],
      ["Materia", grupo.materia],
    ];

    for (const [campo, valor] of campos) {
      if (valor.trim().length <= 3) {
        Toast.notificarWarning(`El ${campo} debe tener más de 3 caracteres.`);
        return false;
      }
    }
    if (!grupo.tablaEquivalenciaId) {
      Toast.notificarWarning("Debes seleccionar una rúbrica.");
      return false;
    }
    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validar()) return;

    const userData = JSON.parse(sessionStorage.getItem("userData"));
    const profesorId = userData?.usuarioId;

    const grupoFinal = {
      ...grupo,
      tablaEquivalenciaId: Number(grupo.tablaEquivalenciaId),
      profesorId,
    };

    await crear(grupoFinal);
 
    // Cerrar el modal
    onClose();
  };
  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Nombre del grupo
        <input type="text" name="nombre" placeholder="Ej: Liceo 12 - 2° A" value={grupo.nombre} onChange={handleChange} />
      </label>

      <label>
        Institución
        <input type="text" name="institucion" placeholder="Ej: Montevideo" value={grupo.institucion} onChange={handleChange} />
      </label>

      <label>
        Materia
        <input type="text" name="materia" placeholder="Ej: Matemática" value={grupo.materia} onChange={handleChange} />
      </label>

      <label>
        Rúbrica
        <div className={styles.selectWrapper}>
          <select name="tablaEquivalenciaId" value={grupo.tablaEquivalenciaId} onChange={handleChange}>
            <option value="">Seleccionar</option>
            {isLoading ? (
              <option disabled>Cargando...</option>
            ) : (
              tablasEquivalencia &&
              tablasEquivalencia.map((tabla) => (
                <option key={tabla.id} value={tabla.id}>
                  {`${tabla.nombre} - ${tabla.equivalencias.length} ${tabla.equivalencias.length > 1 ? "notas" : "nota"}`}
                </option>
              ))
            )}
          </select>
        </div>
      </label>

      <button type="submit" className={`${styles.btnSubmit} button-secondary`}>
        Crear grupo
      </button>
    </form>
  );
};

GroupCreateModal.propTypes = {
  onClose: PropTypes.func.isRequired,
};

export default GroupCreateModal;

import React, { useState } from "react";
import { useRecompensasProfesor, useAsignarRecompensaAGrupos } from "../../../rewards/hooks/useRewardMutation";
import * as Toast from "../../../../lib/toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import PropTypes from "prop-types";
import styles from "./RewardAsignationForm.module.css";

const RewardAsignationForm = ({ grupoId, gruposProfesor, onClose }) => {
  const [recompensaId, setRecompensaId] = useState("");
  const [gruposSeleccionados, setGruposSeleccionados] = useState([grupoId]);

  console.log(grupoId);

  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasProfesor();

  const { mutate: asignarRecompensa, isLoading: isLoadingAsignacion } = useAsignarRecompensaAGrupos(() => {
    onClose();
  });

  const toggleGrupoSeleccionado = (id) => {
    setGruposSeleccionados((prev) => (prev.includes(id) ? prev.filter((gid) => gid !== id) : [...prev, id]));
  };

  const eliminarGrupo = (id) => {
    setGruposSeleccionados((prev) => prev.filter((gid) => gid !== id));
  };

  const handleAsignar = () => {
    if (!recompensaId) {
      Toast.notificarError("Por favor selecciona una recompensa.");
      return;
    }
    if (!gruposSeleccionados.length) {
      Toast.notificarError("Selecciona al menos un grupo.");
      return;
    }

    asignarRecompensa({
      recompensaId: Number(recompensaId),
      gruposIds: gruposSeleccionados,
    });
  };

  return (
    <div className={styles.container}>
      <label htmlFor="selectRecompensa" className={styles.label}>
        Selecciona una recompensa:
      </label>
      <select id="selectRecompensa" value={recompensaId} onChange={(e) => setRecompensaId(e.target.value)} disabled={isLoadingRecompensas} className={styles.select}>
        {isLoadingRecompensas ? (
          <option>Cargando...</option>
        ) : (
          <>
            <option value="">-- Seleccionar --</option>
            {recompensas?.map((r) => (
              <option key={r.id} value={r.id}>
                {r.nombre}
              </option>
            ))}
          </>
        )}
      </select>

      {!grupoId && (
        <>
          <fieldset className={styles.fieldset}>
            <legend>Selecciona los grupos a asignar:</legend>
            {(!gruposProfesor || gruposProfesor.length === 0) && <p>No tienes grupos disponibles.</p>}
            {gruposProfesor?.map((grupo) => (
              <div key={grupo.id} className={styles.checkboxContainer}>
                <label>
                  <input type="checkbox" checked={gruposSeleccionados.includes(grupo.id)} onChange={() => toggleGrupoSeleccionado(grupo.id)} /> {grupo.nombre}
                </label>
              </div>
            ))}
          </fieldset>

          <div className={styles.selectedGroups}>
            <h4>Grupos seleccionados:</h4>
            {gruposSeleccionados.length === 0 && <p>Ningún grupo seleccionado</p>}
            <ul className={styles.selectedList}>
              {gruposSeleccionados.map((id) => {
                const grupo = gruposProfesor?.find((g) => g.id === id);
                if (!grupo) return null;
                return (
                  <li key={id} className={styles.selectedListItem}>
                    <span>{grupo.nombre}</span>
                    <button onClick={() => eliminarGrupo(id)} className={styles.deleteButton} aria-label={`Eliminar grupo ${grupo.nombre}`}>
                      <FontAwesomeIcon icon="trash" />
                    </button>
                  </li>
                );
              })}
            </ul>
          </div>
        </>
      )}

      <button onClick={handleAsignar} disabled={isLoadingAsignacion} className={`button ${styles.assignButton}`}>
        {isLoadingAsignacion ? "Asignando..." : "Asignar"}
      </button>
    </div>
  );
};

RewardAsignationForm.propTypes = {
  grupoId: PropTypes.number.isRequired,
  gruposProfesor: PropTypes.arrayOf(PropTypes.shape({ id: PropTypes.number, nombre: PropTypes.string })).isRequired,
  onClose: PropTypes.func.isRequired,
  asignarRecompensa: PropTypes.func.isRequired,
  isLoadingAsignacion: PropTypes.bool,
};

export default RewardAsignationForm;

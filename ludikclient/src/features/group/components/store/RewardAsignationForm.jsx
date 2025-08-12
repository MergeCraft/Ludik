import React, { useState } from "react";
import { useRecompensasProfesor, useAsignarRecompensaAGrupos } from "../../../rewards/hooks/useRewardMutation";
import * as Toast from "../../../../lib/toastify";
import PropTypes from "prop-types";
import styles from "./RewardAsignationForm.module.css";
import { BarLoader } from "react-spinners";

const RewardAsignationForm = ({ grupoId, gruposProfesor, isLoadingGroups, onClose }) => {
  const [recompensaId, setRecompensaId] = useState("");
  const [gruposSeleccionados, setGruposSeleccionados] = useState(grupoId ? [grupoId] : []);

  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasProfesor();

  const { mutate: asignarRecompensa, isLoading: isLoadingAsignacion } = useAsignarRecompensaAGrupos(() => {
    onClose();
  });

  const toggleGrupoSeleccionado = (id) => {
    setGruposSeleccionados((prev) => (prev.includes(id) ? prev.filter((gid) => gid !== id) : [...prev, id]));
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
    <div className={styles.containerForm}>
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
            {isLoadingGroups ? (
              <BarLoader />
            ) : (
              <>
                <legend>Selecciona los grupos a asignar</legend>
                {(!gruposProfesor || gruposProfesor.length === 0) && <p>No tienes grupos disponibles.</p>}
                {gruposProfesor?.map((grupo) => (
                  <div key={grupo.id} className={styles.checkboxContainer}>
                    <label className={styles.groupAsignationOption}>
                      <p>{grupo.nombre}</p>
                      <input type="checkbox" checked={gruposSeleccionados.includes(grupo.id)} onChange={() => toggleGrupoSeleccionado(grupo.id)} />
                    </label>
                  </div>
                ))}
              </>
            )}
          </fieldset>
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
  isLoadingGroups: PropTypes.bool,
};

export default RewardAsignationForm;

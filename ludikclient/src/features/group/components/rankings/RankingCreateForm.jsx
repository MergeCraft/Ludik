// RankingCreateForm.jsx
import React, { useState } from "react";
import PropTypes from "prop-types";
import styles from "./RankingCreateForm.module.css";
import * as Toast from "../../../../lib/toastify";
import { useMedallasProfesor } from "../../../medals/hooks/useMedalMutation";
import { useCrearRanking } from "../../hooks/useGrupoMutation";

const RankingCreateForm = ({ onClose, groupId }) => {
  const [ranking, setRanking] = useState({ nombre: "", medallaId: "" });
  const { data: medallas, isLoading } = useMedallasProfesor(true);
  const { mutateAsync: crearRanking, isLoading: isCreating } = useCrearRanking();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRanking((prev) => ({ ...prev, [name]: value }));
  };

  const validar = () => {
    if (ranking.nombre.trim().length < 3) {
      Toast.notificarWarning("El nombre debe tener al menos 3 caracteres.");
      return false;
    }
    if (!ranking.medallaId) {
      Toast.notificarWarning("Debes seleccionar una medalla.");
      return false;
    }
    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validar()) return;

    try {
      await crearRanking({
        grupoId: groupId,
        ranking: {
          nombre: ranking.nombre,
          medallaAsociadaId: Number(ranking.medallaId),
        },
      });
      onClose();
    } catch (error) {
      console.error("Error al crear el ranking:", error);
      Toast.notificarError("Hubo un error al crear el ranking. Intenta nuevamente.");
    }
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Nombre del ranking
        <input type="text" name="nombre" placeholder="Ej: Ranking mensual" value={ranking.nombre} onChange={handleChange} />
      </label>

      <label>
        Medalla asociada
        <div className={styles.selectWrapper}>
          <select name="medallaId" value={ranking.medallaId} onChange={handleChange} disabled={isLoading}>
            <option value="">Seleccionar</option>
            {isLoading ? (
              <option disabled>Cargando...</option>
            ) : (
              medallas?.map((medalla) => (
                <option key={medalla.id} value={medalla.id}>
                  {medalla.nombre}
                </option>
              ))
            )}
          </select>
        </div>
      </label>

      <button type="submit" className={`${styles.btnSubmit} button-secondary`} disabled={isCreating}>
        {isCreating ? "Creando..." : "Crear ranking"}
      </button>
    </form>
  );
};

RankingCreateForm.propTypes = {
  onClose: PropTypes.func.isRequired,
  groupId: PropTypes.number.isRequired,
};

export default RankingCreateForm;

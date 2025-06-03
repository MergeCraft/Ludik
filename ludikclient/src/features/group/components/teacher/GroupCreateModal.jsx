import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./GroupCrateModal.module.css";
import * as Toast from "../../../../lib/toastify.js";
import { useCrearGrupo } from "../../hooks/useGrupoMutation.js";

export const GroupCreateModal = ({ onClose }) => {
  const [grupo, setGrupo] = useState({
    nombre: "",
    institucion: "",
    materia: "",
    tablaEquivalenciaId: "",
  });

  const { mutateAsync: crear } = useCrearGrupo(onClose);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setGrupo((prev) => ({ ...prev, [name]: value }));
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
  };

  return (
    <div className={styles.modalBackdrop}>
      <section className={styles.modal}>
        <div className={styles.modalHeader}>
          <h2>Crear nuevo grupo</h2>
          <button className={styles.closeButton} onClick={onClose} aria-label="Cerrar">
            <FontAwesomeIcon icon="fa-solid fa-xmark" />
          </button>
        </div>

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
                <option value="1">Rúbrica 1</option>
                <option value="2">Rúbrica 2</option>
                {/* Podés cargar esto dinámicamente con useQuery */}
              </select>
            </div>
          </label>

          <button type="submit" className={`${styles.btnSubmit} button-secondary`}>
            Crear grupo
          </button>
        </form>
      </section>
    </div>
  );
};

export default GroupCreateModal;

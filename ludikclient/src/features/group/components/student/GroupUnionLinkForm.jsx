import React, { useState } from "react";
import PropTypes from "prop-types";

import styles from "./GroupUnionLinkForm.module.css";

import { useSolicitarUnirseGrupo } from "../../hooks/useGrupoMutation";

const GroupUnionLinkModal = ({ onClose }) => {
  const [codigo, setCodigo] = useState("");

  const { mutate: unirse, isLoading } = useSolicitarUnirseGrupo(onClose);

  const handleSubmit = (e) => {
    e.preventDefault();

    if (codigo.trim().length < 5) {
      alert("Ingrese un código válido.");
      return;
    }

    unirse(codigo.trim()); // Envía el CODIGO
  };

  return (
    <form className={styles.modalFormLink} onSubmit={handleSubmit}>
      <label>
        Código del Grupo
        <input type="text" name="codigo" value={codigo} onChange={(e) => setCodigo(e.target.value)} disabled={isLoading} placeholder="Ingrese el código del grupo" />
      </label>

      <div className={styles.acciones}>
        <button type="submit" disabled={isLoading || codigo.trim().length < 5} className="button-secondary">
          Unirme al Grupo
        </button>

        <button type="button" disabled={isLoading} onClick={onClose} className="button-secondary">
          Cancelar
        </button>
      </div>
    </form>
  );
};

GroupUnionLinkModal.propTypes = {
  onClose: PropTypes.func.isRequired,
};

export default GroupUnionLinkModal;

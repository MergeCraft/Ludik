import React from "react";
import PropTypes from "prop-types";
import styles from "./GroupConfigView.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import * as Toast from "../../../../lib/toastify";
import { useEliminarGrupo, useReiniciarLogrosGrupo } from "../../hooks/useGrupoMutation";
import GroupCreateForm from "../teacher/GroupCreateForm";

const GroupConfigView = ({ id, group, setModalContent, setModalTitle, setShowModal }) => {
  const { nombre, materia, institucion, fCreacion, urlCompleta } = group;

  const eliminarGrupo = useEliminarGrupo();
  const reiniciarLogros = useReiniciarLogrosGrupo();

  const handleEliminar = () => {
    const confirmado = window.confirm(`¿Seguro que quieres eliminar el grupo "${nombre}"?`);
    if (confirmado) eliminarGrupo.mutate(id);
  };

  const handleReiniciarLogros = () => {
    const confirmado = window.confirm(`¿Deseas reiniciar los logros del grupo "${nombre}"?`);
    if (confirmado) reiniciarLogros.mutate(id);
  };

  const handleEditar = () => {
    setModalContent(<GroupCreateForm grupoInicial={group} idGrupo={id} onClose={() => setShowModal(false)} />);
    setModalTitle("Editar grupo");
    setShowModal(true);
  };

  const handleCopy = () => {
    navigator.clipboard.writeText(urlCompleta);
    Toast.notificarExito("Enlace copiado!");
  };

  return (
    <div className={styles.configsContainer}>
      <div className={styles.propsContainer}>
        <h3>Información</h3>
        <div className={styles.props}>
          <label>
            <strong>Nombre</strong> {nombre}
          </label>
          <label>
            <strong>Materia</strong> {materia}
          </label>
          <label>
            <strong>Institución</strong> {institucion}
          </label>
          <label>
            <strong>Fecha de creación</strong> {new Date(fCreacion).toLocaleDateString()}
          </label>
          <label className={styles.linkContainer}>
            <strong>Enlace de invitación</strong>
            <a className={styles.link} href={urlCompleta} target="_blank" rel="noopener noreferrer">
              {urlCompleta}
            </a>
            <button aria-label="Copiar link" onClick={handleCopy} className={styles.copyBtn}>
              <FontAwesomeIcon icon="fa-solid fa-copy" />
            </button>
          </label>
        </div>
      </div>

      <div className={styles.actionsContainer}>
        <h3>Acciones</h3>
        <div className={styles.actions}>
          <button className="button" onClick={handleEditar}>
            Editar grupo
          </button>
          <button className="button-quinary" onClick={handleReiniciarLogros}>
            Reiniciar logros
          </button>
          <button className="button-tertiary" onClick={handleEliminar}>
            Eliminar grupo
          </button>
        </div>
      </div>
    </div>
  );
};

GroupConfigView.propTypes = {
  id: PropTypes.number.isRequired,
  group: PropTypes.shape({
    nombre: PropTypes.string.isRequired,
    tablaEquivalenciaId: PropTypes.number.isRequired,
    tablaEquivalenciaNotaMaxima: PropTypes.number.isRequired,
    profesorId: PropTypes.string.isRequired,
    institucion: PropTypes.string.isRequired,
    materia: PropTypes.string.isRequired,
    fCreacion: PropTypes.string.isRequired,
    urlCompleta: PropTypes.string.isRequired,
    idTienda: PropTypes.number.isRequired,
  }).isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
  setShowModal: PropTypes.func.isRequired,
};

export default GroupConfigView;

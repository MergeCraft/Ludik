import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import styles from "./ThresholdCreateForm.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useCrearUmbralMedalla, useEditarUmbralMedalla, useEliminarUmbralMedalla } from "../../hooks/useGrupoMutation";
import { notificarError } from "../../../../lib/toastify";

const ThresholdCreateForm = ({ grupoId, medallas, tiposKudo, onClose, initialValues = null }) => {
  const [medallaId, setMedallaId] = useState("");
  const [tipoKudoId, setTipoKudoId] = useState("");
  const [cantidadKudos, setCantidadKudos] = useState("");

  // Si recibo initialValues, cargo los valores al montar/actualizar el componente
  useEffect(() => {
    if (initialValues) {
      setMedallaId(initialValues.medallaId);
      setTipoKudoId(initialValues.tipoKudoId);
      setCantidadKudos(initialValues.cantidadKudos);
    }
  }, [initialValues]);

  const { mutate: crearUmbral, isLoading: isLoadingCrear } = useCrearUmbralMedalla(() => {
    onClose?.();
  });

  const { mutate: editarUmbral, isLoading: isLoadingEditar } = useEditarUmbralMedalla(() => {
    onClose?.();
  });

  const { mutate: eliminarUmbral, isLoading: isLoadingEliminar } = useEliminarUmbralMedalla(() => {
    onClose?.();
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!medallaId) return notificarError("Por favor, selecciona una medalla.");
    if (!tipoKudoId) return notificarError("Por favor, selecciona un tipo de kudo.");
    const cantidad = Number(cantidadKudos);
    if (!cantidad || cantidad < 1) return notificarError("Ingresa una cantidad válida de kudos.");

    // Buscar datos de medalla seleccionada
    const medallaSeleccionada = medallas.find((m) => m.id === Number(medallaId));
    if (!medallaSeleccionada) return notificarError("Medalla seleccionada inválida.");

    // Buscar datos de tipo kudo seleccionado
    const tipoKudoSeleccionado = tiposKudo.find((k) => k.id === Number(tipoKudoId));
    if (!tipoKudoSeleccionado) return notificarError("Tipo de kudo seleccionado inválido.");

    const data = {
      id: initialValues?.id || 0, // si es edición, usar id, sino 0 o no enviar (según API)
      medallaId: medallaSeleccionada.id,
      medallaNombre: medallaSeleccionada.nombre,
      rutaIconoMedalla: medallaSeleccionada.rutaIconoMedalla || "", // O el campo que corresponda
      tipoKudoId: tipoKudoSeleccionado.id,
      tipoKudoNombre: tipoKudoSeleccionado.nombre,
      cantidadKudos: cantidad,
    };

    if (initialValues && initialValues.id) {
      editarUmbral(data);
    } else {
      // En creación quizá no necesitas id ni nombres (según API)
      const crearData = {
        medallaId: medallaSeleccionada.id,
        tipoKudoId: tipoKudoSeleccionado.id,
        grupoId,
        cantidadKudos: cantidad,
      };
      crearUmbral(crearData);
    }
  };

  const handleDelete = () => {
    if (!initialValues?.id) return;
    if (window.confirm("¿Estás seguro de que deseas eliminar este umbral?")) {
      eliminarUmbral(initialValues.id);
    }
  };

  const isLoading = isLoadingCrear || isLoadingEditar || isLoadingEliminar;

  return (
    <form className={styles.formThreshold} onSubmit={handleSubmit}>
      <label>
        Medalla:
        <select value={medallaId} onChange={(e) => setMedallaId(e.target.value)} required>
          <option value="" disabled>
            Selecciona una medalla
          </option>
          {medallas.map((m) => (
            <option key={m.id} value={m.id}>
              {m.nombre}
            </option>
          ))}
        </select>
      </label>

      <label>
        Tipo de Kudo:
        <select value={tipoKudoId} onChange={(e) => setTipoKudoId(e.target.value)} required>
          <option value="" disabled>
            Selecciona un tipo de kudo
          </option>
          {tiposKudo.map((k) => (
            <option key={k.id} value={k.id}>
              {k.nombre}
            </option>
          ))}
        </select>
      </label>

      <label>
        Cantidad de kudos:
        <input type="number" min={1} value={cantidadKudos} onChange={(e) => setCantidadKudos(e.target.value)} required />
      </label>

      <div className={styles.accionesThreshold}>
        <button type="submit" className="button-secondary" disabled={isLoading}>
          {isLoading ? "Guardando..." : initialValues ? "Guardar cambios" : "Crear umbral"}
        </button>
        {initialValues && (
          <button
            type="button" // importante que no sea "submit"
            className="button-tertiary"
            onClick={handleDelete} // aquí llamas a la función para eliminar
            disabled={isLoading}
            title="Eliminar umbral"
          >
            <FontAwesomeIcon icon="fa-solid fa-trash" size="lg" />
          </button>
        )}
      </div>
    </form>
  );
};

ThresholdCreateForm.propTypes = {
  grupoId: PropTypes.number.isRequired,
  medallas: PropTypes.array.isRequired,
  tiposKudo: PropTypes.array.isRequired,
  onClose: PropTypes.func,
  initialValues: PropTypes.object,
};

export default ThresholdCreateForm;

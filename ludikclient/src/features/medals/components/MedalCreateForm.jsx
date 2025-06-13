// MedalCreateForm.jsx
import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import styles from "./MedalCreateForm.module.css";
import * as Toast from "../../../lib/toastify.js";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useCrearMedalla, useEditarMedalla, useObtenerMedallaPorId, useEliminarMedalla } from "../hooks/useMedalMutation.js";

const MedalCreateForm = ({ onClose, medalId }) => {
  const [medalla, setMedalla] = useState({
    urlImagen: "",
    nombre: "",
    descripcion: "",
    cantidadMonedasBrinda: "",
    esAsignacionMutua: false,
  });

  // Carga de medalla si estamos editando
  const { data, isFetching } = useObtenerMedallaPorId(medalId);

  // Mutaciones
  const crear = useCrearMedalla(() => {
    Toast.notificarExito("Medalla creada con éxito.");
    onClose?.();
  });

  const editar = useEditarMedalla(() => {
    Toast.notificarExito("Medalla actualizada con éxito.");
    onClose?.();
  });

  // Hook para eliminar
  const eliminar = useEliminarMedalla({
    onSuccess: () => {
      onClose?.();
    },
    onError: (error) => {
      Toast.notificarError(error?.message ?? "Error al eliminar.");
    },
  });

  useEffect(() => {
    if (medalId && data) {
      setMedalla({
        urlImagen: data.urlImagen ?? "",
        nombre: data.nombre ?? "",
        descripcion: data.descripcion ?? "",
        cantidadMonedasBrinda: String(data.cantidadMedallasBrinda ?? "0"),
        esAsignacionMutua: data.esAsignacionMutua ?? false,
      });
    }
  }, [medalId, data]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    const val = type === "checkbox" ? checked : value;
    setMedalla((prev) => ({
      ...prev,
      [name]: val,
    }));
  };

  const validar = () => {
    const campos = [
      ["Nombre de la medalla", medalla.nombre],
      ["Descripción", medalla.descripcion],
      ["URL de imagen", medalla.urlImagen],
    ];

    for (const [campo, valor] of campos) {
      if (!valor || valor.trim().length < 3) {
        Toast.notificarWarning(`El campo "${campo}" debe tener al menos 3 caracteres.`);
        return false;
      }
    }

    if (!medalla.cantidadMonedasBrinda || isNaN(medalla.cantidadMonedasBrinda)) {
      Toast.notificarWarning("Cantidad de monedas debe ser un número válido.");
      return false;
    }

    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validar()) return;

    const payload = {
      ...medalla,
      cantidadMonedasBrinda: Number(medalla.cantidadMonedasBrinda),
    };

    try {
      if (medalId) {
        await editar.mutateAsync({ id: medalId, ...payload });
      } else {
        await crear.mutateAsync(payload);
      }
    } catch (error) {
      console.error(error);
      Toast.notificarError(error?.message ?? "Error al guardar.");
    }
  };

  const handleDelete = () => {
    if (!medalId) return;
    if (window.confirm("¿Estás seguro que deseas eliminar esta medalla? Esta acción no se puede deshacer.")) {
      eliminar.mutate(medalId);
    }
  };

  const isLoading = crear.isLoading || editar.isLoading || eliminar.isLoading;

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      {isFetching ? (
        <p>Cargando...</p>
      ) : (
        <>
          <label>
            Nombre
            <input type="text" name="nombre" value={medalla.nombre} onChange={handleChange} placeholder="Ej: Estrella de participación" disabled={isLoading} />
          </label>

          <label>
            Descripción
            <input type="text" name="descripcion" value={medalla.descripcion} onChange={handleChange} placeholder="Ej: Se otorga por participar activamente" disabled={isLoading} />
          </label>

          <label>
            URL de imagen
            <input type="text" name="urlImagen" value={medalla.urlImagen} onChange={handleChange} placeholder="https://..." disabled={isLoading} />
          </label>

          <label>
            Monedas otorgadas
            <input type="number" name="cantidadMonedasBrinda" value={medalla.cantidadMonedasBrinda} onChange={handleChange} placeholder="Ej: 50" disabled={isLoading} />
          </label>

          <div className={styles.asignacionMutua}>
            <label className="switch">
              <input type="checkbox" name="esAsignacionMutua" checked={medalla.esAsignacionMutua} onChange={handleChange} disabled={isLoading} />
              <span className="slider"></span>
            </label>
            <label htmlFor="esAsignacionMutua" className={styles.recuerdame}>
              ¿Es de asignación mutua?
            </label>
          </div>

          <div className={styles.botones}>
            <button type="submit" disabled={isLoading} className={`button-secondary ${styles.btnSubmit}`}>
              {medalId ? "Guardar Cambios" : "Crear Medalla"}
            </button>

            {medalId && (
              <button type="button" disabled={isLoading} onClick={handleDelete} className={`button-tertiary ${styles.btnDelete}`}>
                <FontAwesomeIcon icon="fa-solid fa-trash" />
              </button>
            )}
          </div>
        </>
      )}
    </form>
  );
};

MedalCreateForm.propTypes = {
  onClose: PropTypes.func,
  medalId: PropTypes.number,
};

export default MedalCreateForm;

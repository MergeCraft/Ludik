import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./GroupUnionLinkForm.module.css";

export const GroupUnionLinkModal = () => {
  const [grupo, setGrupo] = useState({
    nombre: "",
    institucion: "",
    materia: "",
    tablaEquivalenciaId: "",
  });

  const [showQrOptions, setShowQrOptions] = useState(false);

  const handleChange = (e) => {
    setGrupo((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    // tu lógica aquí
  };

  const toggleQrOptions = (e) => {
    e.preventDefault();
    setShowQrOptions((prev) => !prev);
  };

  return (
    <form className={styles.modalForm} onSubmit={handleSubmit}>
      <label>
        Enlace de unión
        <input type="text" name="nombre" placeholder="https://ejemplo.com/grupo/codigo" value={grupo.nombre} onChange={handleChange} />
      </label>

      <div className={styles.acciones} style={{ position: "relative" }}>
        <button type="submit" className={`${styles.btnSubmit} button-secondary`}>
          Solicitar unión
        </button>

        <div className={styles.qrWrapper}>
          <button className={`button-secondary`} onClick={toggleQrOptions} aria-label="Opciones QR">
            <FontAwesomeIcon icon="fa-solid fa-qrcode" size="lg" />
          </button>

          {/* Botones flotantes */}
          <div className={`${styles.qrOptions} ${showQrOptions ? styles.show : ""}`} aria-hidden={!showQrOptions}>
            <button type="button" className={`${styles.qrOptionBtn} button-secondary`} title="Escanear con cámara" onClick={() => alert("Aquí lanzarás el escáner de cámara")}>
              <FontAwesomeIcon icon="fa-solid fa-camera" size="lg" />
            </button>

            <button type="button" className={`${styles.qrOptionBtn} button-secondary`} title="Subir imagen" onClick={() => alert("Aquí lanzarás selector de archivo")}>
              <FontAwesomeIcon icon="fa-solid fa-file-upload" size="lg" />
            </button>
          </div>
        </div>
      </div>
    </form>
  );
};

export default GroupUnionLinkModal;

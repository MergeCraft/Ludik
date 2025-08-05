// MedalCard.jsx
import React, { useState, useRef, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import PropTypes from "prop-types";
import styles from "./MedalCard.module.css";
import DefaultMedalImage1 from "../../../assets/DefaultMedal.png";
import DefaultMedalImage2 from "../../../assets/DefaultMedal2.png";
import DefaultMedalImage3 from "../../../assets/DefaultMedal3.png";

const MedalCard = ({ nombre, descripcion, nombreIcono, cantidadMedallasBrinda, esAsignacionMutua, onEdit, showEditOption }) => {
  const [showPopoverTitulo, setShowPopoverTitulo] = useState(false);
  const [showPopoverDesc, setShowPopoverDesc] = useState(false);
  const popoverTituloRef = useRef(null);
  const popoverDescRef = useRef(null);
  const defaultMedalImages = [DefaultMedalImage1, DefaultMedalImage2, DefaultMedalImage3];

  useEffect(() => {
    const handleClickOutside = (e) => {
      if (popoverTituloRef.current && !popoverTituloRef.current.contains(e.target)) {
        setShowPopoverTitulo(false);
      }
      if (popoverDescRef.current && !popoverDescRef.current.contains(e.target)) {
        setShowPopoverDesc(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  return (
    <div className={`${styles.medallaCard} ${!showEditOption ? styles.bottomPadding : ""}`}>
      <div className={styles.medallaContainer}>
        <div className={styles.medallaImagenWrapper}>
          <img src={defaultMedalImages[Math.floor(Math.random() * defaultMedalImages.length)]} alt={nombre} className={styles.medallaImagen} />
          {esAsignacionMutua && (
            <span className={styles.asignacionLabel}>
              <FontAwesomeIcon icon="fa-solid fa-user" />
              <FontAwesomeIcon icon="fa-solid fa-arrow-right-arrow-left" />
              <FontAwesomeIcon icon="fa-regular fa-user" />
            </span>
          )}
        </div>

        <div className={styles.tituloWrapper}>
          <p className={styles.medallaTitle} onClick={() => setShowPopoverTitulo((prev) => !prev)}>
            {nombre}
          </p>
          {showPopoverTitulo && (
            <div ref={popoverTituloRef} className={`${styles.popover} ${styles.popoverTitulo}`}>
              {nombre}
            </div>
          )}
        </div>

        <div className={styles.descripcionWrapper}>
          <p className={styles.medallaDescripcion} onClick={() => setShowPopoverDesc((prev) => !prev)}>
            {descripcion}
          </p>
          {showPopoverDesc && (
            <div ref={popoverDescRef} className={`${styles.popover} ${styles.popoverDescripcion}`}>
              {descripcion}
            </div>
          )}
        </div>

        {showEditOption && (
          <p className={styles.medallaPuntos}>
            <FontAwesomeIcon icon="fa-solid fa-coins" /> {cantidadMedallasBrinda}
          </p>
        )}
      </div>

      {showEditOption && (
        <button className={styles.editBtn} onClick={onEdit}>
          <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" />
        </button>
      )}
    </div>
  );
};

MedalCard.propTypes = {
  nombre: PropTypes.string.isRequired,
  descripcion: PropTypes.string.isRequired,
  nombreIcono: PropTypes.string.isRequired,
  cantidadMedallasBrinda: PropTypes.number.isRequired,
  esAsignacionMutua: PropTypes.bool.isRequired,
  onEdit: PropTypes.func.isRequired,
  showEditOption: PropTypes.bool.isRequired,
};

export default MedalCard;

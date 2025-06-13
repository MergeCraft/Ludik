// MedalCard.jsx
import React, { useState, useRef, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import PropTypes from "prop-types";
import styles from "./MedalCard.module.css";

const MedalCard = ({ nombre, descripcion, urlImagen, cantidadMedallasBrinda, esAsignacionMutua }) => {
  const [showPopover, setShowPopover] = useState(false);
  const popoverRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (e) => {
      if (popoverRef.current && !popoverRef.current.contains(e.target)) {
        setShowPopover(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const handleEdit = () => {};

  return (
    <div className={styles.medallaCard}>
      <div className={styles.medallaContainer}>
        <div className={styles.medallaImagenWrapper}>
          <img src={urlImagen} alt={nombre} className={styles.medallaImagen} />
          {esAsignacionMutua && (
            <span className={styles.asignacionLabel}>
              <FontAwesomeIcon icon="fa-solid fa-user" />
              <FontAwesomeIcon icon="fa-solid fa-arrow-right-arrow-left" />
              <FontAwesomeIcon icon="fa-regular fa-user" />
            </span>
          )}
        </div>

        <p className={styles.medallaTitle}>{nombre}</p>

        <div className={styles.descripcionWrapper}>
          <p className={styles.medallaDescripcion} onClick={() => setShowPopover((prev) => !prev)}>
            {descripcion}
          </p>
          {showPopover && (
            <div ref={popoverRef} className={styles.popover}>
              {descripcion}
            </div>
          )}
        </div>

        <p className={styles.medallaPuntos}>
          <FontAwesomeIcon icon="fa-solid fa-coins" /> {cantidadMedallasBrinda}
        </p>
      </div>

      <button className={styles.editBtn}>
        <FontAwesomeIcon icon="fa-solid fa-pen-to-square" size="lg" onClick={handleEdit} />
      </button>
    </div>
  );
};

MedalCard.propTypes = {
  nombre: PropTypes.string.isRequired,
  descripcion: PropTypes.string.isRequired,
  urlImagen: PropTypes.string.isRequired,
  cantidadMedallasBrinda: PropTypes.number.isRequired,
  esAsignacionMutua: PropTypes.bool.isRequired,
};

export default MedalCard;

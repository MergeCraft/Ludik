import React from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import styles from "./EquivalenceTableItem.module.css";

const EquivalenceTableItem = ({ item }) => {
  // Calculamos cuántas equivalencias tiene esta tabla
  const cantidad = item.equivalencias.length;

  // Buscamos la nota más baja y más alta
  const notas = item.equivalencias.map((eq) => eq.nota);
  const notaMasBaja = Math.min(...notas);
  const notaMasAlta = Math.max(...notas);

  return (
    <div className={styles.itemCard}>
      <h4 className={styles.itemTitle}>{item.nombre}</h4>

      <div className={styles.itemInfo}>
        <p>
          <FontAwesomeIcon icon="fa-solid fa-list-ol" /> {cantidad}
        </p>
        <p>
          <FontAwesomeIcon icon="fa-solid fa-arrow-up" />
          {notaMasAlta}
        </p>
        <p>
          <FontAwesomeIcon icon="fa-solid fa-arrow-down" />
          {notaMasBaja}
        </p>
      </div>

      <button className={styles.editBtn}>
        <FontAwesomeIcon icon="fa-solid fa-pen-to-square" />
      </button>
    </div>
  );
};

EquivalenceTableItem.propTypes = {
  item: PropTypes.object.isRequired,
};

export default EquivalenceTableItem;

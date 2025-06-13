import React from "react";
import PropTypes from "prop-types";

import styles from "./EquivalenceTableItem.module.css";

const EquivalenceTableItem = ({ item }) => {
  return (
    <div className={styles.itemCard}>
      <h4 className={styles.itemTitle}>{item.nombre}</h4>
      <p className={styles.itemDescription}>{item.descripcion}</p>
      {/* Aquí podrían ir otras opciones o un botón de edición*/}
    </div>
  );
};

EquivalenceTableItem.propTypes = {
  item: PropTypes.object.isRequired,
};

export default EquivalenceTableItem;
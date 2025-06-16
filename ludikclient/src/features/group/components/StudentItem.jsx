// components/StudentCard.jsx
import React from "react";
import PropTypes from "prop-types";

import styles from "./StudentItem.module.css";

const StudentItem = ({ student }) => {
  return (
    <div className={styles.card}>
      <img src={student.enlaceAvatar} alt="avatar" className={styles.avatar} />
      <h4>Id: {student.estudianteId}</h4>
      <p>Monedas: {student.monedas}</p>
      <p>Valor en el grupo: {student.metaCalificacion}</p>
    </div>
  );
};

StudentItem.propTypes = {
  student: PropTypes.object.isRequired,
};

export default StudentItem;

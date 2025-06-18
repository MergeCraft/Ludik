// components/StudentCard.jsx
import React from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import genericProfileImage from "../../../assets/genericStudentAvatar.png";
import styles from "./StudentItem.module.css";

const StudentItem = ({ student }) => {
  return (
    <div className={styles.card}>
      <img src={student.enlaceAvatar ? student.enlaceAvatar : genericProfileImage} alt="avatar" className={styles.avatar} />
      <p>{student.estudianteId}</p>
      <FontAwesomeIcon icon="fa-solid fa-arrow-right-from-bracket" size="lg" />
    </div>
  );
};

StudentItem.propTypes = {
  student: PropTypes.object.isRequired,
};

export default StudentItem;

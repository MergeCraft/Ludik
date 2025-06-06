import React from "react";
import styles from "./GroupItem.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const GroupItem = ({ name, grade, students, imgSrc }) => {
  return (
    <div className={styles.groupItem}>
      <div className={styles.card}>
        <img className={styles.icon} alt="Group Icon" src={imgSrc} />
        <div className={styles.info}>
          <div className={styles.name}>{name}</div>
          <div className={styles.grade}>{grade}</div>
          <div className={styles.studentCount}>Alumnos: {students}</div>
        </div>
        <FontAwesomeIcon icon="arrow-right-from-bracket" className={styles.enterIcon} />
      </div>
    </div>
  );
};

export default GroupItem;

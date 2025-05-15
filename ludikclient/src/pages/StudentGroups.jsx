import React from "react";
import GroupStudentItem from "../components/GroupStudentItem.jsx";
import styles from "./StudentGroups.module.css";
import genericGroupImage from "../assets/genericGroupImage.png";

const groupData = [
  { name: "Liceo 13", grade: "2° C", students: 32, imgSrc: genericGroupImage },
  { name: "Liceo 11", grade: "4° C", students: 30, imgSrc: genericGroupImage },
  { name: "UTU 15", grade: "1° A", students: 23, imgSrc: genericGroupImage },
  { name: "Liceo 1", grade: "4° A", students: 26, imgSrc: genericGroupImage },
];


export const StudentGroups = () => {
  return (
    <div className={styles.studentGroups}>
      {groupData.map((group, index) => (
        <GroupStudentItem key={index} {...group} />
      ))}
    </div>
  );
};

export default StudentGroups;

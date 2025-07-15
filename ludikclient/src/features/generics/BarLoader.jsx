import React from "react";
import styles from "./BarLoader.module.css";
import { BarLoader as ReactBarLoader } from "react-spinners";

const BarLoader = () => {
  return (
    <div className={styles.barLoaderContainer}>
      <ReactBarLoader color="var(--blanco-secundario)" size={10} />
    </div>
  );
};

export default BarLoader;

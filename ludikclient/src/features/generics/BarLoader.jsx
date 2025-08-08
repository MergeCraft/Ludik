import React from "react";
import styles from "./BarLoader.module.css";
import { BarLoader as ReactBarLoader, PulseLoader as ReactPulseLoader } from "react-spinners";

const BarLoader = () => {
  return (
    <div className={styles.barLoaderContainer}>
      <ReactBarLoader color="var(--blanco-secundario)" size={10} />
    </div>
  );
};

const PulseLoader = () => {
  return (
    <div className={styles.pulseLoaderContainer}>
      <ReactPulseLoader color="var(--blanco-secundario)" size={10} />
    </div>
  );
};

export default BarLoader;
export { PulseLoader };

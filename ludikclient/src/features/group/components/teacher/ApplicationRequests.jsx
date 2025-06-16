import React from "react";
import PropTypes from "prop-types";

import { useSolicitudesUnion } from "../../hooks/useGrupoMutation";

import { BarLoader } from "react-spinners";

import selfStyles from "./ApplicationRequests.module.css";

import styles from "../../../generics/BaseManagerPage.module.css";

import * as Toast from "../../../../lib/toastify";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import RequestItem from "./RequestItem";

const ApplicationRequests = ({ groupId, link }) => {
  // Carga de las solicitudes de unión
  const { data: solicitudes, isLoading } = useSolicitudesUnion(groupId);

  const code = new URL(link).searchParams.get("codigo");

  console.log(solicitudes);

  const handleCopy = () => {
    navigator.clipboard.writeText(link);
    Toast.notificarExito("Enlace copiado!");
  };

  if (isLoading) {
    return (
      <div className={styles.barLoaderContainer}>
        <BarLoader color="#fff" size={10} />
      </div>
    );
  }

  return (
    <div className={selfStyles.applicationRequests}>
      <div className={selfStyles.linkContainer}>
        <p className={selfStyles.link}>{code}</p>
        <button aria-label="Copiar link" onClick={handleCopy} className={selfStyles.copyBtn}>
          <FontAwesomeIcon icon="fa-solid fa-copy" />
        </button>
      </div>

      {solicitudes && solicitudes.length ? (
        <ul>
          {solicitudes.map((item, index) => (
            <li key={index}>
              <RequestItem solicitud={item} />
            </li>
          ))}
        </ul>
      ) : (
        <p>Sin solicitudes de unión</p>
      )}
    </div>
  );
};

ApplicationRequests.propTypes = {
  groupId: PropTypes.number.isRequired,
  link: PropTypes.string.isRequired,
};

export default ApplicationRequests;

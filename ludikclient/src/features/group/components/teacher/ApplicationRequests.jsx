import React from "react";
import PropTypes from "prop-types";

import { useSolicitudesUnion } from "../../hooks/useGrupoMutation";

import styles from "./ApplicationRequests.module.css";

import * as Toast from "../../../../lib/toastify";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import RequestItem from "./RequestItem";
import BarLoader from "../../../generics/BarLoader";

const ApplicationRequests = ({ groupId = null, link = "" }) => {
  // Carga de las solicitudes de unión
  const { data: solicitudes, isLoading } = useSolicitudesUnion(groupId);

  const codigo = new URL(link).searchParams.get("codigo");
  
  const handleCopy = () => {
    navigator.clipboard.writeText(codigo);
    Toast.notificarExito("Enlace copiado!");
  };

  if (isLoading) {
    return (
      <div>
        <BarLoader />
      </div>
    );
  }

  return (
    <div className={styles.applicationRequests}>
      <div className={styles.linkContainer}>
        <p className={styles.link}>{codigo}</p>
        <button aria-label="Copiar link" onClick={handleCopy} className={styles.copyBtn}>
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

import React, { useState } from "react";
import PropTypes from "prop-types";
import { faUser, faPalette, faGlasses, faTshirt, faHatCowboy, faArrowsAltH } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./StudentAvatarEditor.module.css";
import { useInventarioAvatar } from "../../hooks/useStudentMutation";

const tabs = [
  { key: "posicion", label: "Posición", icon: faArrowsAltH },
  { key: "estetica", label: "Estética", icon: faPalette },
  { key: "accesorios", label: "Accesorios", icon: faGlasses },
  { key: "ropa", label: "Ropa", icon: faTshirt },
  { key: "sombrero", label: "Sombrero", icon: faHatCowboy },
];

const StudentAvatarEditor = ({ idPerfil }) => {
  const [selectedTab, setSelectedTab] = useState("posicion");

  const { data: inventario, isLoading, isError, error } = useInventarioAvatar(idPerfil);

  console.log("Inventario de avatar:", inventario);

  if (isLoading) return <p>Cargando inventario...</p>;
  if (isError) return <p>Error: {error?.[0]}</p>;

  const renderTabContent = () => {
    switch (selectedTab) {
      case "posicion":
        return (
          <div className={styles.tabContent}>
            <label>
              Voltear:
              <input type="checkbox" className={styles.checkbox} />
            </label>
            <label>
              Ángulo: 0°
              <input type="range" min="-180" max="180" className={styles.slider} />
            </label>
            <label>
              Zoom: 100%
              <input type="range" min="0" max="200" className={styles.slider} />
            </label>
          </div>
        );
      case "estetica":
        return (
          <div className={styles.tabContent}>
            <label>
              Color de fondo: <input type="color" />
            </label>
            <label>
              Color de piel: <input type="color" />
            </label>
            <label>
              Cejas:
              <select>
                <option>Predeterminado</option>
                <option>Gruesas</option>
                <option>Delgadas</option>
              </select>
            </label>
            <label>
              Ojos:
              <select>
                <option>Predeterminado</option>
                <option>Grandes</option>
                <option>Pequeños</option>
              </select>
            </label>
            <label>
              Boca:
              <select>
                <option>Predeterminado</option>
                <option>Sonriente</option>
                <option>Seria</option>
              </select>
            </label>
            <label>
              Barba:
              <select>
                <option>Sin bello facial</option>
                <option>Barba corta</option>
                <option>Barba larga</option>
              </select>
            </label>
            <label>
              Cabello:
              <select>
                <option>Sin cabello</option>
                <option>Corto</option>
                <option>Largo</option>
              </select>
            </label>
          </div>
        );
      case "accesorios":
        return (
          <div className={styles.tabContent}>
            <label>
              Gafas:
              <select>
                <option>Sin gafas</option>
                <option>Redondas</option>
                <option>Cuadradas</option>
              </select>
            </label>
          </div>
        );
      case "ropa":
        return (
          <div className={styles.tabContent}>
            <label>
              Ropa:
              <select>
                <option>Saco y camisa</option>
                <option>Remera</option>
                <option>Campera</option>
              </select>
            </label>
          </div>
        );
      case "sombrero":
        return (
          <div className={styles.tabContent}>
            <label>
              Sombrero:
              <select>
                <option>Sin sombrero</option>
                <option>Gorra</option>
                <option>Sombrero de copa</option>
              </select>
            </label>
          </div>
        );
      default:
        return null;
    }
  };

  return (
    <div className={styles.editorContainer}>
      <div className={styles.avatarPreview}>
        <span className={styles.avatarText}>[Avatar]</span>
      </div>

      <div className={styles.tabs}>
        {tabs.map((tab) => (
          <label key={tab.key} className={styles.tabLabel}>
            <input type="radio" name="avatarTab" className={styles.radioInput} checked={selectedTab === tab.key} onChange={() => setSelectedTab(tab.key)} />
            <div className={`${styles.tabButton} ${selectedTab === tab.key ? styles.tabActive : ""}`}>
              <FontAwesomeIcon icon={tab.icon} />
            </div>
            <span className={styles.tabText}>{tab.label}</span>
          </label>
        ))}
      </div>

      <div className={styles.tabPanel}>{renderTabContent()}</div>
    </div>
  );
};

StudentAvatarEditor.propTypes = {
  idPerfil: PropTypes.number.isRequired,
};

export default StudentAvatarEditor;

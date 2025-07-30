import React, { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./StudentAvatarEditor.module.css";
import BarLoader from "../../../generics/BarLoader";
import { useInventarioAvatar, useGuardarAvatar } from "../../hooks/useStudentMutation";

const iconosPorTipo = {
  posicion: "arrow-right-arrow-left",
  Encuadre: "bullseye",
  Pelo: "user",
  Barba: "face-smile",
  Ojos: "eye",
  Cejas: "eye",
  Sombrero: "graduation-cap",
  Gafas: "glasses",
  Accesorio: "face-grin-stars",
  Boca: "face-kiss-beam",
  Ropa: "shirt",
  ColorPelo: "palette",
  ColorBarba: "palette",
  ColorCejas: "palette",
  ColorPiel: "palette",
  ColorFondo: "palette",
};

const agruparPorTipoConColores = (items) => {
  const agrupados = {};

  items.forEach((item) => {
    const tipo = item.tipo;

    if (tipo === "ColorPiel" || tipo === "ColorFondo") {
      if (!agrupados["Encuadre"]) agrupados["Encuadre"] = [];
      agrupados["Encuadre"].push(item);
    } else if (tipo.startsWith("Color")) {
      const baseTipo = tipo.replace("Color", "");
      if (!agrupados[baseTipo]) agrupados[baseTipo] = [];
      agrupados[baseTipo].push(item);
    } else {
      if (!agrupados[tipo]) agrupados[tipo] = [];
      agrupados[tipo].push(item);
    }
  });

  return agrupados;
};

const StudentAvatarEditor = ({ idPerfil }) => {
  const [selectedTab, setSelectedTab] = useState("posicion");
  const [selecciones, setSelecciones] = useState({});
  const { data: inventario, isLoading, isError, error } = useInventarioAvatar(idPerfil);

  // Controles específicos de la pestaña 'posicion'
  const [voltear, setVoltear] = useState(false);
  const [rotacion, setRotacion] = useState(0);
  const [zoom, setZoom] = useState(100);

  // Tema de ropa para query string
  const [temaRopa, setTemaRopa] = useState("");

  // Estado para el SVG avatar cargado y loader
  const [avatarSvg, setAvatarSvg] = useState(null);
  const [loadingAvatar, setLoadingAvatar] = useState(false);

  // Hook para guardar avatar
  const guardarAvatarMutation = useGuardarAvatar(idPerfil);

  const handleGuardarAvatar = async () => {
    if (!avatarSvg) return;

    const svgImage = new Image();
    const svgBlob = new Blob([avatarSvg], { type: "image/svg+xml" });
    const url = URL.createObjectURL(svgBlob);

    svgImage.onload = async () => {
      const canvas = document.createElement("canvas");
      canvas.width = svgImage.width || 512;
      canvas.height = svgImage.height || 512;
      const ctx = canvas.getContext("2d");

      // Rellenar con fondo blanco (porque JPEG no admite transparencia)
      ctx.fillStyle = "#ffffff";
      ctx.fillRect(0, 0, canvas.width, canvas.height);

      // Dibujar el SVG sobre el fondo blanco
      ctx.drawImage(svgImage, 0, 0);

      // Convertir canvas a blob JPEG en calidad máxima
      canvas.toBlob(
        async (jpegBlob) => {
          if (!jpegBlob) return;

          const atributosSeleccionados = Object.entries(selecciones)
            .filter(([_, codigo]) => !!codigo)
            .map(([tipo]) => {
              const item = inventario.find((i) => i.tipo === tipo && i.codigoUnico === selecciones[tipo]);
              return item?.id;
            })
            .filter(Boolean);

          const avatarDto = {
            ColorFondo: selecciones.ColorFondo || "",
            Voltear: voltear,
            Rotacion: rotacion,
            Zoom: zoom,
            AtributosIds: atributosSeleccionados,
          };

          guardarAvatarMutation.mutate({ avatarDto, jpegBlob });
          URL.revokeObjectURL(url);
        },
        "image/jpeg",
        1.0 // Calidad máxima
      );
    };

    svgImage.onerror = () => {
      console.error("No se pudo cargar el SVG para convertir a JPEG.");
    };

    svgImage.src = url;
  };

  // Inventario con las opciones para avatar
  const tiposAgrupados = Array.isArray(inventario) ? agruparPorTipoConColores(inventario) : {};

  const tabs = [
    { key: "posicion", label: "Posición", icon: iconosPorTipo["posicion"] },
    ...Object.keys(tiposAgrupados).map((tipo) => ({
      key: tipo,
      label: tipo,
      icon: iconosPorTipo[tipo] || null,
    })),
  ];

  // Cambia selecciones para radios de estilos y colores
  const handleChange = (tipo, codigoUnico) => {
    setSelecciones((prev) => ({ ...prev, [tipo]: codigoUnico }));
  };

  // Genera URL para Dicebear con los parámetros actuales
  const generarUrlAvatar = () => {
    const colorFondo = selecciones.ColorFondo ? `&backgroundColor=${selecciones.ColorFondo}` : "";
    const voltearStr = `&flip=${voltear.toString()}`;
    const rotacionStr = `&rotate=${rotacion}`;
    const zoomStr = `&scale=${zoom}`;

    const colorPiel = selecciones.ColorPiel ? `&skinColor=${selecciones.ColorPiel}` : "";
    const cejas = selecciones.Cejas ? `&eyebrows=${selecciones.Cejas}` : "";
    const ojos = selecciones.Ojos ? `&eyes=${selecciones.Ojos}` : "";
    const boca = selecciones.Boca ? `&mouth=${selecciones.Boca}` : "";

    const barbaVal = selecciones.Barba || "";
    const barba = barbaVal ? `&facialHair=${barbaVal}` : "";
    const colorBarba = barbaVal ? `&facialHairColor=${selecciones.ColorBarba || ""}` : "";
    const probabilidadBarba = barba === "" ? "0" : "100";

    const gorroVal = selecciones.Sombrero || "";
    const sombrero = `&top=${gorroVal}`;
    const colorSombrero = `&hatColor=${selecciones.ColorSombrero || ""}`;

    // Aquí la lógica condicionada igual que en el ejemplo imperativo:
    const pelo = gorroVal !== "" ? "" : `&top=${selecciones.Pelo || ""}`;
    const colorPelo = gorroVal !== "" ? "" : `&hairColor=${selecciones.ColorPelo || ""}`;

    const gafasVal = selecciones.Gafas || "";
    const gafas = gafasVal ? `&accessories=${gafasVal}` : "";
    const colorGafas = gafasVal ? `&accessoriesColor=${selecciones.ColorGafas || ""}` : "";
    const probabilidadGafas = gafas === "" ? "0" : "100";

    const ropa = selecciones.Ropa ? `&clothing=${selecciones.Ropa}` : "";
    const colorRopa = selecciones.ColorRopa ? `&clothesColor=${selecciones.ColorRopa}` : "";
    const tema = temaRopa ? `&clothingGraphic=${temaRopa}` : "";

    return `https://api.dicebear.com/9.x/avataaars/svg?${colorFondo}${voltearStr}${rotacionStr}${zoomStr}${colorPiel}${cejas}${ojos}${boca}${colorGafas}${gafas}${barba}${colorBarba}${pelo}${colorPelo}${sombrero}${colorSombrero}&accessoriesProbability=${probabilidadGafas}${colorRopa}${ropa}${tema}&facialHairProbability=${probabilidadBarba}&radius=50`;
  };

  // useEffect para actualizar avatar cuando cambian selecciones o controles
  useEffect(() => {
    const url = generarUrlAvatar();
    setLoadingAvatar(true);
    fetch(url)
      .then((res) => res.text())
      .then((svg) => {
        setAvatarSvg(svg);
        setLoadingAvatar(false);
      })
      .catch(() => {
        setAvatarSvg(null);
        setLoadingAvatar(false);
      });
  }, [selecciones, voltear, rotacion, zoom, temaRopa]);

  // Lógica para mostrar/ocultar controles, ejemplo con color barba y color cabello
  const mostrarColorBarba = !!selecciones.Barba;
  const mostrarColorCabello = !selecciones.Sombrero && !!selecciones.Pelo;

  // Renderiza contenido según pestaña seleccionada
  const renderTabContent = () => {
    if (selectedTab === "posicion") {
      return (
        <div className={styles.tabContent}>
          <label>
            Voltear:
            <input type="checkbox" className={styles.checkbox} checked={voltear} onChange={(e) => setVoltear(e.target.checked)} />
          </label>
          <label>
            Ángulo: {rotacion}°
            <input type="range" min="0" max="360" className={styles.slider} value={rotacion} onChange={(e) => setRotacion(Number(e.target.value))} />
          </label>
          <label>
            Zoom: {zoom - 100}%
            <input type="range" min="100" max="200" className={styles.slider} value={zoom} onChange={(e) => setZoom(Number(e.target.value))} />
          </label>
        </div>
      );
    }

    const items = tiposAgrupados[selectedTab];
    if (!items) return null;

    const itemsPrincipales = items.filter((item) => !item.tipo.startsWith("Color"));
    const itemsColor = items.filter((item) => item.tipo.startsWith("Color") || selectedTab === "Encuadre");

    if (isLoading) return <BarLoader />;
    if (isError) return <p>Error: {error?.[0]}</p>;

    return (
      <div className={styles.tabContentGrid}>
        {itemsPrincipales.length > 0 && (
          <>
            <h4 className={styles.subTitle}>Estilos</h4>
            <div className={styles.itemGrid}>
              {/* Opción para "Ninguno" */}
              <label className={styles.avatarItemCard}>
                <input
                  type="radio"
                  name={`estilo-${selectedTab}`}
                  value=""
                  checked={selecciones[selectedTab] === ""}
                  onChange={() => handleChange(selectedTab, "")}
                  className={styles.radioInputHidden}
                />
                <div className={styles.avatarNoneOption}>Ninguno</div>
              </label>

              {itemsPrincipales.map((item) => (
                <label key={item.id} className={styles.avatarItemCard}>
                  <input
                    type="radio"
                    name={`estilo-${selectedTab}`}
                    value={item.codigoUnico}
                    checked={selecciones[selectedTab] === item.codigoUnico}
                    onChange={() => handleChange(selectedTab, item.codigoUnico)}
                    className={styles.radioInputHidden}
                  />
                  <img src={`/ruta-a-assets/${item.rutaRecurso}`} alt={item.nombre} />
                  <p>{item.nombre}</p>
                </label>
              ))}
            </div>
          </>
        )}

        {itemsColor.length > 0 && (
          <>
            <h4 className={styles.subTitle}>Colores</h4>
            <div className={styles.itemGrid}>
              {/* Opción para "Ninguno" */}
              <label className={styles.avatarItemCard}>
                <input
                  type="radio"
                  name={`color-${selectedTab}`}
                  value=""
                  checked={selecciones[selectedTab] === ""}
                  onChange={() => handleChange(selectedTab, "")}
                  className={styles.radioInputHidden}
                />
                <div className={styles.colorNoneOption}>Ninguno</div>
              </label>

              {itemsColor.map((item) => (
                <label key={item.id} className={styles.avatarItemCard}>
                  <input
                    type="radio"
                    name={`color-${selectedTab}`}
                    value={item.codigoUnico}
                    checked={selecciones["Color" + selectedTab] === item.codigoUnico}
                    onChange={() => handleChange("Color" + selectedTab, item.codigoUnico)}
                    className={styles.radioInputHidden}
                  />
                  <div className={styles.colorBox} style={{ backgroundColor: item.nombre.toLowerCase() }} title={item.nombre} />
                  <p>{item.nombre}</p>
                </label>
              ))}
            </div>
          </>
        )}

        {/* Ejemplo condicional para controles extras */}
        {selectedTab === "Barba" && !mostrarColorBarba && <p className={styles.infoText}>Selecciona un estilo de barba para elegir el color</p>}
        {selectedTab === "Pelo" && !mostrarColorCabello && <p className={styles.infoText}>Selecciona un estilo de pelo para elegir el color</p>}
      </div>
    );
  };

  return (
    <div className={styles.editorContainer}>
      <div className={styles.avatarPreview} id="avatar">
        {loadingAvatar ? <BarLoader /> : avatarSvg ? <div dangerouslySetInnerHTML={{ __html: avatarSvg }} /> : <span className={styles.avatarText}>[Avatar]</span>}
        <button className={`button-secondary ${styles.confirmButton}`} title="Confirmar selección" onClick={handleGuardarAvatar} disabled={guardarAvatarMutation.isLoading}>
          <FontAwesomeIcon icon="floppy-disk" />
        </button>
      </div>

      <div className={styles.tabs}>
        {tabs.map((tab) => (
          <label key={tab.key} className={styles.tabLabel}>
            <input type="radio" name="avatarTab" className={styles.radioInput} checked={selectedTab === tab.key} onChange={() => setSelectedTab(tab.key)} />
            <div className={`${styles.tabButton} ${selectedTab === tab.key ? styles.tabActive : ""}`}>{tab.icon && <FontAwesomeIcon icon={["fas", tab.icon]} />}</div>
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

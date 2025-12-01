// GroupProfileView.jsx
import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import PropTypes from "prop-types";
import BarLoader from "../../../generics/BarLoader";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import { notificarExito, notificarWarning } from "../../../../lib/toastify";

import { useRecompensasPerfil, useImagenPerfil, useBarraProgresoPerfil, useDefinirMetaCalificacion, useSolicitarMedalla } from "../../hooks/useStudentMutation";
import { useMedallasAlumno } from "../../../medals/hooks/useMedalMutation";
import RewardItem from "../../../rewards/components/RewardItem";
import MedalCard from "../../../medals/components/MedalCard";
import StudentAvatarEditor from "./StudentAvatarEditor";
import { selectPathChosed, setPathChosed } from "../../../generics/hooks/iuSlice";

import styles from "./GroupProfileView.module.css";
import lvl1 from "../../../../assets/lvl1.PNG";
import lvl2 from "../../../../assets/lvl2.PNG";
import lvl3 from "../../../../assets/lvl3.PNG";
import lvl4 from "../../../../assets/lvl4.PNG";
import lvl5 from "../../../../assets/lvl5.PNG";
import lvl6 from "../../../../assets/lvl6.PNG";
import lvl7 from "../../../../assets/lvl7.PNG";
import lvl8 from "../../../../assets/lvl8.PNG";
import lvl9 from "../../../../assets/lvl9.PNG";
import lvl10 from "../../../../assets/lvl10.PNG";
import imagenDefaultPerfil from "../../../../assets/genericStudentAvatar2.png";
import { PulseLoader } from "../../../generics/BarLoader";

const GroupProfileView = ({ perfil, groupid, isLoading, setModalContent, setModalTitle, setShowModal, isOwnProfile }) => {
  const { data: recompensas, isLoading: isLoadingRecompensas } = useRecompensasPerfil(perfil?.id);
  const { data: imagenPerfil, isLoading: isLoadingImagen, isError: isErrorImagen } = useImagenPerfil(perfil?.id);
  const { data: barraProgreso, isLoading: isLoadingBarra } = useBarraProgresoPerfil(perfil?.id);
  const { data: medallas, isLoading: isLoadingMedallas } = useMedallasAlumno(groupid);
  const { mutate: setMeta, isPending: isSettingMeta } = useDefinirMetaCalificacion(perfil?.id);
  const { mutate: solicitarMedallaMutate, isPending: isSolicitando } = useSolicitarMedalla(perfil?.id);

  const [metaTemporal, setMetaTemporal] = useState(perfil.metaCalificacion);
  const [editandoMeta, setEditandoMeta] = useState(false);

  const [medallaSeleccionada, setMedallaSeleccionada] = useState("");
  const [mensajeSolicitud, setMensajeSolicitud] = useState("");

  const dispatch = useDispatch();

  const [editandoEstilo, setEditandoEstilo] = useState(false);

  const [matrizEstilos] = useState([
    {
      id: 1,
      estilo: "Aventura Épica",
      metaTitulo: "Camino del héroe",
      niveles: ["Novato", "Explorador", "Aventurero", "Guerrero", "Héroe", "Campeón", "Señor", "Maestro", "Sabio", "Leyenda"],
    },
    {
      id: 2,
      estilo: "Científico Loco",
      metaTitulo: "Ruta del conocimiento",
      niveles: ["Observador", "Investigador", "Técnico", "Analista", "Desarrollador", "Innovador", "Experto", "Arquitecto", "Pionero", "Visionario"],
    },
    {
      id: 3,
      estilo: "Artista Creativo",
      metaTitulo: "Viaje creativo",
      niveles: ["Soñador", "Aprendiz", "Creador", "Diseñador", "Artista", "Inspirador", "Curador", "Maestro", "Genio", "Icono"],
    },
  ]);

  // 👇 lo traemos de Redux (por usuario)
  const pathChosed = useSelector(selectPathChosed); // 1 | 2 | 3 | null
  const estiloSeleccionado = pathChosed ?? 1; // si no hay nada, usamos 1 por defecto

  const [estiloSeleccionadoTemporal, setEstiloSeleccionadoTemporal] = useState(estiloSeleccionado);

  const levelImages = [lvl1, lvl2, lvl3, lvl4, lvl5, lvl6, lvl7, lvl8, lvl9, lvl10];

  // si cambia en Redux (ej, al loguear y cargar config), sincronizamos el temporal
  useEffect(() => {
    setEstiloSeleccionadoTemporal(estiloSeleccionado);
  }, [estiloSeleccionado]);

  useEffect(() => {
    setMetaTemporal(perfil.metaCalificacion);
  }, [perfil.metaCalificacion]);

  const avatarUrl = imagenPerfil?.urlCompleta;

  useEffect(() => {
    try {
      if (!perfil?.id || typeof perfil?.metaCalificacion !== "number" || !barraProgreso) return;

      const actual = Number(barraProgreso.calificacionActual ?? -Infinity);
      const meta = Number(perfil.metaCalificacion);
      const key = `metaReached_${perfil.id}_${meta}`;

      if (actual >= meta && !localStorage.getItem(key)) {
        localStorage.setItem(key, "1");

        notificarExito(`¡Felicidades ${perfil.nombreEstudiante}! Has alcanzado tu meta: ${meta}.`);
      }
    } catch (err) {
      console.error(err);
    }
  }, [barraProgreso, barraProgreso?.calificacionActual, perfil?.metaCalificacion, perfil?.id, perfil?.nombreEstudiante, setModalContent, setModalTitle, setShowModal]);

  const handleSolicitarMedalla = () => {
    let error = false;

    if (medallaSeleccionada === "") {
      notificarWarning("Debes seleccionar una medalla");
      error = true;
    }

    if (mensajeSolicitud.length === 0) {
      notificarWarning("El mensaje de solicitud no puede estar vacío");
      error = true;
    }

    if (error) return;

    solicitarMedallaMutate(
      {
        medallaId: Number(medallaSeleccionada),
        descripcion: mensajeSolicitud || "Sin descripción",
      },
      {
        onSuccess: () => {
          setMedallaSeleccionada("");
        },
      }
    );
  };

  return isLoading ? (
    <BarLoader />
  ) : (
    <div className={styles.container}>
      <section>
        <h3>Avatar</h3>
        {isLoadingImagen && !imagenPerfil && !isErrorImagen ? <BarLoader /> : <img src={avatarUrl || imagenDefaultPerfil} alt={`Avatar de ${perfil.nombreEstudiante}`} className={styles.avatar} />}
        {isOwnProfile && (
          <button
            className={styles.editIconContainer}
            onClick={() => {
              setModalContent(<StudentAvatarEditor idPerfil={perfil.id} />);
              setModalTitle("Personalizar avatar");
              setShowModal(true);
            }}
          >
            <FontAwesomeIcon icon="fa fa-pen-to-square" />
          </button>
        )}
      </section>

      <section>
        <h3>Nombre</h3>
        <p>{perfil.nombreEstudiante}</p>
      </section>

      <section>
        <h3>Resumen</h3>
        <div className={styles.resumen}>
          <div>
            <p>
              <FontAwesomeIcon icon="fa fa-coins" /> {perfil.monedas}
            </p>
            <p>Monedas disponibles</p>
          </div>
          <div>
            <p>
              <FontAwesomeIcon icon="fa fa-award" /> {(perfil.medallas || []).reduce((acc, medalla) => acc + (medalla.cantidad || 0), 0)}
            </p>
            <p>Medallas obtenidas</p>
          </div>
          {isOwnProfile && (
            <div className={styles.metaDisplay}>
              {editandoMeta ? (
                // MODO EDICIÓN
                <div className={styles.metaContainer}>
                  <p>¡Elige una nueva meta!</p>
                  <div className={styles.accionesSeleccionMeta}>
                    <input
                      type="number"
                      value={metaTemporal}
                      onChange={(e) => setMetaTemporal(Number(e.target.value))}
                      className={styles.metaInput}
                      min={barraProgreso?.calificacionMinima || 0}
                      max={barraProgreso?.calificacionMaxima || 100}
                    />
                    <button
                      className="button-secondary"
                      onClick={() => {
                        setMeta(metaTemporal);
                        setEditandoMeta(false); // Vuelve a modo vista
                      }}
                      title="Guardar nueva meta"
                    >
                      {isSettingMeta ? <PulseLoader /> : <FontAwesomeIcon icon="fa fa-check" />}
                    </button>
                  </div>
                </div>
              ) : (
                // MODO VISTA
                <div>
                  <p>
                    <FontAwesomeIcon icon="fa fa-bullseye" /> {perfil?.metaCalificacion} -{" "}
                    {matrizEstilos.find((estilo) => estilo.id === Number(estiloSeleccionado))?.niveles[perfil?.metaCalificacion - 1] || "Nivel desconocido"}
                  </p>
                  <p>{matrizEstilos.find((estilo) => estilo.id === Number(estiloSeleccionado))?.metaTitulo ?? "Reto personal"}</p>
                </div>
              )}

              <button onClick={() => setEditandoMeta(!editandoMeta)} className={styles.editIconContainer}>
                <FontAwesomeIcon icon={editandoMeta ? "fa fa-times" : "fa fa-pen-to-square"} />
              </button>
            </div>
          )}
        </div>

        {isOwnProfile && (
          <div className={styles.progressBarManager}>
            <div className={styles.progressBarContainer}>
              <button onClick={() => setEditandoEstilo(!editandoEstilo)} className={`button-secondary ${styles.editStyleButton}`}>
                <span>{editandoEstilo ? "Ver tu Camino" : "Elige tu Camino"}</span> <FontAwesomeIcon icon={editandoEstilo ? "fa fa-map" : "fa fa-route"} />
              </button>
              <p>{matrizEstilos.find((estilo) => estilo.id === Number(estiloSeleccionado))?.metaTitulo}</p>
              {!isLoadingBarra &&
                barraProgreso &&
                (!editandoEstilo ? (
                  <>
                    <div className={styles.progressBar}>
                      {Array.from({ length: barraProgreso?.calificacionMaxima }, (_, index) => {
                        const numero = index + barraProgreso?.calificacionMinima;
                        const alcanzado = numero <= barraProgreso?.calificacionActual;
                        const esMeta = numero === perfil?.metaCalificacion;
                        const estiloActual = matrizEstilos.find((estilo) => estilo.id === Number(estiloSeleccionado));

                        return (
                          <label key={`progreso-${numero}`} htmlFor={`progreso-${numero}`} className={`${styles.label} ${alcanzado ? styles.alcanzado : styles.noAlcanzado}`}>
                            {esMeta ? <FontAwesomeIcon icon="fa fa-bullseye" className={styles.icono} /> : <p>{estiloActual?.niveles[index] ?? ""}</p>}
                          </label>
                        );
                      })}
                    </div>

                    <div className={styles.progressBar}>
                      {Array.from({ length: barraProgreso?.calificacionMaxima }, (_, index) => {
                        const numero = index + barraProgreso?.calificacionMinima;
                        const alcanzado = numero <= barraProgreso?.calificacionActual;
                        const esMeta = numero === perfil?.metaCalificacion;


                        return (
                          <label key={`progreso-${numero}`} htmlFor={`progreso-${numero}`} className={`${styles.labelVisual} ${alcanzado ? styles.visualAlcanzado : styles.visualNoAlcanzado}`}>
                            <img src={levelImages[index]} alt={`Nivel ${index + 1}`}  className={esMeta ? styles.visualMeta : ""}/>
                          </label>
                        );
                      })}
                    </div>
                  </>
                ) : (
                  <div className={styles.medallasSeleccionContainer}>
                    <select name="medallas" id="medallas" className="button" onChange={(e) => setEstiloSeleccionadoTemporal(Number(e.target.value))} value={estiloSeleccionadoTemporal}>
                      <option value="">Selecciona una estilo de progreso</option>

                      {matrizEstilos?.map((estilo) => (
                        <option key={estilo.id} value={estilo.id}>
                          {estilo.estilo}
                        </option>
                      ))}
                    </select>

                    <button
                      className={`button-secondary ${styles.confirmButton}`}
                      onClick={() => {
                        // Guardamos el camino elegido en Redux (y por usuario)
                        dispatch(setPathChosed(estiloSeleccionadoTemporal));
                        setEditandoEstilo(false);
                      }}
                    >
                      ¡Elegir Camino!
                    </button>
                  </div>
                ))}
              <hr />
              {/* Medallas necesarias para la siguiente nota */}
              <div className={styles.medallasNecesariasNotaContainer}>
                <p>Medallas para avanzar</p>
                <div className={styles.medallasNecesariasNotaGrid}>
                  {barraProgreso?.medallasNecesariasParaSiguienteNota?.map((medalla) => (
                    <MedalCard key={medalla.medallaId + medalla.nombre} medal={medalla} onEdit={() => {}} showEditOption={false} />
                  ))}
                </div>
              </div>
            </div>
          </div>
        )}
      </section>

      {/* Inventario de Medallas */}
      <section className={styles.inventorySection}>
        <h3>Inventario de Medallas</h3>
        <div className={styles.medallasGrid}>
          {perfil.medallas.length === 0 ? (
            <p>¡Tu inventario está listo! Gana tu primera medalla para verla aquí.</p>
          ) : (
            perfil.medallas.map((medalla) => <MedalCard key={medalla.medallaId + medalla.nombre} medal={medalla} cantidad={medalla.cantidad} onEdit={() => {}} showEditOption={false} />)
          )}
        </div>
        {isOwnProfile && (
          <>
            <hr />
            <div>
              <h3>Solicitar medalla al profesor</h3>
              <div className={styles.solicitarMedallaContainer}>
                <div className={styles.medallasSeleccionContainer}>
                  <select name="medallas" id="medallas" className="button" onChange={(e) => setMedallaSeleccionada(e.target.value)} value={medallaSeleccionada || ""}>
                    {isLoadingMedallas ? (
                      <option value="">Cargando medallas...</option>
                    ) : (
                      <>
                        <option value="">Selecciona una medalla</option>
                        {medallas?.map((medalla) => (
                          <option key={medalla.id} value={medalla.id}>
                            {medalla.nombre}
                          </option>
                        ))}
                      </>
                    )}
                  </select>

                  <button className="button-secondary" disabled={isLoadingMedallas || medallaSeleccionada === "" || isSolicitando} onClick={handleSolicitarMedalla}>
                    {isSolicitando ? <PulseLoader /> : "Solicitar"}
                  </button>
                </div>

                {medallaSeleccionada !== "" && (
                  <textarea name="mensaje" id="mensaje" value={mensajeSolicitud} onChange={(e) => setMensajeSolicitud(e.target.value)} placeholder="Creo que merezco esta medalla porque..." />
                )}
              </div>
            </div>
          </>
        )}
      </section>

      {/* Recompensas canjeadas */}
      <section>
        <h3>Recompensas</h3>
        {isLoadingRecompensas ? (
          <BarLoader />
        ) : (
          <div className={styles.recompensasGrid}>
            {recompensas?.length > 0 ? (
              recompensas.map((reward, index) => <RewardItem key={`${reward.id}-${index}`} reward={reward} redeemed={true} />)
            ) : (
              <p className={styles.noRewardsMessage}>No tienes recompensas aún.</p>
            )}
          </div>
        )}
      </section>
    </div>
  );
};

GroupProfileView.propTypes = {
  perfil: PropTypes.shape({
    id: PropTypes.number.isRequired,
    avatarGrupoId: PropTypes.number.isRequired,
    enlaceAvatar: PropTypes.string.isRequired,
    metaCalificacion: PropTypes.number.isRequired,
    estudianteId: PropTypes.string.isRequired,
    nombreEstudiante: PropTypes.string.isRequired,
    monedas: PropTypes.number.isRequired,
    grupoId: PropTypes.number.isRequired,
    nombreGrupo: PropTypes.string.isRequired,
    medallas: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number.isRequired,
        nombre: PropTypes.string.isRequired,
        urlImagen: PropTypes.string,
        descripcion: PropTypes.string.isRequired,
        cantidadMedallasBrinda: PropTypes.number.isRequired,
        cantidad: PropTypes.number.isRequired,
        esAsignacionMutua: PropTypes.bool,
      })
    ).isRequired,
  }).isRequired,
  groupid: PropTypes.number.isRequired,
  isLoading: PropTypes.bool.isRequired,
  setModalContent: PropTypes.func.isRequired,
  setModalTitle: PropTypes.func.isRequired,
  setShowModal: PropTypes.func.isRequired,
  isOwnProfile: PropTypes.bool.isRequired,
};

export default GroupProfileView;

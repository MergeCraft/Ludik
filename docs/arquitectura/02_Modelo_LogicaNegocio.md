classDiagram
    %% Herencia de Usuarios
    IdentityUser <|-- Usuario
    Usuario <|-- Profesor
    Usuario <|-- Estudiante
    
    %% Composiciones y Relaciones Principales
    Usuario *-- NombreCompleto
    Profesor "1" --> "*" RecompensaProfesor
    Profesor "1" --> "*" TablaEquivalencia
    Profesor "1" --> "*" Grupo
    TablaEquivalencia "1" --> "*" Equivalencia
    Equivalencia "*" --> "*" Medalla : medallasNecesarias
    Grupo "1" --> "*" TablaClasificacion
    Grupo "1" --> "*" PerfilEstudiante
    Grupo "1" --> "1" Tienda
    Grupo "1" --> "*" SolicitudUnion
    Grupo "1" --> "1" EnlaceUnion
    Grupo "1" --> "*" SolicitudPerfilMedalla
    Grupo "1" --> "*" ProyectoAulaColaborativo
    Estudiante "1" --> "*" PerfilEstudiante
    Estudiante "1" --> "*" PreguntaRespuestaSeguridad
    Estudiante "1" --> "*" Hito
    PerfilEstudiante "1" --> "1" BarraProgreso
    PerfilEstudiante "1" --> "1" Avatar
    PerfilEstudiante "*" --> "*" RendimientoPeriodo
    PerfilEstudiante "*" --> "*" KudoOtorgado
    PerfilEstudiante "*" --> "*" PerfilEstudianteMedalla
    PerfilEstudiante "*" --> "*" PerfilEstudianteRecompensa
    BarraProgreso --> TablaEquivalencia
    Avatar "1" --> "*" AtributoAvatar
    KudoOtorgado --> TipoKudo
    UmbralMedallaPorKudos --> Medalla
    UmbralMedallaPorKudos --> TipoKudo
    TablaClasificacion --> Medalla
    Tienda "1" --> "*" Recompensa
    Hito --> Recompensa
    PerfilEstudianteRecompensa --> Recompensa
    SolicitudUnion --> Estudiante
    EstudiantePotenciador --> Estudiante
    EstudiantePotenciador --> Potenciador

    %% Herencia de Recompensas
    Recompensa <|-- Potenciador
    Recompensa <|-- PersonalizacionAvatar
    Recompensa <|-- Personalizada

    class IdentityUser {
        -id : int
        -securityStamp : string
        -userName : string
        -email : string
        -password : string
    }

    class Usuario {
        -imagenPerfil : string
        -nombreCompleto : NombreCompleto
    }

    class NombreCompleto {
        -nombre : string
        -apellido : string
        +crear(nombre : string, apellido : string) void
        +validarCampo(nombreCampo : string, errores : int, bool : int) void
    }

    class Profesor {
        -medallas : List~Medalla~
        -tablasEquivalencia : List~TablaEquivalencia~
        -grupos : List~Grupo~
        -recompensasCreadas : ICollection~RecompensaProfesor~
        +asignarMedalla(medalla: Medalla, grupo: Grupo, pEstudiante: PerfilEstudiante) void
        +crearRecompensa(recompensa : Recompensa) Resultado
        +tieneRecompensa(recompensaId : int) bool
    }

    class RecompensaProfesor {
        -profesorId : int
        -recompensaId : int
        -fechaCreacion : DateTime
        -id : int
    }

    class TablaEquivalencia {
        -id : int
        -profesorId : int
        -nombre : String
        -equivalencias : List~Equivalencia~
        +maxCalificacionSegunMedallas(List~Medalla~) int
        +obtenerNotaMinima() int
        +obtenerNotaMaxima() int
        +agregarEquivalencia(Equivalencia : nuevaEquivalencia) void
        +actualizar(nuevoNombre: string, equivalencias: List~Equivalencia~) void
        +obtenerMedallasNecesariasParaSiguienteNota(notaActualDelPerfil : int) List~Medalla~
    }

    class Equivalencia {
        -id : int
        -nota : int
        -medallasNecesarias : List~Medalla~
        -tablaEquivalencia : TablaEquivalencia
        +cumpleMedallasNecesarias(medallasObtenidas: IEnumerable~Medalla~) bool
    }

    class Medalla {
        -id : int
        -nombre : String
        -descripcion : String
        -icono : String
        -monedasOtorgadas : int
    }

    class BarraProgreso {
        -id : int
        -valorMin : int
        -valorMax : int
        -tablaEquivalencia : TablaEquivalencia
        +hallarPosicionActual(medallas: List~Medalla~) int
        +hallarValorMax(te: TablaEquivalencia) int
        +hallarValorMin(te: TablaEquivalencia) int
    }

    class RendimientoPeriodoMedalla {
        -id : int
        -RendimientoPeriodoId : int
        -MedallaId : int
        -FechaOtorgada : DateTime
    }

    class UmbralMedallaPorKudos {
        -id : int
        -MedallaId : int
        -CantidadKudos : int
        -TipoKudoId : int
        -GrupoId : int
        +esValido() Resultado
    }

    class TipoKudo {
        -id : int
        -Nombre : string
        -Descripcion : string
        -NombreIcono : string
    }

    class KudoOtorgado {
        -id : int
        -PerfilEstudianteEmisorId : int
        -PerfilEstudianteReceptorId : int
        -TipoKudoId : int
        -FechaOtorgamiento : DateTime
        -PerfilEstudianteMedallaId : int
        -AsignacionMedalla : PerfilEstudianteMedalla
        +MarcarComoUsadoPara(asignacion : PerfilEstudianteMedalla) void
    }

    class RendimientoPeriodo {
        -id : int
        -fInicio : DateTime
        -fFin : DateTime
        -notaObtenida : String
        -medallasObtuvoEstudiante : List~Medalla~
    }

    class PerfilEstudiante {
        -id : int
        -Avatar : Avatar
        -NombreImagenCompleta : string
        -NombreImagenMiniatura : string
        -MetaCalificacion : int
        -EstudianteId : int
        -Monedas : int
        -MedallasObtenidas : List~PerfilEstudianteMedalla~
        -HistorialRendimientoPeriodos : List~RendimientoPeriodo~
        -Inventario : List~Recompensa~
        -BarraProgreso : BarraProgreso
        -KudosOtorgados : List~KudoOtorgado~
        -KudosRecibidos : List~KudoOtorgado~
        +RecibirMedallas(m: Medalla, cantidad: int) void
        +RecibirKudoYEvaluarMedalla(k: KudoOtorgado, u: UmbralMedallaPorKudos) Resultado
        +OtorgarKudo(tk: TipoKudo, pR: PerfilEstudiante) Resultado~KudoOtorgado~
        +EstablecerMetaDeCalificacion(nM: int) Resultado
        +CalcularNotaActual() int
        +ObtenerItemsAvatarDisponibles() List~RecompensaPersonalizacionAvatar~
    }

    class PerfilEstudianteMedalla {
        -id : int
        -PerfilEstudianteId : int
        -MedallaId : int
        -FechaObtencion : DateTime
    }

    class TablaClasificacion {
        -id : int
        -nombre : String
        -medallaAsociada : Medalla
        -participantes : List~PerfilEstudiante~
        -grupoId : int
        +ordenarParticipantesPorMedallaAsociada() void
    }

    class Grupo {
        -id : int
        -Nombre : String
        -Institucion : String
        -Materia : String
        -FCreacion : DateTime
        -TablaEquivalenciaId : int
        -ProfesorId : int
        -TablasClasificacion : List~TablaClasificacion~
        -Alumnos : List~PerfilEstudiante~
        -Solicitudes : List~SolicitudUnion~
        -EnlaceUnionId : int
        -FechaUltimoReinicio : DateTime
        +esValido() Resultado
        +CalcularNotaDeEstudiante(mO: List~Medalla~) int
        +estudiantePertenece(pEstudiante: PerfilEstudiante) bool
        +AgregarSolicitudPerfilMedalla(sL: SolicitudPerfilMedalla) void
        +ContarMedallasEnPeriodo(fIni: DateTime, fFin: DateTime) int
        +ReiniciarMedallasEstudiantes(DateTime desde, DateTime hasta) List~RendimientoPeriodo~
    }

    class PreguntaRespuestaSeguridad {
        -id : int
        -pregunta : String
        -respuesta : String
        +coincide(pRS : PreguntaRespuestaSeguridad) bool
    }

    class Estudiante {
        -perfiles : List~PerfilEstudiante~
        -hitos : List~Hito~
        -preguntasSeguridad : List~PreguntaRespuestaSeguridad~
        +constrastarRespuestas(pRS: PreguntaRespuestaSeguridad) Boolean
    }

    class AtributoAvatar {
        -TipoAtributo : Enum
        -Id : int
        -Nombre : string
        -NombreImagenRecurso : string
        -CodigoUnico : string
    }

    class Avatar {
        -Id : int
        -ColorFondo : string
        -Voltear : bool
        -Rotacion : int
        -Zoom : int
        -PerfilEstudianteId : int
        -AtributosSeleccionados : List~AtributoAvatar~
        +ObtenerAtributo(tipo : TipoAtributo) AtributoAvatar
    }

    class PerfilEstudianteRecompensa {
        -Id : int
        -PerfilEstudianteId : int
        -RecompensaId : int
    }

    class Hito {
        -cantMedallasRequeridas : int
        -recompensa : Recompensa
        +cumple(cantMedallasPerfiles : int) bool
    }

    class Recompensa {
        -id : int
        -nombre : String
        -imagen : String
        -precio : int
        +otorgar(pEstudiante : PerfilEstudiante) void
        +pagar(precio : int) void
    }

    class Potenciador {
        -periodo : DateTime
        -multiplicador : double
    }

    class PersonalizacionAvatar {
        -codigoElemento : String
    }

    class Personalizada {
        -fueReclamada : bool
    }

    class SolicitudUnion {
        -id : int
        -estudiante : Estudiante
    }

    class Tienda {
        -id : int
        -GrupoId : int
        +AgregarRecompensa(recompensa: Recompensa) Resultado
        +AgregarRecompensaPrecargada(recompensa: Recompensa) Resultado
    }

    class EnlaceUnion {
        -id : int
        -codigoBase : String
        -expiracion : DateTime
        +generarCodigoBase() String
    }

    class SolicitudPerfilMedalla {
        -id : int
        -PerfilEstudianteId : int
        -MedallaId : int
        -Estado : Enum
        -GrupoId : int
        -Fecha : DateTime
        -Descripcion : string
        +esValido() Resultado
    }

    class ProyectoAulaColaborativo {
        -Id : int
        -GrupoId : int
        -PerfilEstudianteId : int
        -CantidadMedallasNecesarias : int
        -RecompensaClaseId : int
        -Estado : Enum
        -FechaInicio : DateTime
        -FechaFin : DateTime
        +esValido() Resultado
    }

    class EstudiantePotenciador {
        -Id : int
        -EstudianteId : int
        -PotenciadorId : int
        -FechaActivacion : DateTime
        -EstaActivo : bool
    }
classDiagram
    class IRepositorio~T~ {
        <<interface>>
        +Add(unObjeto : T) void
        +Remove(id : int) void
        +Remove(unObjeto : T) void
        +Update(unObjeto : T) void
        +GetById(id : int) T
        +GetAll() IEnumerable~T~
    }

    class IRepositorioEquivalencias {
        <<interface>>
    }

    class IRepositorioPreguntasSeguridad {
        <<interface>>
    }

    class IRepositorioUsuarios {
        <<interface>>
        +loginUsuario(identificador : String, hashContrasena : String) Usuario
    }

    class IRepositorioPines {
        <<interface>>
    }

    class IRepositorioSolicitudesUnion {
        <<interface>>
    }

    class IRepositorioTablasEquivalencia {
        <<interface>>
    }

    class IRepositorioPerfilEstudianteGrupo {
        +verMedallasAlumno(idAlumno : int, idGrupo : int) List~Medalla~
    }

    class IRepositorioEnlacesUnionGrupo {
        +obtenerCodigoInvitacion(idGrupo : int) String
    }

    class IRepositorioRendimientoPeriodos {
        <<interface>>
        +almacenarLogrosPrevios(idGrupo : int) void
    }

    class IRepositorioGrupos {
        <<interface>>
        -obtenerTablaDelGrupo(idGrupo : int) TablaEquivalencia
        -calcularNotaEstudiante(idAlumno : int, idGrupo : int) int
        +aceptarSolicitud(idSolicitud : SolicitudUnion) void
        +rechazarSolicitud(idSolicitud : SolicitudUnion) void
        +obtenerGruposPorProfesor(idProfesor : int) List~Grupo~
        +unirseAGrupo(idAlumno : int, grupo : Grupo) void
        +obtenerAlumnosDelGrupo(idGrupo : int) List~Estudiante~
        +reiniciarLogrosDeGrupo(idGrupo : int) void
        +obtenerTablasDeClasificacionDeGrupo(idGrupo : int) List~TablaClasificacion~
    }

    class IRepositorioPerfilEstudianteMedalla {
        <<interface>>
    }

    class IRepositorioMedallas {
        <<interface>>
        +obtenerMedallasAsignablesMutuamente(idGrupo : int) List~Medalla~
    }

    class IRepositorioPerfilEstudianteRecompensa {
        <<interface>>
    }

    class IRepositorioTablaClasificacion {
        <<interface>>
    }

    class IRepositorioRecompensas {
        <<interface>>
    }

    class IRepositorioHitos {
        <<interface>>
    }

    class IRepositorioTiendas {
        <<interface>>
    }

    class IRepositorioProfesores {
        <<interface>>
    }

    class IRepositorioEstudiantes {
        <<interface>>
        +asignarMedalla(idAlumno : int, idMedalla : int) void
        +asignarMedallaEntreAlumnos(idAlumnoOrigen : int, idAlumnoDestino : int, idMedalla : int) void
        +quitarMedalla(idAlumno : int, idMedalla : int) void
        +verMedallasAlumno(idAlumno : int, idGrupo : int) List~Medalla~
    }

    IRepositorio~T~ <|-- IRepositorioEquivalencias
    IRepositorio~T~ <|-- IRepositorioPreguntasSeguridad
    IRepositorio~T~ <|-- IRepositorioUsuarios
    IRepositorio~T~ <|-- IRepositorioPines
    IRepositorio~T~ <|-- IRepositorioSolicitudesUnion
    IRepositorio~T~ <|-- IRepositorioTablasEquivalencia
    IRepositorio~T~ <|-- IRepositorioPerfilEstudianteGrupo
    IRepositorio~T~ <|-- IRepositorioEnlacesUnionGrupo
    IRepositorio~T~ <|-- IRepositorioRendimientoPeriodos
    IRepositorio~T~ <|-- IRepositorioGrupos
    IRepositorio~T~ <|-- IRepositorioPerfilEstudianteMedalla
    IRepositorio~T~ <|-- IRepositorioMedallas
    IRepositorio~T~ <|-- IRepositorioPerfilEstudianteRecompensa
    IRepositorio~T~ <|-- IRepositorioTablaClasificacion
    IRepositorio~T~ <|-- IRepositorioRecompensas
    IRepositorio~T~ <|-- IRepositorioHitos
    IRepositorio~T~ <|-- IRepositorioTiendas
    IRepositorio~T~ <|-- IRepositorioProfesores
    IRepositorio~T~ <|-- IRepositorioEstudiantes
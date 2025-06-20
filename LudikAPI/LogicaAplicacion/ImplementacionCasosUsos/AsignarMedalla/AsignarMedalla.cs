using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla;

public class AsignarMedalla: IAsignarMedalla
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiantes;
    private readonly IRepositorioMedallas _repositorioMedallas;
    private readonly IRepositorioProfesores _repositorioProfesores;
    private readonly IRepositorioPerfilEstudianteMedalla _repositorioPerfilEstudianteMedalla;
    public AsignarMedalla(
        IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
        IRepositorioMedallas repositorioMedalla,
        IRepositorioProfesores repositorioProfesor,
        IRepositorioPerfilEstudianteMedalla repositorioPerfilEstudianteMedalla)
    {
        _repositorioPerfilEstudiantes = repositorioPerfilEstudiante;
        _repositorioMedallas = repositorioMedalla;
        _repositorioProfesores = repositorioProfesor;
        _repositorioPerfilEstudianteMedalla = repositorioPerfilEstudianteMedalla;
    }


    public async Task<Resultado> EjecutarAsync(string profesorId, int idPerfilEstudiante, int idMedalla)
    {
        var profesorResultado = await _repositorioProfesores.GetByStringIdAsync(profesorId);
        var perfilResultado = await _repositorioPerfilEstudiantes.GetByIdAsync(idPerfilEstudiante);
        var medallaResultado = await _repositorioMedallas.GetByIdAsync(idMedalla);

        if (profesorResultado.EsFallo) return Resultado.Falla(Error.NotFound);
        if (perfilResultado.EsFallo) return Resultado.Falla(Error.NotFound);
        if (medallaResultado.EsFallo) return Resultado.Falla(Error.NotFound);

        var profesor = profesorResultado.Valor;
        var perfilEstudiante = perfilResultado.Valor;
        var medalla = medallaResultado.Valor;


        if (!profesor.Grupos.Any(g => g.Id == perfilEstudiante.GrupoId))
            return Resultado.Falla(Error.Forbidden);

        // Crear nueva asignación:
        var nuevaAsignacion = new PerfilEstudianteMedalla
        {
            PerfilEstudianteId = perfilEstudiante.Id,
            MedallaId = medalla.Id,
        };
        var addResultado = await _repositorioPerfilEstudianteMedalla.AddAsync(nuevaAsignacion);
        return addResultado;
    }
}
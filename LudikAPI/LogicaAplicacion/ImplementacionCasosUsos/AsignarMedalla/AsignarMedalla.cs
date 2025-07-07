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
    private readonly IObserver<PerfilEstudianteMedalla> _perfilObserver;
    public AsignarMedalla(
        IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
        IRepositorioMedallas repositorioMedalla,
        IRepositorioProfesores repositorioProfesor,
        IRepositorioPerfilEstudianteMedalla repositorioPerfilEstudianteMedalla,
        IObserver<PerfilEstudianteMedalla> perfilObserver)
    {
        _repositorioPerfilEstudiantes = repositorioPerfilEstudiante;
        _repositorioMedallas = repositorioMedalla;
        _repositorioProfesores = repositorioProfesor;
        _repositorioPerfilEstudianteMedalla = repositorioPerfilEstudianteMedalla;
        _perfilObserver = perfilObserver;
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


        perfilEstudiante.Monedas = perfilEstudiante.Monedas + medalla.MonedasOtorgadas;

        if (!profesor.Grupos.Any(g => g.Id == perfilEstudiante.GrupoId))
            return Resultado.Falla(Error.Forbidden);

        if(!profesor.Medallas.Any(m => m.Id == medalla.Id))
            return Resultado.Falla(Error.Forbidden);

        var nuevaAsignacion = new PerfilEstudianteMedalla
        {
            PerfilEstudianteId = perfilEstudiante.Id,
            MedallaId = medalla.Id,
        };
        var addResultado = await _repositorioPerfilEstudianteMedalla.AddAsync(nuevaAsignacion);
        perfilEstudiante.Subscribe(_perfilObserver);                  
        perfilEstudiante.NotifyMedallaAsignada(nuevaAsignacion);      
        perfilEstudiante.Unsubscribe(_perfilObserver);
        return addResultado;
    }
}
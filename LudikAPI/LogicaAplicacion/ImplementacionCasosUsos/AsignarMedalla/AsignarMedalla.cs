using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.AsignacionMedalla;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla;

public class AsignarMedalla: IAsignarMedalla
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilEstudiantes;
    private readonly IRepositorioMedallas _repositorioMedallas;
    private readonly IRepositorioProfesores _repositorioProfesores;

    public AsignarMedalla(
        IRepositorioPerfilEstudianteGrupo repositorioPerfilEstudiante,
        IRepositorioMedallas repositorioMedalla,
        IRepositorioProfesores repositorioProfesor)
    {
        _repositorioPerfilEstudiantes = repositorioPerfilEstudiante;
        _repositorioMedallas = repositorioMedalla;
        _repositorioProfesores = repositorioProfesor;
    }


    public async Task<Resultado> EjecutarAsync(string profesorId, int idPerfilEstudiante, MedallaDto medallaDto)
    {
        var profesorResultado = await _repositorioProfesores.GetByStringIdAsync(profesorId);
        var perfilResultado = await _repositorioPerfilEstudiantes.GetByIdAsync(idPerfilEstudiante);
        var medallaResultado = await _repositorioMedallas.GetByIdAsync(medallaDto.Id);

        if (profesorResultado.EsFallo) return Resultado.Falla(Error.NotFound);
        if (perfilResultado.EsFallo) return Resultado.Falla(Error.NotFound);
        if (medallaResultado.EsFallo) return Resultado.Falla(Error.NotFound);

        var profesor = profesorResultado.Valor;
        var perfil = perfilResultado.Valor;
        var medalla = medallaResultado.Valor;


        if (!profesor.Grupos.Any(g => g.Id == perfil.GrupoId))
            return Resultado.Falla(Error.Forbidden);
        

        perfil.MedallasObtenidas.Add(medalla);

        var updateResultado = await _repositorioPerfilEstudiantes.UpdateAsync(perfil);

        return updateResultado;
    }
}
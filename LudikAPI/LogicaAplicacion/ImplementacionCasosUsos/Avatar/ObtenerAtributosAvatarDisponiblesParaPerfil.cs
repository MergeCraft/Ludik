using InterfacesRepositorio;
using LogicaAplicacion.DTOs.AtributoAvatarDTOs;
using LogicaAplicacion.DTOs.AvatarDTOs;
using LogicaAplicacion.DTOsMappers;
using LogicaAplicacion.InterfacesCasosUsos.Avatar;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Avatar;

public class ObtenerAtributosAvatarDisponiblesParaPerfil : IObtenerAtributosAvatarDisponiblesParaPerfil
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilesEstudiantes;

    public ObtenerAtributosAvatarDisponiblesParaPerfil(IRepositorioPerfilEstudianteGrupo repositorioPerfilesEstudiantes)
    {
        _repositorioPerfilesEstudiantes = repositorioPerfilesEstudiantes;
    }
    /// <summary>
    /// Retorna los atributos de avatar disponibles para un perfil de estudiante.
    /// <summary>
    public async Task<Resultado<IEnumerable<AtributoAvatarDto>>> EjecutarAsync(int idPerfilEstudiante, string idUsuarioAutenticado)
    {
        var resultado = await _repositorioPerfilesEstudiantes.GetByIdAsync(idPerfilEstudiante);
        if (resultado.EsFallo)
            return Resultado<IEnumerable<AtributoAvatarDto>>.Falla(new Error("Error.NotFound", "Perfil de estudiante no encontrado."));
        var perfilEstudiante = resultado.Valor;

        if (perfilEstudiante.EstudianteId != idUsuarioAutenticado)
            return Resultado<IEnumerable<AtributoAvatarDto>>.Falla(Error.Forbidden);
        
        List<PersonalizacionAvatar> itemsDisponiblesParaPersonazarAvatar = perfilEstudiante.ObtenerItemsAvatarDisponibles();

        var atributosDto = itemsDisponiblesParaPersonazarAvatar
            .Select(item => AtributoAvatarMapper.toDto(item.AtributoDesbloqueable)).ToList();

        return Resultado<IEnumerable<AtributoAvatarDto>>.Exitoso(atributosDto);

    }
}
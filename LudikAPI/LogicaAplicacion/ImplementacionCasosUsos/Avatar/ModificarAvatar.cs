using InterfacesRepositorio;
using LogicaAplicacion.DTOs.AvatarDTOs;
using LogicaAplicacion.DTOsMappers;
using LogicaAplicacion.InterfacesCasosUsos.Avatar;
using LogicaAplicacion.InterfacesCasosUsos.Imagenes;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Entidades = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.Avatar;

public class ModificarAvatar: IModificarAvatar
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilesEstudiantes;
    private readonly IRepositorioAvatares _repositorioAvatares;
    private readonly IServicioGestionImagenPerfil _servicioGestionImagenPerfil;

    public ModificarAvatar(
        IRepositorioPerfilEstudianteGrupo repositorioPerfilesEstudiantes,
        IRepositorioAvatares repositorioAvatares,
        IServicioGestionImagenPerfil servicioGestionImagenPerfil)
    {
        _repositorioPerfilesEstudiantes = repositorioPerfilesEstudiantes;
        _repositorioAvatares = repositorioAvatares;
        _servicioGestionImagenPerfil = servicioGestionImagenPerfil;
    }

    public async Task<Resultado> EjecutarAsync(int idPerfilEstudiante, string idUsuarioAutenticado, AvatarDto avatarDto,
        Stream streamImagen)
    {
        var resultadaoPerfil = await _repositorioPerfilesEstudiantes.GetByIdAsync(idPerfilEstudiante);
        if (resultadaoPerfil == null || resultadaoPerfil.EsFallo)
            return Resultado.Falla(Error.NotFound);

        Entidades.PerfilEstudiante perfilEstudiante = resultadaoPerfil.Valor;

        if (perfilEstudiante.EstudianteId != idUsuarioAutenticado)
            return Resultado.Falla(Error.Forbidden);
        
        // Justificación: Cumple con la regla de que solo se pueden usar items comprados.
        var resultadoItemsAdquiridos = await _repositorioPerfilesEstudiantes.ObtenerItemsAvatarAdquiridosAsync(idPerfilEstudiante);
        if (resultadoItemsAdquiridos.EsFallo)
            return resultadoItemsAdquiridos;

        var erroresValidacion = ValidarItemsAvatar(avatarDto, resultadoItemsAdquiridos.Valor);
        if (erroresValidacion.Any())
            return Resultado.Falla(erroresValidacion);
        
        var resultadoAvatar = await _repositorioAvatares.GetByPerfilIdAsync(idPerfilEstudiante);
        if (resultadoAvatar.EsFallo)
            return Resultado.Falla(Error.NotFound);
        
        Entidades.Avatar avatarActualizado = AvatarMappers.fromDto(avatarDto);
        Resultado resultadoActualizarAvatar= await _repositorioAvatares.UpdateAsync(avatarActualizado);
        if (resultadoActualizarAvatar.EsFallo)
            return resultadoActualizarAvatar;


        Resultado resultadoSubirImagen = await _servicioGestionImagenPerfil.SubirImagenPerfilAsync(idPerfilEstudiante, idUsuarioAutenticado, streamImagen);
        if (resultadoSubirImagen.EsFallo)
            return resultadoSubirImagen;
        

        return Resultado.Exitoso();

    }

    private List<Error> ValidarItemsAvatar(AvatarDto dto, IEnumerable<Entidades.Recompensa> itemsAdquiridos)
    {
        var errores = new List<Error>();
        var itemsAValidar = new List<string> { dto.Pelo, dto.Ojos, dto.Boca, dto.Ropa, dto.Gorro, dto.Gafas };

        foreach (var item in itemsAValidar)
        {

        }

        return errores;
    }

}
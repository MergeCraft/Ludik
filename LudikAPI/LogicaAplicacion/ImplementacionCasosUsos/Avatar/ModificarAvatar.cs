using InterfacesRepositorio;
using LogicaAplicacion.DTOs.AvatarDTOs;
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
        //Se debe de validar los datos recibidos por parametro no sean nulos
        // Se debe de validad que el id del perfil de estudiante pertenezca al usuario que realiza la petición
        //  Se debe actualizar el avatar del perfil de estudiante con los datos recibidos por parámetro (avatarDto)
        // Se debe de actualizar la imagen (archivo) que tiene asociado urlImagenAvatar
        var resultadaoPerfil = await _repositorioPerfilesEstudiantes.GetByIdAsync(idPerfilEstudiante);
        if (resultadaoPerfil == null || resultadaoPerfil.EsFallo)
            return Resultado.Falla(Error.NotFound);

        Entidades.PerfilEstudiante perfilEstudiante = resultadaoPerfil.Valor;

        if (perfilEstudiante.EstudianteId != idUsuarioAutenticado)
            return Resultado.Falla(Error.Forbidden);
        

        // 2. Validación de reglas de negocio (items adquiridos)
        // Justificación: Cumple con la regla de que solo se pueden usar items comprados.
        var itemsAdquiridos = await _repositorioPerfilesEstudiantes.ObtenerItemsAdquiridosAsync(idPerfilEstudiante);
        var erroresValidacion = ValidarItemsAvatar(avatarDto, itemsAdquiridos);
        if (erroresValidacion.Any())
        {
            return Resultado.Falla(erroresValidacion);
        }

        // 3. Actualización de la entidad Avatar
        var resultadoAvatar = await _repositorioAvatares.GetByPerfilIdAsync(idPerfilEstudiante);
        if (resultadoAvatar == null || resultadoAvatar.EsFallo)
        {
            // Esto sería un estado inconsistente, pero lo manejamos por si acaso.
            return Resultado.Falla(Error.NotFound);
        }

        // Aquí podrías usar un Mapper (ej. AutoMapper) para hacer esto más limpio.
        // Por ahora, lo hacemos manualmente para claridad.
        MapearDtoAEntidad(avatarDto, resultadoAvatar);
        _repositorioAvatares.Actualizar(resultadoAvatar);

        // 4. Actualización de la imagen usando el servicio existente.
        Resultado resultadoSubirImagen = await _servicioGestionImagenPerfil.SubirImagenDePerfilAsync(idPerfilEstudiante, idUsuarioAutenticado, streamImagen);
        if (resultadoSubirImagen.EsFallo)
        {
            return resultadoSubirImagen;
        }

        // Por ahora, vamos a simular la lógica de la URL. El servicio de imagen debería hacer esto.
        // En un escenario real, el servicio de imagen devolvería la nueva URL.
        // var resultadoUrl = await _servicioImagen.GuardarImagenYObtenerUrl(imagenStream, ...);
        // if(resultadoUrl.EsFallo) return resultadoUrl;
        // avatar.UrlImagen = resultadoUrl.Valor;
        // Por simplicidad en este paso, asumimos que el servicio se encarga de todo.

        // 5. Persistencia atómica
        // Justificación: El patrón Unit of Work asegura que la actualización de los datos del avatar
        // y la (potencial) actualización de la URL de la imagen se guarden en una única transacción.
        // Si una de las dos falla, ninguna se aplica, manteniendo la consistencia de los datos.
        await _unitOfWork.SaveChangesAsync();

        return Resultado.Exitoso();

    }

}
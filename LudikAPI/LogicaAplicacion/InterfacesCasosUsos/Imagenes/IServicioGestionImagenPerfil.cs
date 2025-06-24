using LogicaAplicacion.DTOs.ImagenPerfilDtos;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Http;

namespace LogicaAplicacion.InterfacesCasosUsos.Imagenes;

public interface IServicioGestionImagenPerfil
{
    Task<Resultado> SubirImagenPerfilAsync(int idPerfilEstudiante, string idUsuarioAutenticado, Stream imagen);

    Task<Resultado<ImagenPerfilDto>> ObtenerUrlImagenPerfilAsync(int idPerfilEstudiante, string idUsuarioAutenticado);
}
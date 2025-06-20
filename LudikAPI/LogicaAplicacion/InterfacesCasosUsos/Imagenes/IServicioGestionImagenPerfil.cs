using LogicaAplicacion.DTOs.ImagenPerfilDtos;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Http;

namespace LogicaAplicacion.InterfacesCasosUsos.Imagenes;

public interface IServicioGestionImagenPerfil
{
    Task<Resultado> SubirImagenDePerfilAsync(int idPerfilEstudiante, string idUsuarioAutenticado, IFormFile imagen);

    Task<Resultado<ImagenPerfilDto>> ObtenerUrlImagenPerfilAsync(int idPerfilEstudiante, string idUsuarioAutenticado);
}
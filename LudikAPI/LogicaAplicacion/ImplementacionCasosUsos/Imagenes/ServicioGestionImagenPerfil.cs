using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ImagenPerfilDtos;
using LogicaAplicacion.InterfacesCasosUsos.Imagenes;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Microsoft.AspNetCore.Http;

namespace LogicaAplicacion.ImplementacionCasosUsos.Imagenes;

public class ServicioGestionImagenPerfil: IServicioGestionImagenPerfil
{
    private readonly IRepositorioAlmacenamientoArchivos _repositorioArchivos;
    private readonly IServicioProcesamientoImagenes _procesadorImagenes;
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfilesEstudiantes;

    public ServicioGestionImagenPerfil(
        IRepositorioAlmacenamientoArchivos fileStorage,
        IServicioProcesamientoImagenes procesadorImagenes,
        IRepositorioPerfilEstudianteGrupo repositorioPerfilesEstudiantes)
    {
        _repositorioArchivos = fileStorage;
        _procesadorImagenes = procesadorImagenes;
        _repositorioPerfilesEstudiantes = repositorioPerfilesEstudiantes;
    }

    public async Task<Resultado> SubirImagenDePerfilAsync(int idPerfilEstudiante, string idUsuarioAutenticado, IFormFile imagen)
    {
        var resultadoPerfilEstudiante = await _repositorioPerfilesEstudiantes.GetByIdAsync(idPerfilEstudiante);
        if (resultadoPerfilEstudiante.EsFallo)
            return Resultado<ImagenPerfilDto>.Falla(Error.NotFound);

        Dominio.PerfilEstudiante perfil = resultadoPerfilEstudiante.Valor;
        if (perfil.EstudianteId != idUsuarioAutenticado)
        {
            // El usuario está autenticado, pero intenta modificar un recurso que no le pertenece.
            return Resultado<ImagenPerfilDto>.Falla(Error.Forbidden);
        }

        using var originalStream = imagen.OpenReadStream();
        var resultadoProcesamiento = await _procesadorImagenes.ProcesarImagenPerfilAsync(originalStream);

        if (resultadoProcesamiento.EsFallo)
            return Resultado<ImagenPerfilDto>.Falla(resultadoProcesamiento.Errores);

        var urls = new Dictionary<string, string>();
        var nombresArchivo = new Dictionary<string, string>();

        // 2. Subir cada versión con el nombre correcto
        foreach (var imagenProcesada in resultadoProcesamiento.Valor)
        {
            var nombreArchivo = $"{idPerfilEstudiante}/perfil_{imagenProcesada.Tipo}.jpg";

            var resultadoUpload = await _repositorioArchivos.SubirArchivoAsync(
                imagenProcesada.Contenido,
                nombreArchivo,
                "image/jpeg"); // Forzamos jpeg ya que el procesador lo convierte

            // Liberar el MemoryStream
            await imagenProcesada.Contenido.DisposeAsync();

            if (resultadoUpload.EsFallo)
                return Resultado<ImagenPerfilDto>.Falla(resultadoUpload.Errores);
            
            urls[imagenProcesada.Tipo] = resultadoUpload.Valor;
            nombresArchivo[imagenProcesada.Tipo] = nombreArchivo;
        }

        //Guardar los nombres las imagenes en la base de datos
        perfil.RutaImagenCompleta = nombresArchivo["completa"];
        perfil.RutaImagenMiniatura = nombresArchivo["mini"];
        await _repositorioPerfilesEstudiantes.UpdateAsync(perfil);

        //Devolver las URLs SAS (o directas si el contenedor es público)
        var dto = new ImagenPerfilDto
        {
            UrlCompleta = urls["completa"],
            UrlMiniatura = urls["mini"]
        };

        return Resultado<ImagenPerfilDto>.Exitoso(dto);
    }


    public async Task<Resultado<ImagenPerfilDto>> ObtenerUrlImagenPerfilAsync(int idPerfilEstudiante, string idUsuarioAutenticado)
    {
        var resultadoPerfil = await _repositorioPerfilesEstudiantes.GetByIdAsync(idPerfilEstudiante);
        if (resultadoPerfil.EsFallo)
            return Resultado<ImagenPerfilDto>.Falla(Error.NotFound);

        Dominio.PerfilEstudiante perfil = resultadoPerfil.Valor;
        if (perfil.EstudianteId != idUsuarioAutenticado)
        {
            // El usuario está autenticado, pero intenta modificar un recurso que no le pertenece.
            return Resultado<ImagenPerfilDto>.Falla(Error.Forbidden);
        }

        var nombreCompleta = resultadoPerfil.Valor.RutaImagenCompleta;
        var nombreMiniatura = resultadoPerfil.Valor.RutaImagenMiniatura;


        var resultadoUrlCompleta = await _repositorioArchivos.ObtenerArchivoSasUrlAsync(nombreCompleta);
        var resultadoUrlMiniatura = await _repositorioArchivos.ObtenerArchivoSasUrlAsync(nombreMiniatura);

        if (resultadoUrlCompleta.EsFallo || resultadoUrlMiniatura.EsFallo)
            return Resultado<ImagenPerfilDto>.Falla(Error.NotFound);
        

        var imagenPerfilDto = new ImagenPerfilDto
        {
            UrlCompleta = resultadoUrlCompleta.Valor,
            UrlMiniatura = resultadoUrlMiniatura.Valor
        };

        return Resultado<ImagenPerfilDto>.Exitoso(imagenPerfilDto);
    }
}
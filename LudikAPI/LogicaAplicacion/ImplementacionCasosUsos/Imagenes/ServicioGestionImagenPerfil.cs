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
    // Si tienes un repositorio de usuarios para guardar la URL, inyéctalo aquí.
    // private readonly IUsuarioRepository _usuarioRepository;

    public ServicioGestionImagenPerfil(IRepositorioAlmacenamientoArchivos fileStorage, IServicioProcesamientoImagenes procesadorImagenes)
    {
        _repositorioArchivos = fileStorage;
        _procesadorImagenes = procesadorImagenes;
    }

    public async Task<Resultado> SubirImagenDePerfilAsync(int userId, IFormFile imagen)
    {
        // 1. Generar un nombre de archivo único
        //    Es crucial evitar colisiones y asociar la imagen al usuario.
        var fileName = $"perfil-{userId}{Path.GetExtension(imagen.FileName)}";

        // 2. Procesar y comprimir la imagen
        using var originalStream = imagen.OpenReadStream();
        var resultadoStreamProcesada = await _procesadorImagenes.ProcesarImagenPerfilAsync(originalStream);

        if(resultadoStreamProcesada.EsFallo)
            return resultadoStreamProcesada;
        var streamProcesada = resultadoStreamProcesada.Valor;

        // 3. Subir la imagen procesada
        var resultadoImageUrl = await _repositorioArchivos.SubirArchivoAsync(streamProcesada, fileName, imagen.ContentType);
        if(resultadoImageUrl.EsFallo)
            return resultadoImageUrl;

        // 4. (Opcional pero recomendado) Guardar la URL o el nombre del archivo en la DB
        // var usuario = await _usuarioRepository.FindByIdAsync(userId);
        // usuario.UrlImagenPerfil = fileName; // Guardar el nombre es más flexible que la URL
        // await _usuarioRepository.UpdateAsync(usuario);
        return resultadoImageUrl;
    }

    public async Task<Resultado<ImagenPerfilDto>> ObtenerUrlImagenPerfilAsync(int userId)
    {
        // Asume que buscas en la DB el nombre del archivo de imagen del usuario.
        // Por ahora, construiremos el nombre directamente.
        var fileName = $"perfil-{userId}.jpg"; // Asumiendo que siempre se guarda como jpg

        var resultadoSasUrl = await _repositorioArchivos.ObtenerArchivoSasUrlAsync(fileName);
        if(resultadoSasUrl.EsFallo)
            return resultadoSasUrl;
        ImagenPerfilDto imagenPerfil = new ImagenPerfilDto { Url = resultadoSasUrl.Valor }; 
        return Resultado<ImagenPerfilDto>.Exitoso(imagenPerfil);
    }
}
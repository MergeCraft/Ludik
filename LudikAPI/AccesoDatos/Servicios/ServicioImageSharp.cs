using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.InterfacesCasosUsos.Imagenes;
using LogicaNegocio.Resultados;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace AccesoDatos.Servicios
{
    public class ServicioImageSharp: IServicioProcesamientoImagenes
    {
        public async Task<Resultado<Stream>> ProcesarImagenPerfilAsync(Stream streamOriginal)
        {
            var outputStream = new MemoryStream();
            using (var image = await Image.LoadAsync(streamOriginal))
            {
                // Redimensionar para un tamaño máximo de 500x500 px, manteniendo la relación de aspecto.
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(500, 500),
                    Mode = ResizeMode.Max
                }));

                // Guardar la imagen comprimida como JPEG con calidad 80.
                await image.SaveAsJpegAsync(outputStream, new JpegEncoder { Quality = 80 });
            }
            outputStream.Position = 0; // Rebobinar el stream para que pueda ser leído desde el principio.
            return Resultado<Stream>.Exitoso(outputStream);
        }
    }
}

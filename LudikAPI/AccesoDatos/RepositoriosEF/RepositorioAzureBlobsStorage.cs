using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Microsoft.Extensions.Configuration;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioAzureBlobsStorage: IRepositorioAlmacenamientoArchivos
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public RepositorioAzureBlobsStorage(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = configuration.GetValue<string>("StorageContainerName");
        }

        public async Task<Resultado<string>> SubirArchivoAsync(Stream streamArchivo, string nombreArchivo, string tipoContenido)
        {
            var containerClient = await GetContainerClient();
            var blobClient = containerClient.GetBlobClient(nombreArchivo);

            await blobClient.UploadAsync(streamArchivo, new BlobHttpHeaders { ContentType = tipoContenido });

            return blobClient.Uri.ToString();
        }

        public async Task<Resultado<string>> ObtenerArchivoSasUrlAsync(string nombreArchivo)
        {
            var containerClient = await GetContainerClient();
            var blobClient = containerClient.GetBlobClient(nombreArchivo);

            if (!await blobClient.ExistsAsync())
            {
                return Resultado<string>.Falla(Error.NotFound);
            }

            if (!blobClient.CanGenerateSasUri)
            {
                throw new InvalidOperationException("La configuración del cliente de almacenamiento no permite generar SAS tokens.");
            }

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _containerName,
                BlobName = nombreArchivo,
                Resource = "b", // "b" para blob
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5), // Margen de 5 mins por si hay desfase de reloj
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1), // El enlace expira en 1 hora
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read); // Permiso de solo lectura

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }
    }
}

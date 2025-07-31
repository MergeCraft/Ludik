using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Tienda;
using LogicaAplicacion.ImplementacionServicios;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Tienda
{
    public class PruebasObtenerListadoRecompensa
    {
        private readonly Mock<IRepositorioTiendas> _mockRepoTiendas;
        private readonly ObtenerListadoRecompensa _useCase;

        public PruebasObtenerListadoRecompensa()
        {
            _mockRepoTiendas = new Mock<IRepositorioTiendas>();
            var mockRepoArchivos = new Mock<IRepositorioAlmacenamientoArchivos>();

            // Simula una URL generada por archivo
            mockRepoArchivos
     .Setup(a => a.ObtenerArchivoSasUrlAsync(It.IsAny<string>()))
     .Returns<string>(nombre =>
         Task.FromResult(Resultado<string>.Exitoso($"url-fake/{nombre}")));

            var generadorUrlsParaColecciones = new GeneradorUrlsParaColeccionesImagenes(
                new GeneradorUrlImagen(mockRepoArchivos.Object));

            _useCase = new ObtenerListadoRecompensa(
                _mockRepoTiendas.Object,
                generadorUrlsParaColecciones
            );
        }

        [Fact]
        public async Task TiendaNoExiste_RetornaNotFound()
        {
            _mockRepoTiendas
                .Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Tienda>.Falla(new Error("Error.NotFound", "No se encontró la tienda")));

            var resultado = await _useCase.EjecutarAsync(5);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.NotFound" &&
                e.Mensaje.Contains("No se encontró la tienda"));
        }

        [Fact]
        public async Task CaminoFeliz_RetornaDtosConUrls()
        {
            var recompensas = new List<LogicaNegocio.Entidades.Recompensa>
            {
                new RecompensaSimple { Id = 1, Nombre = "R1", Precio = 5, NombreImagenCompleta = "c1", NombreImagenMiniatura = "m1" },
                new RecompensaSimple { Id = 2, Nombre = "R2", Precio = 10, NombreImagenCompleta = "c2", NombreImagenMiniatura = "m2" }
            };

            var tienda = new LogicaNegocio.Entidades.Tienda
            {
                Id = 20,
                Recompesas = recompensas
            };

            _mockRepoTiendas
                .Setup(r => r.GetByIdAsync(20))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Tienda>.Exitoso(tienda));

            var resultado = await _useCase.EjecutarAsync(20);

            Assert.True(resultado.EsExitoso);
            var lista = resultado.Valor!.ToList();
            Assert.Equal(2, lista.Count);

            Assert.Contains(lista, dto =>
                dto.Nombre == "R1" &&
                dto.Precio == 5 &&
                dto.EnlaceImagenCompleta == "url-fake/c1" &&
                dto.EnlaceImagenMiniatura == "url-fake/m1");

            Assert.Contains(lista, dto =>
                dto.Nombre == "R2" &&
                dto.Precio == 10 &&
                dto.EnlaceImagenCompleta == "url-fake/c2" &&
                dto.EnlaceImagenMiniatura == "url-fake/m2");
        }
    }
}
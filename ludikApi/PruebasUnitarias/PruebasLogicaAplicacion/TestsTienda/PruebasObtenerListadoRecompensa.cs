using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.ImplementacionCasosUsos.Tienda;
using LogicaAplicacion.ImplementacionServicios;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsTienda
{
    public class PruebasObtenerListadoRecompensa
    {
        private readonly Mock<IRepositorioRecompensas> _mockRepoRec;
        private readonly Mock<IRepositorioTiendas> _mockRepoTiendas;
        private readonly IGeneradorUrlsParaColeccionesImagenes _generadorUrlsParaColecciones;
        private readonly ObtenerListadoRecompensa _useCase;
        private const string ProfesorId = "unused"; 

        public PruebasObtenerListadoRecompensa()
        {
            _mockRepoRec = new Mock<IRepositorioRecompensas>();
            _mockRepoTiendas = new Mock<IRepositorioTiendas>();
            _generadorUrlsParaColecciones =
                new GeneradorUrlsParaColeccionesImagenes(
                    new GeneradorUrlImagen(new Mock<IRepositorioAlmacenamientoArchivos>().Object));
            _useCase = new ObtenerListadoRecompensa(
                _mockRepoTiendas.Object,
                _generadorUrlsParaColecciones
            );
        }

        [Fact]
        public async Task IdNoEntero_RetornaInvalidId()
        {
            var resultado = await _useCase.EjecutarAsync(10);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.InvalidId" &&
                e.Mensaje.Contains("ID de tienda inválido"));
            _mockRepoTiendas.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockRepoRec.Verify(r => r.GetByTiendaIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task TiendaNoExiste_RetornaNotFound()
        {
            _mockRepoTiendas
                .Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(Resultado<Tienda>.Falla(new Error("X", "")));

            var resultado = await _useCase.EjecutarAsync(5);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.NotFound" &&
                e.Mensaje.Contains("No se encontró la tienda"));
            _mockRepoRec.Verify(r => r.GetByTiendaIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ErrorAlObtenerRecompensas_RetornaUnexpected()
        {
            _mockRepoTiendas
                .Setup(r => r.GetByIdAsync(10))
                .ReturnsAsync(Resultado<Tienda>.Exitoso(new Tienda { Id = 10 }));
            _mockRepoRec
                .Setup(r => r.GetByTiendaIdAsync(10))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Falla(new Error("X", "")));

            var resultado = await _useCase.EjecutarAsync(10);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Unexpected" &&
                e.Mensaje.Contains("Error al obtener recompensas"));
        }

        [Fact]
        public async Task CaminoFeliz_RetornaDtos()
        {
            var tienda = new Tienda { Id = 20 };
            var recompensas = new[]
            {
                new RecompensaSimple { Id = 1, Nombre="R1", Precio=5, NombreImagenCompleta="c1", NombreImagenMiniatura="m1" },
                new RecompensaSimple { Id = 2, Nombre="R2", Precio=10, NombreImagenCompleta="c2", NombreImagenMiniatura="m2"}
            };

            _mockRepoTiendas
                .Setup(r => r.GetByIdAsync(20))
                .ReturnsAsync(Resultado<Tienda>.Exitoso(tienda));
            _mockRepoRec
                .Setup(r => r.GetByTiendaIdAsync(20))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Exitoso(recompensas));

            var resultado = await _useCase.EjecutarAsync(20);

            Assert.True(resultado.EsExitoso);
            var lista = resultado.Valor!.ToList();
            Assert.Equal(2, lista.Count);

            Assert.Contains(lista, dto =>
                dto.Nombre == "R1" &&
                dto.Precio == 5 &&
                dto.EnlaceImagenCompleta == "c1" &&
                dto.EnlaceImagenMiniatura == "m1");
            Assert.Contains(lista, dto =>
                dto.Nombre == "R2" &&
                dto.Precio == 10 &&
                dto.EnlaceImagenCompleta == "c2" &&
                dto.EnlaceImagenMiniatura == "m2");
        }
    }
}

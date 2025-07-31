using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Recompensa;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Recompensa
{
    public class PruebasAltaRecompensa
    {
        private readonly Mock<IRepositorioRecompensas> _repoRecMock;
        private readonly Mock<IRepositorioTiendas> _repoTiendaMock;
        private readonly Mock<IRepositorioProfesores> _repoProfesorMock;
        private readonly AltaRecompensa _casoUso;
        private const string TiendaId = "tienda-1";
        private const string ProfesorId = "prof-1";
        private readonly Tienda _tienda;

        public PruebasAltaRecompensa()
        {
            _repoRecMock = new Mock<IRepositorioRecompensas>();
            _repoTiendaMock = new Mock<IRepositorioTiendas>();
            _casoUso = new AltaRecompensa(_repoProfesorMock.Object);

            // Preparamos una tienda válida para los tests felices
            _tienda = new Tienda
            {
                Id = 10,
                Grupo = new LogicaNegocio.Entidades.Grupo { ProfesorId = ProfesorId }
            };
        }

        [Fact]
        public async Task EjecutarAsync_DtoNulo_RetornaFalloValidation()
        {
            var resultado = await _casoUso.EjecutarAsync(null, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Validation" &&
                e.Mensaje.Contains("No hay información para poder dar de alta"));
            _repoRecMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Never);
            _repoTiendaMock.Verify(r => r.GetByStringIdAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_TiendaNoExiste_RetornaFalloValidation()
        {
            _repoTiendaMock
                .Setup(r => r.GetByStringIdAsync(TiendaId))
                .ReturnsAsync(Resultado<Tienda>.Falla(new Error("X", "")));

            var dto = new RecompensaAltaDto { Nombre = "R", Precio = 1 };
            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Validation" &&
                e.Mensaje.Contains("No se encontró la tienda"));
            _repoRecMock.Verify(r => r.GetByTiendaIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_TiendaNoPerteneceProfesor_RetornaFalloValidation()
        {
            _repoTiendaMock
                .Setup(r => r.GetByStringIdAsync(TiendaId))
                .ReturnsAsync(Resultado<Tienda>.Exitoso(new Tienda
                {
                    Id = 11,
                    Grupo = new LogicaNegocio.Entidades.Grupo { ProfesorId = "otro-prof" }
                }));

            var dto = new RecompensaAltaDto { Nombre = "R", Precio = 1 };
            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Validation" &&
                e.Mensaje.Contains("La tienda no pertenece"));
            _repoRecMock.Verify(r => r.GetByTiendaIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorAlObtenerRecompensas_RetornaFalloUnexpected()
        {
            // Arrange
            _repoTiendaMock
                .Setup(r => r.GetByStringIdAsync(TiendaId))
                .ReturnsAsync(Resultado<Tienda>.Exitoso(_tienda));

            _repoRecMock
                .Setup(r => r.GetByTiendaIdAsync(_tienda.Id))
                .ReturnsAsync(
                    Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Falla(
                        new Error("X", "")
                    )
                );

            var dto = new RecompensaAltaDto { Nombre = "R", Precio = 1 };

            // Act
            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Unexpected" &&
                e.Mensaje.Contains("No se pudo verificar la unicidad")
            );
            _repoRecMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_NombreDuplicado_RetornaFalloValidation()
        {
            // Arrange
            _repoTiendaMock
                .Setup(r => r.GetByStringIdAsync(TiendaId))
                .ReturnsAsync(Resultado<Tienda>.Exitoso(_tienda));

            _repoRecMock
                .Setup(r => r.GetByTiendaIdAsync(_tienda.Id))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Exitoso(
                    new List<RecompensaSimple>
                    {
                new RecompensaSimple { Nombre = "Test" }
                    }.AsEnumerable()
                ));

            var dto = new RecompensaAltaDto { Nombre = "Test", Precio = 1 };

            // Act
            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Validation" &&
                e.Mensaje.Contains("ya está en uso"));
            _repoRecMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_DatosValidos_YAgregaRecompensa()
        {
            // Arrange
            _repoTiendaMock
                .Setup(r => r.GetByStringIdAsync(TiendaId))
                .ReturnsAsync(Resultado<Tienda>.Exitoso(_tienda));

            _repoRecMock
                .Setup(r => r.GetByTiendaIdAsync(_tienda.Id))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Exitoso(
                    Enumerable.Empty<LogicaNegocio.Entidades.Recompensa>()
                ));

            var dto = new RecompensaAltaDto
            {
                Nombre = "Nueva",
                RutaImagenCompleta = "urlC",
                RutaImagenMiniatura = "urlM",
                Precio = 50
            };
            LogicaNegocio.Entidades.Recompensa capturada = null!;
            _repoRecMock
                .Setup(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()))
                .Callback<LogicaNegocio.Entidades.Recompensa>(r => capturada = r)
                .ReturnsAsync(Resultado.Exitoso());

            // Act
            var resultado = await _casoUso.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            _repoRecMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Once);
            Assert.Equal(dto.Nombre, capturada.Nombre);
            Assert.Equal(dto.Precio, capturada.Precio);
            Assert.Equal(dto.RutaImagenCompleta, capturada.NombreImagenCompleta);
            Assert.Equal(dto.RutaImagenMiniatura, capturada.NombreImagenMiniatura);
        }
    }
}

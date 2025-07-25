using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Recompensa;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsRecompensa
{
    public class PruebaEditarRecompensa
    {
        private readonly Mock<IRepositorioRecompensas> _mockRepoRecompensas;
        private readonly Mock<IRepositorioTiendas> _mockRepoTiendas;
        private readonly EditarRecompensa _useCase;

        public PruebaEditarRecompensa()
        {
            _mockRepoRecompensas = new Mock<IRepositorioRecompensas>();
            _mockRepoTiendas = new Mock<IRepositorioTiendas>(); // no se usa dentro del caso de uso
            _useCase = new EditarRecompensa(_mockRepoRecompensas.Object, _mockRepoTiendas.Object);
        }

        [Fact]
        public async Task EjecutarAsync_IdNoEntero_RetornaFalloInvalidId()
        {
            // Arrange
            var dto = new RecompensaEditarDto { Nombre = "X", Precio = 10 };

            // Act
            var resultado = await _useCase.EjecutarAsync("no-int", dto, "prof-1");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Collection(resultado.Errores, e =>
            {
                Assert.Equal("Error.InvalidId", e.Codigo);
                Assert.Contains("ID de recompensa inválido", e.Mensaje);
            });
            _mockRepoRecompensas.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_RecompensaNoExiste_RetornaFalloNotFound()
        {
            // Arrange
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Falla(new Error("X", "")));
            var dto = new RecompensaEditarDto { Nombre = "X", Precio = 10 };

            // Act
            var resultado = await _useCase.EjecutarAsync("5", dto, "prof-1");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Collection(resultado.Errores, e =>
            {
                Assert.Equal("Error.NotFound", e.Codigo);
                Assert.Contains("No se encontró la recompensa especificada", e.Mensaje);
            });
            _mockRepoRecompensas.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_DominioInvalido_RetornaFalloValidation()
        {
            // Arrange
            // Recompensa inicial con valores válidos
            var recompensa = new RecompensaSimple
            {
                Id = 7,
                Nombre = "Original",
                Precio = 100
            };
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(7))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            // DTO con precio inválido para forzar falla en esValido()
            var dto = new RecompensaEditarDto
            {
                Nombre = "Edited",
                RutaImagenCompleta = null,
                RutaImagenMiniatura = null,
                Precio = -50
            };

            // Act
            var resultado = await _useCase.EjecutarAsync("7", dto, "prof-1");

            // Assert
            Assert.True(resultado.EsFallo);
            // Asumimos que la validación de dominio reporta Error.Validation
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Validation");
            _mockRepoRecompensas.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorAlActualizar_RetornaFalloUnexpected()
        {
            // Arrange
            var recompensa = new RecompensaSimple
            {
                Id = 9,
                Nombre = "Original",
                Precio = 20
            };
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(9))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            var dto = new RecompensaEditarDto
            {
                Nombre = "Edited",
                RutaImagenCompleta = "newFull",
                RutaImagenMiniatura = "newThumb",
                Precio = 30
            };

            // validación de dominio pasa
            // simulamos falla en UpdateAsync
            _mockRepoRecompensas
                .Setup(r => r.UpdateAsync(It.Is<LogicaNegocio.Entidades.Recompensa>(r =>
                    r.Id == 9 &&
                    r.Nombre == dto.Nombre &&
                    r.Precio == dto.Precio)))
                .ReturnsAsync(Resultado.Falla(new Error("X", "")));

            // Act
            var resultado = await _useCase.EjecutarAsync("9", dto, "prof-1");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e =>
                e.Codigo == "Error.Unexpected" &&
                e.Mensaje.Contains("No se pudo actualizar"));
        }

        [Fact]
        public async Task EjecutarAsync_CaminoFeliz_RetornaExitoso()
        {
            // Arrange
            var recompensa = new RecompensaSimple
            {
                Id = 15,
                Nombre = "Original",
                Precio = 5,
                NombreImagenCompleta = "oldFull",
                NombreImagenMiniatura = "oldThumb"
            };
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(15))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            var dto = new RecompensaEditarDto
            {
                Nombre = "NuevoNombre",
                RutaImagenCompleta = "newFull",
                RutaImagenMiniatura = null, // conserva oldThumb
                Precio = 50
            };

            LogicaNegocio.Entidades.Recompensa capturada = null!;
            _mockRepoRecompensas
                .Setup(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()))
                .Callback<LogicaNegocio.Entidades.Recompensa>(r => capturada = r)
                .ReturnsAsync(Resultado.Exitoso());

            // Act
            var resultado = await _useCase.EjecutarAsync("15", dto, "prof-1");

            // Assert
            Assert.True(resultado.EsExitoso);

            // Verificamos que el mapper hizo su trabajo
            Assert.Equal("NuevoNombre", capturada.Nombre);
            Assert.Equal("newFull", capturada.NombreImagenCompleta);
            Assert.Equal("oldThumb", capturada.NombreImagenMiniatura);
            Assert.Equal(50, capturada.Precio);

            _mockRepoRecompensas.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Once);
        }
    }
}

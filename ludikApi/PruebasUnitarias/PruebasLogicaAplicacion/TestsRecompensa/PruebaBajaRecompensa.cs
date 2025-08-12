using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Recompensa;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsRecompensa
{
    public class PruebaBajaRecompensa
    {
        private readonly Mock<IRepositorioRecompensas> _mockRepoRecompensas;
        private readonly BajaRecompensa _useCase;
        private const string ProfesorId = "prof-1";

        public PruebaBajaRecompensa()
        {
            //_mockRepoRecompensas = new Mock<IRepositorioRecompensas>();
            //_useCase = new BajaRecompensa(_mockRepoRecompensas.Object);
        }

        [Fact]
        public async Task EjecutarAsync_IdNoEntero_RetornaFalloInvalidId()
        {
            // Act
            var resultado = await _useCase.EjecutarAsync("no-int", ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Collection(resultado.Errores, e =>
            {
                Assert.Equal("Error.InvalidId", e.Codigo);
                Assert.Contains("ID de recompensa inválido", e.Mensaje);
            });
            _mockRepoRecompensas.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockRepoRecompensas.Verify(r => r.RemoveAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_RecompensaNoExiste_RetornaFalloNotFound()
        {
            // Arrange
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Falla(
                    new Error("X", "")
                ));

            // Act
            var resultado = await _useCase.EjecutarAsync("5", ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Collection(resultado.Errores, e =>
            {
                Assert.Equal("Error.NotFound", e.Codigo);
                Assert.Contains("No se encontró la recompensa especificada", e.Mensaje);
            });
            _mockRepoRecompensas.Verify(r => r.RemoveAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorAlEliminar_RetornaFalloUnexpected()
        {
            // Arrange
            // GetByIdAsync devuelve un objeto válido
            var recompensa = new LogicaNegocio.Entidades.RecompensaSimple { Id = 7 };
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(7))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            // RemoveAsync falla
            _mockRepoRecompensas
                .Setup(r => r.RemoveAsync(7))
                .ReturnsAsync(Resultado.Falla(new Error("X", "")));

            // Act
            var resultado = await _useCase.EjecutarAsync("7", ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Collection(resultado.Errores, e =>
            {
                Assert.Equal("Error.Unexpected", e.Codigo);
                Assert.Contains("No se pudo eliminar la recompensa", e.Mensaje);
            });
        }

        [Fact]
        public async Task EjecutarAsync_CaminoFeliz_RetornaExitoso()
        {
            // Arrange
            var recompensa = new LogicaNegocio.Entidades.RecompensaSimple { Id = 9 };
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(9))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));
            _mockRepoRecompensas
                .Setup(r => r.RemoveAsync(9))
                .ReturnsAsync(Resultado.Exitoso());

            // Act
            var resultado = await _useCase.EjecutarAsync("9", ProfesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            _mockRepoRecompensas.Verify(r => r.RemoveAsync(9), Times.Once);
        }
    }
}

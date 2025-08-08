using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.DTOsMappers.RecompensaMappers;
using LogicaAplicacion.ImplementacionCasosUsos.Recompensa;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesAuxiliares;
using LogicaNegocio.Resultados;
using Moq;
using System.Threading.Tasks;
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
            var dto = new RecompensaSimpleEditarDto { Nombre = "X", Precio = 10, NombreIcono = "icon" };

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
            var dto = new RecompensaSimpleEditarDto { Nombre = "X", Precio = 10, NombreIcono = "icon" };

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
            var recompensa = new RecompensaSimple
            {
                Id = 7,
                Nombre = "Original",
                Precio = 100
            };
            ((RepresentacionIcono)recompensa.Representacion).NombreIcono = "original-icon";

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(7))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));


            var dto = new RecompensaSimpleEditarDto
            {
                Nombre = "Edited",
                NombreIcono = "edited-icon",
                Precio = -50 // Precio inválido para forzar el fallo
            };

            var resultado = await _useCase.EjecutarAsync("7", dto, "prof-1");

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Validation");
            _mockRepoRecompensas.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorAlActualizar_RetornaFalloUnexpected()
        {
            // Arrange
            var recompensa = new RecompensaSimple { Id = 9, Nombre = "Original", Precio = 20 };
            ((RepresentacionIcono)recompensa.Representacion).NombreIcono = "original-icon";

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(9))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            var dto = new RecompensaSimpleEditarDto { Nombre = "Edited", Precio = 30, NombreIcono = "edited-icon" };

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
            // ARRANGE
            var recompensa = new RecompensaSimple { Id = 15, Nombre = "Original", Precio = 5 };
            ((RepresentacionIcono)recompensa.Representacion).NombreIcono = "original-icon";

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(15))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            // CAMBIO: El DTO de edición tiene los nuevos valores, incluyendo NombreIcono
            var dto = new RecompensaSimpleEditarDto
            {
                Nombre = "NuevoNombre",
                Precio = 50,
                NombreIcono = "nuevo-icon"
            };

            LogicaNegocio.Entidades.Recompensa recompensaCapturada = null;
            _mockRepoRecompensas
                .Setup(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()))
                .Callback<LogicaNegocio.Entidades.Recompensa>(r => recompensaCapturada = r) // Capturamos el objeto actualizado
                .ReturnsAsync(Resultado.Exitoso());

            // ACT
            var resultado = await _useCase.EjecutarAsync("15", dto, "prof-1");

            // ASSERT
            Assert.True(resultado.EsExitoso);

            // Verificamos que se llamó a UpdateAsync una vez
            _mockRepoRecompensas.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Recompensa>()), Times.Once);

            // Verificamos que los datos en el objeto capturado son los correctos
            Assert.NotNull(recompensaCapturada);
            Assert.Equal(dto.Nombre, recompensaCapturada.Nombre);
            Assert.Equal(dto.Precio, recompensaCapturada.Precio);

            // CAMBIO: Verificamos la propiedad anidada de forma segura
            var representacion = Assert.IsType<RepresentacionIcono>(recompensaCapturada.Representacion);
            Assert.Equal(dto.NombreIcono, representacion.NombreIcono);
        }
    }
}

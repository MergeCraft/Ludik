using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaNegocio.Resultados;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Medalla
{
    public class PruebasAltaMedalla
    {
        private readonly Mock<IRepositorioMedallas> _repoMedallasMock;
        private readonly AltaMedalla _servicio;
        private const string ProfesorId = "prof-123";

        public PruebasAltaMedalla()
        {
            _repoMedallasMock = new Mock<IRepositorioMedallas>();
            _servicio = new AltaMedalla(_repoMedallasMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_DtoNulo_RetornaErrorValidacion()
        {
            // Act
            var resultado = await _servicio.EjecutarAsync(null, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e =>
                e.Mensaje.Contains("no pueden ser nulos", StringComparison.OrdinalIgnoreCase)));
            _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Never);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task EjecutarAsync_NombreInvalido_RetornaErrorValidacion(string nombreInvalido)
        {
            // Arrange
            var dto = new MedallaAltaDto
            {
                UrlImagen = "url.png",
                Nombre = nombreInvalido,
                Descripcion = "Descripción válida",
                CantidadMonedasBrinda = 1,
                EsAsignacionMutua = false
            };

            // Act
            var resultado = await _servicio.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e =>
                e.Mensaje.Contains("nombre", StringComparison.OrdinalIgnoreCase)));
            _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_DescripcionMuyLarga_RetornaErrorValidacion()
        {
            // Arrange
           
            var dto = new MedallaAltaDto
            {
                UrlImagen = "url.png",
                Nombre = "NombreVálido",
                Descripcion = "asdaasdasdasfawgfagagawdadagashgrhdrhfdkjkljilñjlfyhjdsgsesersgfdshjftkjfykgkhulkihjfhydrtgdsrtwsetshy",
                CantidadMonedasBrinda = 1,
                EsAsignacionMutua = false
            };

            // Act
            var resultado = await _servicio.EjecutarAsync(dto, ProfesorId);

            // Assert: debe fallar la validación
            Assert.True(resultado.EsFallo, "Se esperaba EsFallo=true para descripción >50 caracteres");

            // Protegemos contra Errores == null
            var errores = resultado.Errores ?? Enumerable.Empty<Error>();
            Assert.NotEmpty(errores);

            // Buscamos un mensaje que mencione "descripción" y el límite "50"
            Assert.Contains(errores, e =>
                !string.IsNullOrEmpty(e.Mensaje)
                && e.Mensaje.IndexOf("descripción", StringComparison.OrdinalIgnoreCase) >= 0
                && e.Mensaje.Contains("50"));

            // No debe haberse llamado al repositorio
            _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_MonedasNegativas_RetornaErrorValidacion()
        {
            // Arrange
            var dto = new MedallaAltaDto
            {
                UrlImagen = "url.png",
                Nombre = "NombreVálido",
                Descripcion = "Descripción válida",
                CantidadMonedasBrinda = -5,
                EsAsignacionMutua = true
            };

            // Act
            var resultado = await _servicio.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e =>
                e.Mensaje.Contains("monedas") &&
                e.Mensaje.Contains("no puede ser menor a 0", StringComparison.OrdinalIgnoreCase)));
            _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_AddAsyncExitoso_RetornaExitosoYMapeaEntidad()
        {
            // Arrange
            var dto = new MedallaAltaDto
            {
                UrlImagen = "icono.png",
                Nombre = "NombreVálido",
                Descripcion = "Descripción válida",
                CantidadMonedasBrinda = 10,
                EsAsignacionMutua = false
            };

            LogicaNegocio.Entidades.Medalla capturada = null;
            _repoMedallasMock
                .Setup(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()))
                .Callback<LogicaNegocio.Entidades.Medalla>(m => capturada = m)
                .ReturnsAsync(Resultado.Exitoso());

            // Act
            var resultado = await _servicio.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Once);
            Assert.NotNull(capturada);
            Assert.Equal(dto.Nombre, capturada.Nombre);
            Assert.Equal(dto.Descripcion, capturada.Descripcion);
            Assert.Equal(dto.UrlImagen, capturada.NombreImagenMiniatura);
            Assert.Equal(dto.CantidadMonedasBrinda, capturada.MonedasOtorgadas);
            Assert.Equal(dto.EsAsignacionMutua, capturada.TieneAsignacionMutua);
            Assert.Equal(ProfesorId, capturada.ProfesorId);
        }

        [Fact]
        public async Task EjecutarAsync_AddAsyncFalla_PropagaErrores()
        {
            // Arrange
            var dto = new MedallaAltaDto
            {
                UrlImagen = "icono.png",
                Nombre = "NombreVálido",
                Descripcion = "Descripción válida",
                CantidadMonedasBrinda = 3,
                EsAsignacionMutua = true
            };
            var errorRepo = new Error("Repo.Error", "Fallo BD");
            _repoMedallasMock
                .Setup(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()))
                .ReturnsAsync(Resultado.Falla(errorRepo));

            // Act
            var resultado = await _servicio.EjecutarAsync(dto, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == errorRepo.Codigo && e.Mensaje == errorRepo.Mensaje);
            _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Once);
        }
    }
}

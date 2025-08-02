using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaNegocio.Resultados;
using Entidad = LogicaNegocio.Entidades;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Medalla
{
    public class PruebasModificarMedalla
    {
        private readonly Mock<IRepositorioMedallas> _repoMedallasMock;
        private readonly ModificarMedalla _servicio;

        private const string ProfesorCorrecto = "prof1";
        private const string ProfesorIncorrecto = "otro";

        public PruebasModificarMedalla()
        {
            _repoMedallasMock = new Mock<IRepositorioMedallas>();
            _servicio = new ModificarMedalla(_repoMedallasMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_IdInvalido_RetornaErrorValidacion()
        {
            int idInvalido = 0;
            var dto = new MedallaEditarDto
            {
                Nombre = "NombreValido",
                Descripcion = "Desc",
                NombreIcono = "url",
                CantidadMonedasBrinda = 1,
                EsAsignacionMutua = false
            };

            var resultado = await _servicio.EjecutarAsync(idInvalido, dto, ProfesorCorrecto);

            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("entero positivo")));
            _repoMedallasMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_DtoNulo_RetornaErrorValidacion()
        {
            var resultado = await _servicio.EjecutarAsync(1, null, ProfesorCorrecto);

            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("no pueden ser nulos")));
            _repoMedallasMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_MedallaNoExiste_RetornaErrorNotFound()
        {
            int id = 10;
            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Falla(Error.NotFound));

            var dto = new MedallaEditarDto
            {
                Nombre = "NombreValido",
                Descripcion = "Desc",
                NombreIcono = "url",
                CantidadMonedasBrinda = 2,
                EsAsignacionMutua = true
            };

            var resultado = await _servicio.EjecutarAsync(id, dto, ProfesorCorrecto);

            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains($"ID {id}")));
            _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _repoMedallasMock.Verify(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ProfesorNoAutorizado_RetornaForbidden()
        {
            int id = 12;
            var entidad = new Entidad.Medalla
            {
                Id = id,
                Nombre = "NombreValido",
                Descripcion = "Desc",
                NombreIcono = "icono",
                MonedasOtorgadas = 5,
                TieneAsignacionMutua = false,
                ProfesorId = ProfesorCorrecto
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

            var dto = new MedallaEditarDto
            {
                Nombre = "NombreNuevo",
                Descripcion = "Descripcion",
                NombreIcono = "nuevaUrl",
                CantidadMonedasBrinda = 3,
                EsAsignacionMutua = false
            };

            var resultado = await _servicio.EjecutarAsync(id, dto, ProfesorIncorrecto);

            Assert.True(resultado.EsFallo);
            Assert.Contains("No tienes permiso", resultado.Errores.First().Mensaje);
            _repoMedallasMock.Verify(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ValidacionFalla_NoLlamaUpdate()
        {
            // Arrange
            int id = 20;
            var entidad = new Entidad.Medalla
            {
                Id = id,
                NombreIcono = "vieja",
                Nombre = "NombreValido",
                Descripcion = "Desc",
                MonedasOtorgadas = 5,
                TieneAsignacionMutua = false,
                ProfesorId = "prof1"   // Asegurarse que esté asignado
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

            var dto = new MedallaEditarDto
            {
                Nombre = "ab", // menos de 3 caracteres para provocar fallo en validación
                Descripcion = "DescNueva",
                NombreIcono = "urlNueva",
                CantidadMonedasBrinda = 3,
                EsAsignacionMutua = true
            };

            // Act
            var resultado = await _servicio.EjecutarAsync(id, dto, "prof1");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.NotNull(resultado.Errores);
            Assert.NotEmpty(resultado.Errores);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("al menos 3 caracteres"));
            _repoMedallasMock.Verify(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_Valido_LlamaUpdateYRetornaExitoso()
        {
            int id = 30;
            var entidad = new Entidad.Medalla
            {
                Id = id,
                Nombre = "NombreViejo",
                Descripcion = "Desc",
                NombreIcono = "viejaUrl",
                MonedasOtorgadas = 5,
                TieneAsignacionMutua = false,
                ProfesorId = ProfesorCorrecto
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

            _repoMedallasMock
                .Setup(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()))
                .ReturnsAsync(Resultado.Exitoso());

            var dto = new MedallaEditarDto
            {
                Nombre = "NombreNuevo",
                Descripcion = "NuevaDesc",
                NombreIcono = "nuevaUrl",
                CantidadMonedasBrinda = 10,
                EsAsignacionMutua = true
            };

            var resultado = await _servicio.EjecutarAsync(id, dto, ProfesorCorrecto);

            Assert.False(resultado.EsFallo);
            _repoMedallasMock.Verify(r => r.UpdateAsync(It.Is<Entidad.Medalla>(m =>
                m.Id == id &&
                m.Nombre == dto.Nombre &&
                m.Descripcion == dto.Descripcion &&
                m.NombreIcono == dto.NombreIcono &&
                m.MonedasOtorgadas == dto.CantidadMonedasBrinda &&
                m.TieneAsignacionMutua == dto.EsAsignacionMutua
            )), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_UpdateFalla_PropagaError()
        {
            int id = 40;
            var entidad = new Entidad.Medalla
            {
                Id = id,
                Nombre = "NombreViejo",
                Descripcion = "Desc",
                NombreIcono = "viejaUrl",
                MonedasOtorgadas = 5,
                TieneAsignacionMutua = false,
                ProfesorId = ProfesorCorrecto
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

            var mensajeError = "Error al actualizar";
            _repoMedallasMock
                .Setup(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()))
                .ReturnsAsync(Resultado.Falla(new Error("Repo.Update", mensajeError)));

            var dto = new MedallaEditarDto
            {
                Nombre = "NombreNuevo",
                Descripcion = "NuevaDesc",
                NombreIcono = "nuevaUrl",
                CantidadMonedasBrinda = 8,
                EsAsignacionMutua = true
            };

            var resultado = await _servicio.EjecutarAsync(id, dto, ProfesorCorrecto);

            Assert.True(resultado.EsFallo);
            Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains(mensajeError)));
        }
    }
}

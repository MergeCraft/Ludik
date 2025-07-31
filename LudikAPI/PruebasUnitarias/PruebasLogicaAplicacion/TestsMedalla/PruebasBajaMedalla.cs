using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaNegocio.Resultados;

namespace PruebasUnitarias.PruebasLogicaAplicacion.Medalla
{
    public class PruebasBajaMedalla
    {
        private readonly Mock<IRepositorioMedallas> _repoMedallasMock;
        private readonly Mock<IRepositorioProfesores> _repoProfesoresMock;
        private readonly BajaMedalla _servicio;
        private const string ProfesorId = "prof123";

        public PruebasBajaMedalla()
        {
            _repoMedallasMock = new Mock<IRepositorioMedallas>();
            _repoProfesoresMock = new Mock<IRepositorioProfesores>();
            _servicio = new BajaMedalla(_repoMedallasMock.Object, _repoProfesoresMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_IdInvalido_RetornaErrorValidacion()
        {
            int idInvalido = 0;

            var resultado = await _servicio.EjecutarAsync(idInvalido, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains("entero positivo", resultado.Errores.First().Mensaje);
            _repoMedallasMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_MedallaNoExiste_RetornaNotFound()
        {
            int id = 5;
            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Falla(Error.NotFound));

            var resultado = await _servicio.EjecutarAsync(id, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains("No se encontró ninguna medalla", resultado.Errores.First().Mensaje);
            _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_GetByIdRetornaNull_RetornaNotFound()
        {
            int id = 6;
            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Exitoso(null!));

            var resultado = await _servicio.EjecutarAsync(id, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains("No se encontró ninguna medalla", resultado.Errores.First().Mensaje);
            _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_MedallaNoPerteneceAProfesor_RetornaErrorValidacion()
        {
            int id = 9;
            var entidad = new LogicaNegocio.Entidades.Medalla
            {
                Id = id,
                Nombre = "X",
                ProfesorId = "otroProfesor"
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Exitoso(entidad));

            var resultado = await _servicio.EjecutarAsync(id, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains("No se encuentra dentro de la lista de medallas", resultado.Errores.First().Mensaje);
            _repoMedallasMock.Verify(r => r.RemoveAsync(It.IsAny<LogicaNegocio.Entidades.Medalla>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_RemoveSuccess_RetornaExitoso()
        {
            int id = 7;
            var entidad = new LogicaNegocio.Entidades.Medalla
            {
                Id = id,
                Nombre = "NombreValido",
                Descripcion = "Desc",
                NombreImagenMiniatura = "url",
                MonedasOtorgadas = 2,
                TieneAsignacionMutua = false,
                ProfesorId = ProfesorId
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Exitoso(entidad));
            _repoMedallasMock
                .Setup(r => r.RemoveAsync(entidad))
                .ReturnsAsync(Resultado.Exitoso());

            var resultado = await _servicio.EjecutarAsync(id, ProfesorId);

            Assert.True(resultado.EsExitoso);
            _repoMedallasMock.Verify(r => r.RemoveAsync(entidad), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_RemoveFail_PropagaError()
        {
            int id = 8;
            var entidad = new LogicaNegocio.Entidades.Medalla
            {
                Id = id,
                Nombre = "NombreValido",
                Descripcion = "Desc",
                NombreImagenMiniatura = "url",
                MonedasOtorgadas = 3,
                TieneAsignacionMutua = true,
                ProfesorId = ProfesorId
            };

            _repoMedallasMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Exitoso(entidad));

            var mensajeError = "Error al eliminar la medalla: restricción";
            _repoMedallasMock
                .Setup(r => r.RemoveAsync(entidad))
                .ReturnsAsync(Resultado.Falla(new Error("Repo.Medalla.Remove", mensajeError)));

            var resultado = await _servicio.EjecutarAsync(id, ProfesorId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(mensajeError, resultado.Errores.First().Mensaje);
            _repoMedallasMock.Verify(r => r.RemoveAsync(entidad), Times.Once);
        }
    }
}
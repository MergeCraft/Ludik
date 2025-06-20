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
        private readonly BajaMedalla _servicio;

        //public PruebasBajaMedalla()
        //{
        //    _repoMedallasMock = new Mock<IRepositorioMedallas>();
        //    _servicio = new BajaMedalla(_repoMedallasMock.Object);
        //}

        //[Fact]
        //public async Task EjecutarAsync_IdInvalido_RetornaErrorValidacion()
        //{
        //    // Arrange
        //    int idInvalido = 0;

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(idInvalido);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("ID de la medalla debe ser un entero positivo")));
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        //    _repoMedallasMock.Verify(r => r.RemoveAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_MedallaNoExiste_GetByIdRetornaFalla_RetornaErrorNotFound()
        //{
        //    // Arrange
        //    int id = 5;
        //    // Simular que GetByIdAsync devuelve Falla (NotFound)
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Falla(Error.NotFound));

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains($"No se encontró ninguna medalla con ID {id}")));
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        //    _repoMedallasMock.Verify(r => r.RemoveAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_GetByIdExitosoPeroValorNull_RetornaErrorNotFound()
        //{
        //    // Arrange
        //    int id = 6;
        //    // Simular que GetByIdAsync devuelve Exitoso con Valor null
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso((Entidad.Medalla)null!));

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains($"No se encontró ninguna medalla con ID {id}")));
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        //    _repoMedallasMock.Verify(r => r.RemoveAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_RemoveSuccess_RetornaExitoso()
        //{
        //    // Arrange
        //    int id = 7;
        //    var entidad = new Entidad.Medalla
        //    {
        //        Id = id,
        //        Nombre = "NombreValido",
        //        Descripcion = "Desc",
        //        Icono = "url",
        //        MonedasOtorgadas = 2,
        //        TieneAsignacionMutua = false
        //    };
        //    // Simular GetByIdAsync Exitoso con entidad
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));
        //    // Simular RemoveAsync Exitoso
        //    _repoMedallasMock
        //        .Setup(r => r.RemoveAsync(entidad))
        //        .ReturnsAsync(Resultado.Exitoso());

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id);

        //    // Assert
        //    Assert.False(resultado.EsFallo);
        //    Assert.True(resultado.EsExitoso);
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        //    _repoMedallasMock.Verify(r => r.RemoveAsync(entidad), Times.Once);
        //}

        //[Fact]
        //public async Task EjecutarAsync_RemoveFail_PropagaErrorMensaje()
        //{
        //    // Arrange
        //    int id = 8;
        //    var entidad = new Entidad.Medalla
        //    {
        //        Id = id,
        //        Nombre = "NombreValido",
        //        Descripcion = "Desc",
        //        Icono = "url",
        //        MonedasOtorgadas = 3,
        //        TieneAsignacionMutua = true
        //    };
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

        //    var mensajeErrorRepo = "Error al eliminar la medalla: restricción";
        //    var errorRepo = new Error("Repositorio.Medalla.Remove.DbError", mensajeErrorRepo);
        //    _repoMedallasMock
        //        .Setup(r => r.RemoveAsync(entidad))
        //        .ReturnsAsync(Resultado.Falla(errorRepo));

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains(mensajeErrorRepo)));
        //    _repoMedallasMock.Verify(r => r.RemoveAsync(entidad), Times.Once);
        //}
    }
}
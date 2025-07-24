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
        //private readonly Mock<IRepositorioMedallas> _repoMedallasMock;
        //private readonly ModificarMedalla _servicio;

        //public PruebasModificarMedalla()
        //{
        //    _repoMedallasMock = new Mock<IRepositorioMedallas>();
        //    _servicio = new ModificarMedalla(_repoMedallasMock.Object);
        //}

        //[Fact]
        //public async Task EjecutarAsync_IdInvalido_RetornaErrorValidacion()
        //{
        //    // Arrange
        //    int idInvalido = 0;
        //    var dto = new MedallaEditarDto
        //    {
        //        Nombre = "NombreValido",
        //        Descripcion = "Desc",
        //        UrlImagen = "url",
        //        CantidadMonedasBrinda = 1,
        //        EsAsignacionMutua = false
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(idInvalido, dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    // Verificamos que en alguno de los errores aparezca el mensaje de validación de ID inválido
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("ID de la medalla debe ser un entero positivo")));
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_DtoNulo_RetornaErrorValidacion()
        //{
        //    // Arrange
        //    int idValido = 5;

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(idValido, null);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("datos para editar la medalla no pueden ser nulos")));
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_MedallaNoExiste_RetornaErrorNotFound()
        //{
        //    // Arrange
        //    int id = 10;
        //    // Simular que GetByIdAsync devuelve Falla por no encontrado
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Falla(Error.NotFound));

        //    var dto = new MedallaEditarDto
        //    {
        //        Nombre = "NombreValido",
        //        Descripcion = "Desc",
        //        UrlImagen = "url",
        //        CantidadMonedasBrinda = 2,
        //        EsAsignacionMutua = true
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id, dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    // Verificamos mensaje de not found con ID
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains($"No se encontró ninguna medalla con ID {id}")));
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        //    _repoMedallasMock.Verify(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_ValidacionFalla_NoLlamaUpdate()
        //{
        //    // Arrange
        //    int id = 20;
        //    // Entidad existente válida inicialmente
        //    var entidad = new Entidad.Medalla
        //    {
        //        Id = id,
        //        NombreImagenMiniatura = "vieja",
        //        Nombre = "NombreValido",
        //        Descripcion = "Desc",
        //        MonedasOtorgadas = 5,
        //        TieneAsignacionMutua = false
        //    };
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

        //    // DTO con nombre muy corto para provocar fallo en esValido()
        //    var dto = new MedallaEditarDto
        //    {
        //        Nombre = "abc", // <5 caracteres según esValido()
        //        Descripcion = "DescNueva",
        //        UrlImagen = "urlNueva",
        //        CantidadMonedasBrinda = 3,
        //        EsAsignacionMutua = true
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id, dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    // Verificamos mensaje de validación de longitud de nombre
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("al menos 5 caracteres")));
        //    _repoMedallasMock.Verify(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_Valido_LlamaUpdateYRetornaExitoso()
        //{
        //    // Arrange
        //    int id = 30;
        //    var entidadOriginal = new Entidad.Medalla
        //    {
        //        Id = id,
        //        NombreImagenMiniatura = "viejaIcono",
        //        Nombre = "NombreValido",
        //        Descripcion = "DescOriginal",
        //        MonedasOtorgadas = 5,
        //        TieneAsignacionMutua = false
        //    };
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidadOriginal));

        //    _repoMedallasMock
        //        .Setup(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()))
        //        .ReturnsAsync(Resultado.Exitoso());

        //    var dto = new MedallaEditarDto
        //    {
        //        Nombre = "NombreNuevo",
        //        Descripcion = "DescNueva",
        //        UrlImagen = "urlNueva",
        //        CantidadMonedasBrinda = 10,
        //        EsAsignacionMutua = true
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id, dto);

        //    // Assert
        //    Assert.False(resultado.EsFallo);
        //    _repoMedallasMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        //    _repoMedallasMock.Verify(r => r.UpdateAsync(It.Is<Entidad.Medalla>(m =>
        //        m.Id == id &&
        //        m.Nombre == "NombreNuevo" &&
        //        m.Descripcion == "DescNueva" &&
        //        m.NombreImagenMiniatura == "urlNueva" &&
        //        m.MonedasOtorgadas == 10 &&
        //        m.TieneAsignacionMutua == true
        //    )), Times.Once);
        //}

        //[Fact]
        //public async Task EjecutarAsync_UpdateFail_PropagaErrorMensaje()
        //{
        //    // Arrange
        //    int id = 40;
        //    var entidad = new Entidad.Medalla
        //    {
        //        Id = id,
        //        NombreImagenMiniatura = "viejaIcono",
        //        Nombre = "NombreValido",
        //        Descripcion = "DescOriginal",
        //        MonedasOtorgadas = 5,
        //        TieneAsignacionMutua = false
        //    };
        //    _repoMedallasMock
        //        .Setup(r => r.GetByIdAsync(id))
        //        .ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(entidad));

        //    var mensajeErrorRepo = "Fallo en actualización BD";
        //    var errorRepo = new Error("Repositorio.Medalla.Update.DbError", mensajeErrorRepo);
        //    _repoMedallasMock
        //        .Setup(r => r.UpdateAsync(It.IsAny<Entidad.Medalla>()))
        //        .ReturnsAsync(Resultado.Falla(errorRepo));

        //    var dto = new MedallaEditarDto
        //    {
        //        Nombre = "NombreNuevo",
        //        Descripcion = "DescNueva",
        //        UrlImagen = "urlNueva",
        //        CantidadMonedasBrinda = 8,
        //        EsAsignacionMutua = false
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(id, dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    // Verificamos que el mensaje del error de repositorio se propaga en resultado.Errores
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains(mensajeErrorRepo)));
        //    _repoMedallasMock.Verify(r => r.UpdateAsync(It.Is<Entidad.Medalla>(m =>
        //        m.Id == id &&
        //        m.Nombre == "NombreNuevo"
        //    )), Times.Once);
        //}
    }
}

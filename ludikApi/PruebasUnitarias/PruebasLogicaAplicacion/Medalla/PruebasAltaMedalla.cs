using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Dominio;
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

        //public PruebasAltaMedalla()
        //{
        //    _repoMedallasMock = new Mock<IRepositorioMedallas>();
        //    _servicio = new AltaMedalla(_repoMedallasMock.Object);
        //}

        //[Fact]
        //public async Task EjecutarAsync_DtoNulo_RetornaErrorValidacion()
        //{
        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(null);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("datos para crear la medalla no pueden ser nulos")));
        //    _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<Dominio.Medalla>()), Times.Never);
        //}

        
        //public async Task EjecutarAsync_NombreInvalido_RetornaErrorValidacion(string nombre, string caso)
        //{
        //    // Arrange
        //    var dto = new MedallaAltaDto
        //    {
        //        UrlImagen = "url",
        //        Nombre = nombre,
        //        Descripcion = "Descripción válida",
        //        CantidadMonedasBrinda = 1,
        //        EsAsignacionMutua = false
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    // validaciones de nombre en esValido():
        //    // si es null/whitespace: "La medalla debe de tener un nombre."
        //    // si longitud <5: "al menos 5 caracteres"
        //    // si longitud>30: "menos de 30 caracteres"
        //    Assert.True(resultado.Errores.Any(e =>
        //        e.Mensaje.Contains("medalla") &&
        //        (e.Mensaje.Contains("tener un nombre") ||
        //         e.Mensaje.Contains("al menos 5 caracteres") ||
        //         e.Mensaje.Contains("menos de 30 caracteres"))
        //    ));
        //    _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<Dominio.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_DescripcionMuyLarga_RetornaErrorValidacion()
        //{
        //    // Arrange: descripción > 50 caracteres
        //    string descripcionLarga = new string('D', 51);
        //    var dto = new MedallaAltaDto
        //    {
        //        UrlImagen = "url",
        //        Nombre = "NombreVálido",
        //        Descripcion = descripcionLarga,
        //        CantidadMonedasBrinda = 1,
        //        EsAsignacionMutua = false
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("descripción de la medalla") && e.Mensaje.Contains("no puede superar los 50 caracteres")));
        //    _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<Dominio.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_MonedasNegativas_RetornaErrorValidacion()
        //{
        //    // Arrange
        //    var dto = new MedallaAltaDto
        //    {
        //        UrlImagen = "url",
        //        Nombre = "NombreVálido",
        //        Descripcion = "Descripción válida",
        //        CantidadMonedasBrinda = -5,
        //        EsAsignacionMutua = true
        //    };

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains("cantidad de monedas otorgadas") && e.Mensaje.Contains("no puede ser menor a 0")));
        //    _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<Dominio.Medalla>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_AddAsyncExitoso_RetornaExitoso()
        //{
        //    // Arrange
        //    var dto = new MedallaAltaDto
        //    {
        //        UrlImagen = "icono.png",
        //        Nombre = "NombreVálido",
        //        Descripcion = "Descripción válida",
        //        CantidadMonedasBrinda = 10,
        //        EsAsignacionMutua = false
        //    };

        //    Dominio.Medalla medallaEsperada = null;
        //    _repoMedallasMock
        //        .Setup(r => r.AddAsync(It.IsAny<Dominio.Medalla>()))
        //        .Callback<Dominio.Medalla>(m => medallaEsperada = m)
        //        .ReturnsAsync(Resultado.Exitoso());

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(dto);

        //    // Assert
        //    Assert.False(resultado.EsFallo);
        //    // Verificar que AddAsync fue llamado con una entidad que refleja el DTO
        //    _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<Dominio.Medalla>()), Times.Once);
        //    Assert.NotNull(medallaEsperada);
        //    Assert.Equal(dto.Nombre, medallaEsperada.Nombre);
        //    Assert.Equal(dto.Descripcion, medallaEsperada.Descripcion);
        //    Assert.Equal(dto.UrlImagen, medallaEsperada.Icono);
        //    Assert.Equal(dto.CantidadMonedasBrinda, medallaEsperada.MonedasOtorgadas);
        //    Assert.Equal(dto.EsAsignacionMutua, medallaEsperada.TieneAsignacionMutua);
        //}

        //[Fact]
        //public async Task EjecutarAsync_AddAsyncFalla_PropagaErrorMensaje()
        //{
        //    // Arrange
        //    var dto = new MedallaAltaDto
        //    {
        //        UrlImagen = "icono.png",
        //        Nombre = "NombreVálido",
        //        Descripcion = "Descripción válida",
        //        CantidadMonedasBrinda = 3,
        //        EsAsignacionMutua = true
        //    };

        //    var mensajeErrorRepo = "Error al guardar en BD";
        //    var errorRepo = new Error("Repositorio.Medalla.Add.DbError", mensajeErrorRepo);
        //    _repoMedallasMock
        //        .Setup(r => r.AddAsync(It.IsAny<Dominio.Medalla>()))
        //        .ReturnsAsync(Resultado.Falla(errorRepo));

        //    // Act
        //    var resultado = await _servicio.EjecutarAsync(dto);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.True(resultado.Errores.Any(e => e.Mensaje.Contains(mensajeErrorRepo)));
        //    _repoMedallasMock.Verify(r => r.AddAsync(It.IsAny<Dominio.Medalla>()), Times.Once);
        //}
    }
}

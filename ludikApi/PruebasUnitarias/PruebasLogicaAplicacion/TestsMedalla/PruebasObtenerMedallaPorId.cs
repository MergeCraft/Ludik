using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsMedalla
{
    public class PruebasObtenerMedallaPorId
    {
        private readonly Mock<IRepositorioMedallas> _repoMock;
        private readonly Mock<IGeneradorUrlImagen> _generadorUrlImagenMock;
        private readonly ObtenerMedallaPorId _casoUso;
        private const int IdMedalla = 1;

        public PruebasObtenerMedallaPorId()
        {
            _repoMock = new Mock<IRepositorioMedallas>();
            _generadorUrlImagenMock = new Mock<IGeneradorUrlImagen>();

            // Setup básico para que devuelva la misma url que recibe
            _generadorUrlImagenMock
                .Setup(g => g.GenerarUrlLecturaAsync(It.IsAny<string>()))
                .ReturnsAsync((string url) => url);

            _casoUso = new ObtenerMedallaPorId(_repoMock.Object, _generadorUrlImagenMock.Object);
        }

        [Fact]
        public async Task MedallaNoExiste_RetornaFallo()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetByIdAsync(IdMedalla))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Falla(Error.NotFound));

            // Act
            var resultado = await _casoUso.EjecutarAsync(IdMedalla);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.NotFound");
        }

        [Fact]
        public async Task MedallaExiste_RetornaDtoCorrecto()
        {
            // Arrange
            var medalla = new LogicaNegocio.Entidades.Medalla
            {
                Id = IdMedalla,
                Nombre = "Colaborador",
                Descripcion = "Ayuda frecuentemente a sus compañeros",
                NombreIcono = "img/colaborador.png",
                MonedasOtorgadas = 50,
                TieneAsignacionMutua = true
            };

            _repoMock
                .Setup(r => r.GetByIdAsync(IdMedalla))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Medalla>.Exitoso(medalla));

            // Act
            var resultado = await _casoUso.EjecutarAsync(IdMedalla);

            // Assert
            Assert.True(resultado.EsExitoso);

            var dto = resultado.Valor!;
            Assert.Equal(medalla.Id, dto.Id);
            Assert.Equal(medalla.Nombre, dto.Nombre);
            Assert.Equal(medalla.Descripcion, dto.Descripcion);
            Assert.Equal(medalla.NombreIcono, dto.NombreIcono);
            Assert.Equal(medalla.MonedasOtorgadas, dto.CantidadMedallasBrinda);
            Assert.Equal(medalla.TieneAsignacionMutua, dto.EsAsignacionMutua);
        }
    }
}

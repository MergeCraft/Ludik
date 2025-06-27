using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Medallas;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsMedalla
{
    public class PruebasObtenerTodasLasMedallas
    {
        private readonly Mock<IRepositorioMedallas> _repoMock;
        private readonly ObtenerTodasLasMedallas _casoUso;

        public PruebasObtenerTodasLasMedallas()
        {
            _repoMock = new Mock<IRepositorioMedallas>();
            _casoUso = new ObtenerTodasLasMedallas(_repoMock.Object);
        }

        [Fact]
        public async Task RepositorioFalla_RetornaFallo()
        {
            // Arrange
            var errores = new List<Error> { new Error("Error.DB", "Fallo en base de datos") };
            _repoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Medalla>>.Falla(errores));

            // Act
            var resultado = await _casoUso.EjecutarAsync();

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(errores, resultado.Errores);
        }

        [Fact]
        public async Task RepositorioExitoso_RetornaListaDto()
        {
            // Arrange
            var medallas = new List<LogicaNegocio.Entidades.Medalla>
            {
                new LogicaNegocio.Entidades.Medalla
                {
                    Id = 1,
                    Nombre = "Responsable",
                    Descripcion = "Cumple siempre con sus tareas",
                    UrlImagenMiniatura = "img/responsable.png",
                    MonedasOtorgadas = 20,
                    TieneAsignacionMutua = false
                },
                new LogicaNegocio.Entidades.Medalla
                {
                    Id = 2,
                    Nombre = "Colaborador",
                    Descripcion = "Ayuda a otros estudiantes",
                    UrlImagenMiniatura = "img/colaborador.png",
                    MonedasOtorgadas = 30,
                    TieneAsignacionMutua = true
                }
            };

            _repoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Medalla>>.Exitoso(medallas));

            // Act
            var resultado = await _casoUso.EjecutarAsync();

            // Assert
            Assert.True(resultado.EsExitoso);
            var lista = resultado.Valor!.ToList();

            Assert.Equal(2, lista.Count);
            Assert.Contains(lista, m => m.Nombre == "Responsable" && m.CantidadMedallasBrinda == 20);
            Assert.Contains(lista, m => m.Nombre == "Colaborador" && m.EsAsignacionMutua);
        }
    }
}

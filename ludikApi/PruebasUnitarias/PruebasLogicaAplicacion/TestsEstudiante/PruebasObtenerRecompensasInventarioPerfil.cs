using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsEstudiante
{
    public class PruebasObtenerRecompensasInventarioPerfil
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly Mock<IGeneradorUrlsParaColeccionesImagenes> _mockUrlGen;
        private readonly ObtenerRecompensasInventarioPerfil _casoUso;

        private const int PerfilId = 101;
        private const string EstudianteId = "est-1";

        public PruebasObtenerRecompensasInventarioPerfil()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _mockUrlGen = new Mock<IGeneradorUrlsParaColeccionesImagenes>();
            _casoUso = new ObtenerRecompensasInventarioPerfil(_mockPerfilRepo.Object, _mockUrlGen.Object);
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaNotFound()
        {
            // Arrange: GetByIdAsync falla
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Falla(
                    new Error("Error.NotFound", "no existe")));

            // Act
            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.NotFound");
            // No verificamos ObtenerRecompensasInventarioAsync porque el caso de uso lo invoca antes
        }

        [Fact]
        public async Task PerfilNoTieneRecompensas_RetornaEmptyList()
        {
            // Arrange: perfil válido pero sin recompensas
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, EstudianteId = EstudianteId };
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));
            _mockPerfilRepo
                .Setup(r => r.ObtenerRecompensasInventarioAsync(PerfilId))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Exitoso(
                    Enumerable.Empty<LogicaNegocio.Entidades.Recompensa>()));

            // Act
            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.Empty(resultado.Valor);
        }

        [Fact]
        public async Task PerfilNoPerteneceEstudiante_RetornaForbidden()
        {
            // Arrange: perfil existe pero de otro estudiante
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, EstudianteId = "otro-est" };
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));
            _mockPerfilRepo
                .Setup(r => r.ObtenerRecompensasInventarioAsync(PerfilId))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Exitoso(
                    Enumerable.Empty<LogicaNegocio.Entidades.Recompensa>()));

            // Act
            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Forbidden");
        }

        [Fact]
        public async Task CaminoFeliz_RetornaListadoDeRecompensas()
        {
            // Arrange: perfil válido y dos recompensas en el inventario
            var r1 = new LogicaNegocio.Entidades.RecompensaSimple
            {
                Id = 1,
                Nombre = "Espada mágica",
                NombreImagenCompleta = "url1",
                NombreImagenMiniatura = "mini1",
                Precio = 100,
                RequiereImagen = true
            };
            var r2 = new LogicaNegocio.Entidades.RecompensaSimple
            {
                Id = 2,
                Nombre = "Escudo legendario",
                NombreImagenCompleta = "url2",
                NombreImagenMiniatura = "mini2",
                Precio = 150,
                RequiereImagen = false
            };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, EstudianteId = EstudianteId };

            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));
            _mockPerfilRepo
                .Setup(r => r.ObtenerRecompensasInventarioAsync(PerfilId))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.Recompensa>>.Exitoso(
                    new LogicaNegocio.Entidades.Recompensa[] { r1, r2 }));

            // Mock: el método recibe un List<RecompensaDto> y un array de tuplas
            _mockUrlGen
                .Setup(g => g.EjecutarProcesarUrlsAsync<RecompensaDto>(
                    It.IsAny<List<RecompensaDto>>(),
                    It.IsAny<(System.Func<RecompensaDto, string>, System.Action<RecompensaDto, string>)[]>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            // Assert
            Assert.True(resultado.EsExitoso);

            var listaDtos = resultado.Valor;
            Assert.Equal(2, listaDtos.Count);
            Assert.Contains(listaDtos, dto =>
                dto.Nombre == "Espada mágica" && dto.Precio == 100 && dto.RequiereImagen);
            Assert.Contains(listaDtos, dto =>
                dto.Nombre == "Escudo legendario" && dto.Precio == 150 && !dto.RequiereImagen);

            _mockUrlGen.Verify(g => g.EjecutarProcesarUrlsAsync<RecompensaDto>(
                It.Is<List<RecompensaDto>>(l => l.Count == 2),
                It.Is<(System.Func<RecompensaDto, string>, System.Action<RecompensaDto, string>)[]>(arr => arr.Length == 2)
            ), Times.Once);
        }
    }
}

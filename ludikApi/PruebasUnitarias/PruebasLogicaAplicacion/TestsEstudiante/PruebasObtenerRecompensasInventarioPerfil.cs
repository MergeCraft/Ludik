using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.EntidadesAuxiliares;
using LogicaNegocio.Resultados;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsEstudiante
{
    public class PruebasObtenerRecompensasInventarioPerfil
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly Mock<IRecompensaEnricher> _mockRecompensaEnricher;
        private readonly Mock<IRepositorioTiendas> _mockRepoTiendas;
        private readonly ObtenerRecompensasInventarioPerfil _casoUso;

        private const int PerfilId = 101;
        private const string EstudianteId = "est-1";

        public PruebasObtenerRecompensasInventarioPerfil()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _mockRecompensaEnricher = new Mock<IRecompensaEnricher>();
            _mockRepoTiendas = new Mock<IRepositorioTiendas>();
            _casoUso = new ObtenerRecompensasInventarioPerfil(_mockPerfilRepo.Object, _mockRecompensaEnricher.Object);
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
            // ARRANGE
            var recompensaIcono = new RecompensaSimple { Id = 1, Nombre = "Icono Genial", Precio = 5 };
            ((RepresentacionIcono)recompensaIcono.Representacion).NombreIcono = "fa-icon";

            var recompensaImagen = new PersonalizacionAvatar { Id = 2, Nombre = "Avatar Increíble", Precio = 10 };
            ((RepresentacionImagen)recompensaImagen.Representacion).NombreImagenCompleta = "completa.png";
            ((RepresentacionImagen)recompensaImagen.Representacion).NombreImagenMiniatura = "mini.png";

            var recompensasDeDominio = new List<LogicaNegocio.Entidades.Recompensa> { recompensaIcono, recompensaImagen };
            var tienda = new LogicaNegocio.Entidades.Tienda { Id = 20, Recompesas = recompensasDeDominio };

            _mockRepoTiendas
                .Setup(r => r.GetByIdAsync(20))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Tienda>.Exitoso(tienda));

            // Preparamos la respuesta que ESPERAMOS del enricher
            var dtosEsperados = new List<RecompensaClienteDto>
            {
                new RecompensaClienteDto { Id = 1, Nombre = "Icono Genial", Datos = new RespuestaIconoDto("fa-icon") },
                new RecompensaClienteDto { Id = 2, Nombre = "Avatar Increíble", Datos = new RespuestaImagenDto("url-fake/mini.png", "url-fake/completa.png") }
            };

            // Configuramos el mock del enricher para que devuelva nuestra lista esperada
            _mockRecompensaEnricher
                .Setup(e => e.EnrichAsync(recompensasDeDominio))
                .ReturnsAsync(dtosEsperados);

            // ACT
            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            // ASSERT
            Assert.True(resultado.EsExitoso);
            var listaResultado = resultado.Valor!.ToList();

            // 4. Verificamos que el resultado del caso de uso es exactamente lo que devolvió el mock.
            Assert.Equal(2, listaResultado.Count);
            Assert.Equal(dtosEsperados, listaResultado);

            // 5. (Opcional pero recomendado) Verificamos que los mocks fueron llamados.
            _mockRepoTiendas.Verify(r => r.GetByIdAsync(20), Times.Once);
            _mockRecompensaEnricher.Verify(e => e.EnrichAsync(recompensasDeDominio), Times.Once);
        }
    }
}

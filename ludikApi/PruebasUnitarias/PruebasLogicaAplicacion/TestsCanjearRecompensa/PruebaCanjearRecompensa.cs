using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsCanjearRecompensa
{
    public class PruebaCanjearRecompensa
    {
        private readonly Mock<IRepositorioRecompensas> _mockRepoRecompensas;
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockRepoPerfil;
        private readonly CanjearRecompensa _casoUso;

        private const int RecompensaId = 1;
        private const int PerfilId = 2;
        private const string EstudianteId = "est-123";

        public PruebaCanjearRecompensa()
        {
            _mockRepoRecompensas = new Mock<IRepositorioRecompensas>();
            _mockRepoPerfil = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _casoUso = new CanjearRecompensa(_mockRepoRecompensas.Object, _mockRepoPerfil.Object);
        }

        [Fact]
        public async Task RecompensaNoExiste_RetornaErrorNotFound()
        {
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Falla(new Error("X", "error")));

            var resultado = await _casoUso.EjecutarAsync(RecompensaId, PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.NotFound" && e.Mensaje.Contains("recompensa"));
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaErrorNotFound()
        {
            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(new RecompensaSimple { Id = RecompensaId, Precio = 10 }));

            _mockRepoPerfil
                .Setup(p => p.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Falla(new Error("X", "error")));

            var resultado = await _casoUso.EjecutarAsync(RecompensaId, PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.NotFound" && e.Mensaje.Contains("perfil"));
        }

        [Fact]
        public async Task MonedasInsuficientes_RetornaErrorValidation()
        {
            var recompensa = new RecompensaSimple { Id = RecompensaId, Precio = 20 };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, Monedas = 5, EstudianteId = EstudianteId };

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));
            _mockRepoPerfil
                .Setup(p => p.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var resultado = await _casoUso.EjecutarAsync(RecompensaId, PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Validation" && e.Mensaje.Contains("suficientes puntos"));
        }

        [Fact]
        public async Task EstudianteNoEsElDueño_RetornaErrorForbidden()
        {
            var recompensa = new RecompensaSimple { Id = RecompensaId, Precio = 10 };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, Monedas = 100, EstudianteId = "otro-est" };

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));
            _mockRepoPerfil
                .Setup(p => p.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var resultado = await _casoUso.EjecutarAsync(RecompensaId, PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Forbidden");
        }

        [Fact]
        public async Task FallaAlActualizarPerfil_RetornaErrorUnexpected()
        {
            var recompensa = new RecompensaSimple { Id = RecompensaId, Precio = 10 };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                Monedas = 50,
                EstudianteId = EstudianteId,
                InventarioRecompensas = new List<PerfilEstudianteRecompensa>()
            };

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));
            _mockRepoPerfil
                .Setup(p => p.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));
            _mockRepoPerfil
                .Setup(p => p.UpdateAsync(perfil))
                .ReturnsAsync(Resultado.Falla(new Error("Error.Unexpected", "Error al guardar")));

            var resultado = await _casoUso.EjecutarAsync(RecompensaId, PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Unexpected");
        }

        [Fact]
        public async Task CaminoFeliz_CanjeExitoso()
        {
            var recompensa = new RecompensaSimple { Id = RecompensaId, Precio = 15 };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                Monedas = 20,
                EstudianteId = EstudianteId,
                InventarioRecompensas = new List<PerfilEstudianteRecompensa>()
            };

            _mockRepoRecompensas
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));
            _mockRepoPerfil
                .Setup(p => p.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));
            _mockRepoPerfil
                .Setup(p => p.UpdateAsync(perfil))
                .ReturnsAsync(Resultado.Exitoso());

            var resultado = await _casoUso.EjecutarAsync(RecompensaId, PerfilId, EstudianteId);

            Assert.True(resultado.EsExitoso);
            Assert.Equal(5, perfil.Monedas); // 20 - 15
            Assert.Single(perfil.InventarioRecompensas);
            Assert.Equal(RecompensaId, perfil.InventarioRecompensas.First().RecompensaId);
        }
    }
}

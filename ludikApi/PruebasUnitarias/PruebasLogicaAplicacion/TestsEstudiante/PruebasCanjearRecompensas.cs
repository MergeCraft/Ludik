using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.PruebasEstudiante
{
    public class PruebasCanjearRecompensa
    {
        private readonly Mock<IRepositorioRecompensas> _mockRecRepo;
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly Mock<IRepositorioPerfilEstudianteRecompensa> _mockPerfilRecRepo;
        private readonly CanjearRecompensa _casoUso;

        private const int RecId = 5;
        private const int PerfilId = 7;
        private const string EstudianteId = "user-7";

        public PruebasCanjearRecompensa()
        {
            _mockRecRepo = new Mock<IRepositorioRecompensas>();
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _mockPerfilRecRepo = new Mock<IRepositorioPerfilEstudianteRecompensa>();
            _casoUso = new CanjearRecompensa(
                _mockRecRepo.Object,
                _mockPerfilRepo.Object,
                _mockPerfilRecRepo.Object
            );
        }

        [Fact]
        public async Task RecompensaNoExiste_RetornaNotFound()
        {
            _mockRecRepo.Setup(r => r.GetByIdAsync(RecId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Falla(new Error("Error.NotFound", "No se encontró la recompensa especificada.")));

            var res = await _casoUso.EjecutarAsync(RecId, PerfilId, EstudianteId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.NotFound", res.Errores.First().Codigo);
            Assert.Contains("No se encontró la recompensa especificada", res.Errores.First().Mensaje);
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaNotFound()
        {
            _mockRecRepo.Setup(r => r.GetByIdAsync(RecId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(new RecompensaSimple { Id = RecId, Precio = 10 }));
            _mockPerfilRepo.Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Falla(new Error("Error.NotFound", "No se encontró el perfil del estudiante especificado.")));

            var res = await _casoUso.EjecutarAsync(RecId, PerfilId, EstudianteId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.NotFound", res.Errores.First().Codigo);
            Assert.Contains("No se encontró el perfil del estudiante especificado", res.Errores.First().Mensaje);
        }

        [Fact]
        public async Task MonedasInsuficientes_RetornaValidationError()
        {
            var rec = new LogicaNegocio.Entidades.RecompensaSimple { Id = RecId, Precio = 100 };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, EstudianteId = EstudianteId, Monedas = 50 };

            _mockRecRepo.Setup(r => r.GetByIdAsync(RecId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(rec));
            _mockPerfilRepo.Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var res = await _casoUso.EjecutarAsync(RecId, PerfilId, EstudianteId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.Validation", res.Errores.First().Codigo);
            Assert.Contains("puntos", res.Errores.First().Mensaje);
        }

        [Fact]
        public async Task UsuarioNoAutorizado_RetornaForbidden()
        {
            var rec = new LogicaNegocio.Entidades.RecompensaSimple { Id = RecId, Precio = 10 };
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilId, EstudianteId = "otro", Monedas = 100 };

            _mockRecRepo.Setup(r => r.GetByIdAsync(RecId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(rec));
            _mockPerfilRepo.Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var res = await _casoUso.EjecutarAsync(RecId, PerfilId, EstudianteId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.Forbidden", res.Errores.First().Codigo);
        }

        

        

    }
}

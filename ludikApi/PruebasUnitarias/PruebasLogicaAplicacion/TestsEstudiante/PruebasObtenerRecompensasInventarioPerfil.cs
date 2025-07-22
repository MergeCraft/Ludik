using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.RecompensaDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Estudiantes;
using LogicaAplicacion.ImplementacionServicios;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsEstudiante
{
    public class PruebasObtenerRecompensasInventarioPerfil
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;

        private readonly IGeneradorUrlImagen _generadorUrlImagen =
            new GeneradorUrlImagen(new Mock<IRepositorioAlmacenamientoArchivos>().Object);
        private readonly ObtenerRecompensasInventarioPerfil _casoUso;

        private const int PerfilId = 101;
        private const string EstudianteId = "est-1";

        public PruebasObtenerRecompensasInventarioPerfil()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _casoUso = new ObtenerRecompensasInventarioPerfil(_mockPerfilRepo.Object, _generadorUrlImagen);
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaNotFound()
        {
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Falla(Error.NotFound));

            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.NotFound");
        }

        [Fact]
        public async Task PerfilNoPerteneceEstudiante_RetornaForbidden()
        {
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                EstudianteId = "otro-estudiante"
            };

            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Forbidden");
        }

        [Fact]
        public async Task CaminoFeliz_RetornaListadoDeRecompensas()
        {
            var recompensa1 = new RecompensaSimple
            {
                Nombre = "Espada mágica",
                NombreImagenCompleta = "url1",
                NombreImagenMiniatura = "mini1",
                Precio = 100
            };

            var recompensa2 = new RecompensaSimple
            {
                Nombre = "Escudo legendario",
                NombreImagenCompleta = "url2",
                NombreImagenMiniatura = "mini2",
                Precio = 150
            };

            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                EstudianteId = EstudianteId,
                InventarioRecompensas = new List<PerfilEstudianteRecompensa>
                {
                    new PerfilEstudianteRecompensa { Recompensa = recompensa1 },
                    new PerfilEstudianteRecompensa { Recompensa = recompensa2 }
                }
            };

            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var resultado = await _casoUso.EjecutarAsync(PerfilId, EstudianteId);

            Assert.True(resultado.EsExitoso);
            Assert.Equal(2, resultado.Valor.Count);

            Assert.Contains(resultado.Valor, r => r.Nombre == "Espada mágica" && r.Precio == 100);
            Assert.Contains(resultado.Valor, r => r.Nombre == "Escudo legendario" && r.Precio == 150);
        }
    }
}

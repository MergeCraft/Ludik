using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad = LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;
using LogicaNegocio.InterfacesRepositorios;
using MediatR;

namespace PruebasUnitarias.PruebasLogicaAplicacion.BrindarMedalla
{
    public class PruebasAsignarMedalla
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly Mock<IRepositorioMedallas> _mockMedallasRepo;
        private readonly Mock<IRepositorioProfesores> _mockProfesoresRepo;
        private readonly Mock<IRepositorioPerfilEstudianteMedalla> _mockPerfilMedallaRepo;
        private readonly Mock<IMediator> _mockMediator;
        private readonly AsignarMedalla _casoUso;

        private const string ProfesorId = "prof-1";
        private const int PerfilId = 1;
        private const int MedallaId = 2;

        public PruebasAsignarMedalla()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _mockMedallasRepo = new Mock<IRepositorioMedallas>();
            _mockProfesoresRepo = new Mock<IRepositorioProfesores>();
            _mockPerfilMedallaRepo = new Mock<IRepositorioPerfilEstudianteMedalla>();
            _mockMediator = new Mock<IMediator>();


            _casoUso = new AsignarMedalla(
                _mockPerfilRepo.Object,
                _mockMedallasRepo.Object,
                _mockProfesoresRepo.Object,
                _mockPerfilMedallaRepo.Object,
                _mockMediator.Object
            );
        }

        [Fact]
        public async Task ProfesorNoExiste_RetornaNotFound()
        {
            _mockProfesoresRepo
                .Setup(r => r.GetByStringIdAsync(ProfesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Falla(Error.NotFound));

            var res = await _casoUso.EjecutarAsync(ProfesorId, PerfilId, MedallaId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.NotFound", res.Errores.First().Codigo);
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaNotFound()
        {
            _mockProfesoresRepo
                .Setup(r => r.GetByStringIdAsync(ProfesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(new Entidad.Profesor()));
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Falla(Error.NotFound));

            var res = await _casoUso.EjecutarAsync(ProfesorId, PerfilId, MedallaId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.NotFound", res.Errores.First().Codigo);
        }

        [Fact]
        public async Task MedallaNoExiste_RetornaNotFound()
        {
            _mockProfesoresRepo
                .Setup(r => r.GetByStringIdAsync(ProfesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(new Entidad.Profesor()));
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(new Entidad.PerfilEstudiante { GrupoId = 5 }));
            _mockMedallasRepo
                .Setup(r => r.GetByIdAsync(MedallaId))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Falla(Error.NotFound));

            var res = await _casoUso.EjecutarAsync(ProfesorId, PerfilId, MedallaId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.NotFound", res.Errores.First().Codigo);
        }

        [Fact]
        public async Task ProfesorNoPerteneceAlGrupo_RetornaForbidden()
        {
            var profesor = new Entidad.Profesor { Id = ProfesorId, Grupos = new List<Entidad.Grupo> { new Entidad.Grupo { Id = 10 } }, Medallas = new List<Entidad.Medalla> { new Entidad.Medalla { Id = MedallaId } } };
            _mockProfesoresRepo.Setup(r => r.GetByStringIdAsync(ProfesorId)).ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));
            _mockPerfilRepo.Setup(r => r.GetByIdAsync(PerfilId)).ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(new Entidad.PerfilEstudiante { GrupoId = 20 }));
            _mockMedallasRepo.Setup(r => r.GetByIdAsync(MedallaId)).ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(new Entidad.Medalla { Id = MedallaId }));

            var res = await _casoUso.EjecutarAsync(ProfesorId, PerfilId, MedallaId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.Forbidden", res.Errores.First().Codigo);
        }

        [Fact]
        public async Task ProfesorNoPoseeMedalla_RetornaForbidden()
        {
            var profesor = new Entidad.Profesor { Id = ProfesorId, Grupos = new List<Entidad.Grupo> { new Entidad.Grupo { Id = 10 } }, Medallas = new List<Entidad.Medalla>() };
            _mockProfesoresRepo.Setup(r => r.GetByStringIdAsync(ProfesorId)).ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));
            _mockPerfilRepo.Setup(r => r.GetByIdAsync(PerfilId)).ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(new Entidad.PerfilEstudiante { GrupoId = 10 }));
            _mockMedallasRepo.Setup(r => r.GetByIdAsync(MedallaId)).ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(new Entidad.Medalla { Id = MedallaId }));

            var res = await _casoUso.EjecutarAsync(ProfesorId, PerfilId, MedallaId);

            Assert.True(res.EsFallo);
            Assert.Equal("Error.Forbidden", res.Errores.First().Codigo);
        }

        [Fact]
        public async Task CaminoFeliz_AgregaAsignacionYNotifica()
        {
            var profesor = new Entidad.Profesor { Id = ProfesorId, Grupos = new List<Entidad.Grupo> { new Entidad.Grupo { Id = 10 } }, Medallas = new List<Entidad.Medalla> { new Entidad.Medalla { Id = MedallaId } } };
            var perfilEst = new Entidad.PerfilEstudiante { Id = PerfilId, GrupoId = 10 };
            var medalla = new Entidad.Medalla { Id = MedallaId, MonedasOtorgadas = 5 };

            _mockProfesoresRepo.Setup(r => r.GetByStringIdAsync(ProfesorId)).ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));
            _mockPerfilRepo.Setup(r => r.GetByIdAsync(PerfilId)).ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(perfilEst));
            _mockMedallasRepo.Setup(r => r.GetByIdAsync(MedallaId)).ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(medalla));
            _mockPerfilMedallaRepo.Setup(r => r.AddAsync(It.IsAny<Entidad.PerfilEstudianteMedalla>()))
                .ReturnsAsync(Resultado.Exitoso());

            var res = await _casoUso.EjecutarAsync(ProfesorId, PerfilId, MedallaId);

            Assert.True(res.EsExitoso);
            _mockPerfilMedallaRepo.Verify(r => r.AddAsync(It.Is<Entidad.PerfilEstudianteMedalla>(p => p.PerfilEstudianteId == PerfilId && p.MedallaId == MedallaId)), Times.Once);
        }
    }
}
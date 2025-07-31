using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Kudo;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsKudo
{
    public class PruebasAsignarKudo
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly Mock<IRepositorioTiposKudo> _mockTiposKudoRepo;
        private readonly Mock<IRepositorioEstudiantes> _mockEstudiantesRepo;
        private readonly Mock<IRepositorioUmbralesParaMedallasPorKudos> _mockUmbralesRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly AsignarKudo _casoUso;

        private const string EmisorId = "emisor-1";
        private const int PerfilEmisorId = 10;
        private const int PerfilReceptorId = 20;

        public PruebasAsignarKudo()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _mockTiposKudoRepo = new Mock<IRepositorioTiposKudo>();
            _mockEstudiantesRepo = new Mock<IRepositorioEstudiantes>();
            _mockUmbralesRepo = new Mock<IRepositorioUmbralesParaMedallasPorKudos>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Alineamos la inyección al constructor real: perfil, tiposKudo, estudiantes, umbrales, unitOfWork
            _casoUso = new AsignarKudo(
                _mockPerfilRepo.Object,
                _mockTiposKudoRepo.Object,
                _mockEstudiantesRepo.Object,
                _mockUmbralesRepo.Object,
                _mockUnitOfWork.Object
            );

            // Default: SaveChanges devuelve 1
            _mockUnitOfWork
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Default: LogicaNegocio.Entidades.Estudiante emisor con un perfil válido
            var estudianteEmisor = new LogicaNegocio.Entidades.Estudiante { Id = EmisorId };
            estudianteEmisor.Perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>
            {
                new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilEmisorId, Grupo = new LogicaNegocio.Entidades.Grupo { Id = 1, ProfesorId = "prof-1" } }
            };
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Exitoso(estudianteEmisor));

            // Default: LogicaNegocio.Entidades.PerfilEstudiante receptor existe
            var perfilReceptor = new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilReceptorId, Grupo = new LogicaNegocio.Entidades.Grupo { Id = 1, ProfesorId = "prof-1" } };
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilReceptorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfilReceptor));

            // Default: LogicaNegocio.Entidades.TipoKudo existe
            var tipoKudo = new LogicaNegocio.Entidades.TipoKudo { Id = 5, Nombre = "Amistad", Descripcion = "Promueve la amistad", NombreImagenMiniatura = "amistad.png" };
            _mockTiposKudoRepo
                .Setup(r => r.GetByIdAsync(tipoKudo.Id))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.TipoKudo>.Exitoso(tipoKudo));

            // Default: LogicaNegocio.Entidades.UmbralParaMedallaPorKudos
            var umbral = new LogicaNegocio.Entidades.UmbralParaMedallaPorKudos { Id = 1, TipoKudoId = tipoKudo.Id, CantidadKudos = 1, Medalla = new LogicaNegocio.Entidades.Medalla { Id = 2 } };
            _mockUmbralesRepo
                .Setup(r => r.GetAllByProfesorAndGrupoIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.UmbralParaMedallaPorKudos>>.Exitoso(new[] { umbral }));
        }

        [Fact]
        public async Task EmisorNoExiste_RetornaErrorRepoEstudiantes()
        {
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Falla(new Error("Error.NotFound", "Estudiante emisor no encontrado")));

            var dto = new AsignarKudoDto
            {
                IdPerfilEstudianteEmisor = PerfilEmisorId,
                IdPerfilEstudianteRecibe = PerfilReceptorId,
                Kudo = new TipoKudoDto { Id = 5 }
            };

            var res = await _casoUso.EjecutarAsync(EmisorId, dto);

            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.NotFound");
        }

        [Fact]
        public async Task PerfilEmisorNoExiste_RetornaErrorDominio()
        {
            // Dominio: LogicaNegocio.Entidades.Estudiante sin perfiles
            var estudianteEmisor = new LogicaNegocio.Entidades.Estudiante { Id = EmisorId, Perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>() };
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Exitoso(estudianteEmisor));

            var dto = new AsignarKudoDto { IdPerfilEstudianteEmisor = PerfilEmisorId, IdPerfilEstudianteRecibe = PerfilReceptorId, Kudo = new TipoKudoDto { Id = 5 } };
            var res = await _casoUso.EjecutarAsync(EmisorId, dto);

            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.NotFound");
        }

        [Fact]
        public async Task TipoKudoNoExiste_RetornaErrorRepoTipos()
        {
            // Dominio emisor válido
            var estudianteEmisor = new LogicaNegocio.Entidades.Estudiante
            {
                Id = EmisorId,
                Perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>
                {
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilEmisorId, Grupo = new LogicaNegocio.Entidades.Grupo { Id = 1, ProfesorId = "prof-1" } }
                }
            };
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Exitoso(estudianteEmisor));

            _mockTiposKudoRepo
                .Setup(r => r.GetByIdAsync(5))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.TipoKudo>.Falla(new Error("Error.NotFound", "Tipo de kudo no encontrado")));

            var dto = new AsignarKudoDto { IdPerfilEstudianteEmisor = PerfilEmisorId, IdPerfilEstudianteRecibe = PerfilReceptorId, Kudo = new TipoKudoDto { Id = 5 } };
            var res = await _casoUso.EjecutarAsync(EmisorId, dto);

            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.NotFound");
        }

        [Fact]
        public async Task ErrorAlEvaluarUmbrales_RetornaEseError()
        {
            var estudianteEmisor = new LogicaNegocio.Entidades.Estudiante
            {
                Id = EmisorId,
                Perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>
                {
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilEmisorId, Grupo = new LogicaNegocio.Entidades.Grupo { Id = 1, ProfesorId = "prof-1" } }
                }
            };
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Exitoso(estudianteEmisor));

            _mockUmbralesRepo
                .Setup(r => r.GetAllByProfesorAndGrupoIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.UmbralParaMedallaPorKudos>>.Falla(new Error("Error.Validation", "Umbral inválido")));

            var dto = new AsignarKudoDto { IdPerfilEstudianteEmisor = PerfilEmisorId, IdPerfilEstudianteRecibe = PerfilReceptorId, Kudo = new TipoKudoDto { Id = 5 } };
            var res = await _casoUso.EjecutarAsync(EmisorId, dto);

            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.Validation");
        }

        [Fact]
        public async Task ErrorSaveChanges_RetornaUnexpected()
        {
            var estudianteEmisor = new LogicaNegocio.Entidades.Estudiante
            {
                Id = EmisorId,
                Perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>
                {
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilEmisorId, Grupo = new LogicaNegocio.Entidades.Grupo { Id = 1, ProfesorId = "prof-1" } }
                }
            };
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Exitoso(estudianteEmisor));

            // Forzar umbrales vacíos para llegar a SaveChanges
            _mockUmbralesRepo
                .Setup(r => r.GetAllByProfesorAndGrupoIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.UmbralParaMedallaPorKudos>>.Exitoso(Array.Empty<LogicaNegocio.Entidades.UmbralParaMedallaPorKudos>()));

            _mockUnitOfWork
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("fail"));

            var dto = new AsignarKudoDto { IdPerfilEstudianteEmisor = PerfilEmisorId, IdPerfilEstudianteRecibe = PerfilReceptorId, Kudo = new TipoKudoDto { Id = 5 } };
            var res = await _casoUso.EjecutarAsync(EmisorId, dto);

            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.Unexpected");
        }

        [Fact]
        public async Task CaminoFeliz_RetornaExitoso()
        {
            var estudianteEmisor = new LogicaNegocio.Entidades.Estudiante
            {
                Id = EmisorId,
                Perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>
                {
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = PerfilEmisorId, Grupo = new LogicaNegocio.Entidades.Grupo { Id = 1, ProfesorId = "prof-1" } }
                }
            };
            _mockEstudiantesRepo
                .Setup(r => r.GetByStringIdAsync(EmisorId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Estudiante>.Exitoso(estudianteEmisor));

            // Umbrales vacíos para que RecibirKudoYEvaluarMedalla use umbral null
            _mockUmbralesRepo
                .Setup(r => r.GetAllByProfesorAndGrupoIdAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(Resultado<IEnumerable<LogicaNegocio.Entidades.UmbralParaMedallaPorKudos>>.Exitoso(Array.Empty<LogicaNegocio.Entidades.UmbralParaMedallaPorKudos>()));

            var dto = new AsignarKudoDto { IdPerfilEstudianteEmisor = PerfilEmisorId, IdPerfilEstudianteRecibe = PerfilReceptorId, Kudo = new TipoKudoDto { Id = 5 } };
            var res = await _casoUso.EjecutarAsync(EmisorId, dto);

            Assert.True(res.EsExitoso);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}


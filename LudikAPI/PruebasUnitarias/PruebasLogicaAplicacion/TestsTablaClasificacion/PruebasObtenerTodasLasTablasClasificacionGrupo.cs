using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion;
using LogicaAplicacion.InterfacesCasosUsos.TablaClasificacion;
using Entidades = LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Microsoft.Extensions.Azure;
using Moq;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsTablaClasificacion;

public class PruebasObtenerTodasLasTablasClasificacionGrupo
{
    public class ObtenerTodasLasTablasClasificacionGrupoTests
    {
        private readonly Mock<IRepositorioGrupos> _mockRepositorioGrupos;
        private readonly Mock<IRepositorioTablasClasificacion> _mockRepositorioTablasClasificacion;
        private readonly IObtenerTodasLasTablasClasificacionGrupo _casoUso;

        public ObtenerTodasLasTablasClasificacionGrupoTests()
        {
            _mockRepositorioGrupos = new Mock<IRepositorioGrupos>();
            _mockRepositorioTablasClasificacion = new Mock<IRepositorioTablasClasificacion>();
            _casoUso = new ObtenerTodasLasTablasClasificacionGrupo(
                _mockRepositorioGrupos.Object,
                _mockRepositorioTablasClasificacion.Object
            );
        }

        //==================================
        // PRUEBAS DE ERROR
        //==================================

        [Fact]
        public async Task EjecutarAsync_GrupoNoExistente_DebeRetornarErrorNotFound()
        {
            // Arrange
            var grupoIdInexistente = 999;
            var usuarioId = "usuario-valido";
            _mockRepositorioGrupos
                .Setup(repo => repo.GetByIdAsync(grupoIdInexistente))
                .ReturnsAsync(Resultado<Entidades.Grupo>.Falla(Error.NotFound));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoIdInexistente, usuarioId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(Error.NotFound.Codigo, resultado.Errores.First().Codigo);
            _mockRepositorioTablasClasificacion.Verify(repo => repo.GetAllByAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_UsuarioNoEsMiembroDelGrupo_DebeRetornarErrorForbidden()
        {
            // Arrange
            var grupoId = 1;
            var profesorId = "profesor-1";
            var usuarioNoAutorizadoId = "usuario-externo";
            var grupo = new Entidades.Grupo { Id = grupoId, ProfesorId = profesorId, Alumnos = new List<Entidades.PerfilEstudiante>() };

            _mockRepositorioGrupos
                .Setup(repo => repo.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidades.Grupo>.Exitoso(grupo));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId, usuarioNoAutorizadoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(Error.Forbidden.Codigo, resultado.Errores.First().Codigo);
            _mockRepositorioTablasClasificacion.Verify(repo => repo.GetAllByAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorAlObtenerTablas_DebePropagarErrorDelRepositorio()
        {
            // Arrange
            var grupoId = 1;
            var profesorId = "profesor-1";
            var grupo = new Entidades.Grupo { Id = grupoId, ProfesorId = profesorId, Alumnos = new List<Entidades.PerfilEstudiante>() };
            var errorDB = new Error("Error.Unexpected", "Fallo en la base de datos");

            _mockRepositorioGrupos
                .Setup(repo => repo.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidades.Grupo>.Exitoso(grupo));

            _mockRepositorioTablasClasificacion
                .Setup(repo => repo.GetAllByAsync(grupoId))
                .ReturnsAsync(Resultado<IEnumerable<Entidades.TablaClasificacion>>.Falla(errorDB));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId, profesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(errorDB.Codigo, resultado.Errores.First().Codigo);
        }

        //==================================
        // PRUEBAS DE ÉXITO
        //==================================

        [Fact]
        public async Task EjecutarAsync_UsuarioEsProfesorYExistenRankings_DebeRetornarListaDeDtos()
        {
            // Arrange
            var grupoId = 1;
            var profesorId = "profesor-1";
            var grupo = new Entidades.Grupo { Id = grupoId, ProfesorId = profesorId, Alumnos = new List<Entidades.PerfilEstudiante>() };
            var tablas = new List<Entidades.TablaClasificacion>
            {
                new Entidades.TablaClasificacion { Id = 10, Nombre = "Ranking 1", MedallaAsociadaId = 1, MedallaAsociada = new Entidades.Medalla { Id = 1, Nombre = "Medalla Oro" } },
                new Entidades.TablaClasificacion { Id = 11, Nombre = "Ranking 2", MedallaAsociadaId = 2, MedallaAsociada = new Entidades.Medalla { Id = 2, Nombre = "Medalla Plata" } }
            };

            _mockRepositorioGrupos
                .Setup(repo => repo.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidades.Grupo>.Exitoso(grupo));

            _mockRepositorioTablasClasificacion
                .Setup(repo => repo.GetAllByAsync(grupoId))
                .ReturnsAsync(Resultado<IEnumerable<Entidades.TablaClasificacion>>.Exitoso(tablas));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId, profesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Equal(2, resultado.Valor.Count());
            Assert.Equal("Ranking 1", resultado.Valor.First().Nombre);
        }

        [Fact]
        public async Task EjecutarAsync_UsuarioEsAlumnoYExistenRankings_DebeRetornarListaDeDtos()
        {
            // Arrange
            var grupoId = 1;
            var profesorId = "profesor-1";
            var alumnoId = "alumno-que-pertenece"; 
            var alumnoEnGrupo = new Entidades.PerfilEstudiante { EstudianteId = alumnoId, GrupoId = grupoId };

            // Inicializamos el grupo correctamente
            var grupo = new Entidades.Grupo
            {
                Id = grupoId,
                ProfesorId = profesorId,
                Alumnos = new List<Entidades.PerfilEstudiante> { alumnoEnGrupo }
            };

            var tablas = new List<Entidades.TablaClasificacion>
            {
                new Entidades.TablaClasificacion {
                    Id = 10,
                    Nombre = "Ranking General",
                    MedallaAsociadaId = 1,
                    MedallaAsociada = new Entidades.Medalla{ Id = 1, Nombre="Medalla Default"},
                }
            };

            _mockRepositorioGrupos
                .Setup(repo => repo.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidades.Grupo>.Exitoso(grupo));

            _mockRepositorioTablasClasificacion
                .Setup(repo => repo.GetAllByAsync(grupoId))
                .ReturnsAsync(Resultado<IEnumerable<Entidades.TablaClasificacion>>.Exitoso(tablas));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId, alumnoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Single(resultado.Valor);
        }

        [Fact]
        public async Task EjecutarAsync_GrupoNoTieneRankings_DebeRetornarListaVaciaExitosamente()
        {
            // Arrange
            var grupoId = 1;
            var profesorId = "profesor-1";
            var grupo = new Entidades.Grupo { Id = grupoId, ProfesorId = profesorId, Alumnos = new List<Entidades.PerfilEstudiante>() };

            _mockRepositorioGrupos
                .Setup(repo => repo.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidades.Grupo>.Exitoso(grupo));

            _mockRepositorioTablasClasificacion
                .Setup(repo => repo.GetAllByAsync(grupoId))
                .ReturnsAsync(Resultado<IEnumerable<Entidades.TablaClasificacion>>.Exitoso(new List<Entidades.TablaClasificacion>()));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId, profesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Empty(resultado.Valor);
        }
    }
}
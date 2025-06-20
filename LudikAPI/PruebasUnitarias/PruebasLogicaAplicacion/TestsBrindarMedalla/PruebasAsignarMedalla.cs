using LogicaNegocio.Resultados;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad = LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.AsignarMedalla;
using LogicaNegocio.InterfacesRepositorios;

namespace PruebasUnitarias.PruebasLogicaAplicacion.BrindarMedalla
{
    public class PruebasAsignarMedalla
    {
        private readonly Mock<IRepositorioProfesores> _mockRepoProfesores;
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockRepoPerfilEstudiantes;
        private readonly Mock<IRepositorioMedallas> _mockRepoMedallas;
        private readonly Mock<IRepositorioPerfilEstudianteMedalla> _mockRepoPerfilEstudianteMedalla;
        private readonly AsignarMedalla _asignarMedallaUseCase;

        public PruebasAsignarMedalla()
        {
            _mockRepoProfesores = new Mock<IRepositorioProfesores>();
            _mockRepoPerfilEstudiantes = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _mockRepoMedallas = new Mock<IRepositorioMedallas>();
            _mockRepoPerfilEstudianteMedalla = new Mock<IRepositorioPerfilEstudianteMedalla>();

            _asignarMedallaUseCase = new AsignarMedalla(
                _mockRepoPerfilEstudiantes.Object,
                _mockRepoMedallas.Object,
                _mockRepoProfesores.Object,
                _mockRepoPerfilEstudianteMedalla.Object
            );
        }

        [Fact]
        public async Task EjecutarAsync_ProfesorNoExiste_DebeRetornarFalloNotFound()
        {
            // Arrange
            var profesorId = "id-inexistente";
            _mockRepoProfesores.Setup(r => r.GetByStringIdAsync(profesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Falla(Error.NotFound));

            // Act
            var resultado = await _asignarMedallaUseCase.EjecutarAsync(profesorId, 1, 1);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(Error.NotFound.Codigo, resultado.Errores.First().Codigo);
        }

        [Fact]
        public async Task EjecutarAsync_PerfilEstudianteNoExiste_DebeRetornarFalloNotFound()
        {
            // Arrange
            var profesorId = "profesor-123";
            var idPerfilEstudiante = 99; // ID inexistente
            var profesor = new Entidad.Profesor { Id = profesorId };

            _mockRepoProfesores.Setup(r => r.GetByStringIdAsync(profesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));

            _mockRepoPerfilEstudiantes.Setup(r => r.GetByIdAsync(idPerfilEstudiante))
                .ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Falla(Error.NotFound));

            // Act
            var resultado = await _asignarMedallaUseCase.EjecutarAsync(profesorId, idPerfilEstudiante, 1);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(Error.NotFound.Codigo, resultado.Errores.First().Codigo);
        }

        [Fact]
        public async Task EjecutarAsync_MedallaNoExiste_DebeRetornarFalloNotFound()
        {
            // Arrange
            var profesorId = "profesor-123";
            var idPerfilEstudiante = 1;
            var idMedalla = 99; // ID inexistente
            var profesor = new Entidad.Profesor { Id = profesorId };
            var perfilEstudiante = new Entidad.PerfilEstudiante { Id = idPerfilEstudiante, GrupoId = 10 };

            _mockRepoProfesores.Setup(r => r.GetByStringIdAsync(profesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));

            _mockRepoPerfilEstudiantes.Setup(r => r.GetByIdAsync(idPerfilEstudiante))
                .ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(perfilEstudiante));

            _mockRepoMedallas.Setup(r => r.GetByIdAsync(idMedalla))
                .ReturnsAsync(Resultado<Entidad.Medalla>.Falla(Error.NotFound));

            // Act
            var resultado = await _asignarMedallaUseCase.EjecutarAsync(profesorId, idPerfilEstudiante, idMedalla);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(Error.NotFound.Codigo, resultado.Errores.First().Codigo);
        }

        [Fact]
        public async Task EjecutarAsync_ProfesorNoPerteneceAlGrupoDelEstudiante_DebeRetornarFalloForbidden()
        {
            // Arrange
            var profesorId = "profesor-123";
            var idPerfilEstudiante = 1;
            var idMedalla = 1;
            var idGrupoEstudiante = 10;
            var idGrupoProfesor = 20; // Grupo diferente

            var profesor = new Entidad.Profesor
                { Id = profesorId, Grupos = new List<Entidad.Grupo> { new Entidad.Grupo { Id = idGrupoProfesor } } };
            var perfilEstudiante = new Entidad.PerfilEstudiante { Id = idPerfilEstudiante, GrupoId = idGrupoEstudiante };
            var medalla = new Entidad.Medalla { Id = idMedalla };

            _mockRepoProfesores.Setup(r => r.GetByStringIdAsync(profesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));
            _mockRepoPerfilEstudiantes.Setup(r => r.GetByIdAsync(idPerfilEstudiante))
                .ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(perfilEstudiante));
            _mockRepoMedallas.Setup(r => r.GetByIdAsync(idMedalla)).ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(medalla));

            // Act
            var resultado = await _asignarMedallaUseCase.EjecutarAsync(profesorId, idPerfilEstudiante, idMedalla);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(Error.Forbidden.Codigo, resultado.Errores.First().Codigo);
        }

        [Fact]
        public async Task EjecutarAsync_DatosValidosYProfesorPerteneceAlGrupo_DebeRetornarExitoso()
        {
            // Arrange
            var profesorId = "profesor-123";
            var idPerfilEstudiante = 1;
            var idMedalla = 1;
            var idGrupo = 10; // Mismo grupo

            var profesor = new Entidad.Profesor { Id = profesorId, Grupos = new List<Entidad.Grupo> { new Entidad.Grupo { Id = idGrupo } } };
            var perfilEstudiante = new Entidad.PerfilEstudiante { Id = idPerfilEstudiante, GrupoId = idGrupo };
            var medalla = new Entidad.Medalla { Id = idMedalla };

            _mockRepoProfesores.Setup(r => r.GetByStringIdAsync(profesorId))
                .ReturnsAsync(Resultado<Entidad.Profesor>.Exitoso(profesor));
            _mockRepoPerfilEstudiantes.Setup(r => r.GetByIdAsync(idPerfilEstudiante))
                .ReturnsAsync(Resultado<Entidad.PerfilEstudiante>.Exitoso(perfilEstudiante));
            _mockRepoMedallas.Setup(r => r.GetByIdAsync(idMedalla)).ReturnsAsync(Resultado<Entidad.Medalla>.Exitoso(medalla));

            // Importante: Simular que el guardado en el repositorio de la tabla intermedia es exitoso
            _mockRepoPerfilEstudianteMedalla.Setup(r => r.AddAsync(It.IsAny<Entidad.PerfilEstudianteMedalla>()))
                .ReturnsAsync(Resultado.Exitoso());

            // Act
            var resultado = await _asignarMedallaUseCase.EjecutarAsync(profesorId, idPerfilEstudiante, idMedalla);

            // Assert
            Assert.True(resultado.EsExitoso);
            // Opcional: Verificar que el método AddAsync fue llamado una vez
            _mockRepoPerfilEstudianteMedalla.Verify(r => r.AddAsync(It.Is<Entidad.PerfilEstudianteMedalla>(pem =>
                pem.PerfilEstudianteId == idPerfilEstudiante && pem.MedallaId == idMedalla
            )), Times.Once);
        }
    }
}
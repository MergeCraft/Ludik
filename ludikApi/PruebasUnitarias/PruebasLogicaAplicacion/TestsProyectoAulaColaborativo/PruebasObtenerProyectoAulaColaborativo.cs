using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.ProyectoAulaColaborativoDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.ProyectoAulaColaborativo;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsProyectoAulaColaborativo
{
    public class PruebasObtenerProyectoAulaColaborativo
    {
        private readonly Mock<IRepositorioProyectoAulaColaborativo> _repoPacMock;
        private readonly Mock<IRepositorioGrupos> _repoGruposMock;
        private readonly ObtenerProyectoAulaColaborativo _casoUso;
        private const int GrupoId = 99;

        public PruebasObtenerProyectoAulaColaborativo()
        {
            _repoPacMock = new Mock<IRepositorioProyectoAulaColaborativo>();
            _repoGruposMock = new Mock<IRepositorioGrupos>();
            _casoUso = new ObtenerProyectoAulaColaborativo(_repoPacMock.Object, _repoGruposMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_RepoPacFalla_RetornaFalloConErrores()
        {
            // Arrange
            var errores = new List<Error> { new Error("RepoError", "Error en repositorio PAC") };
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                // El mock del repositorio debe seguir devolviendo una lista, que es lo que el método espera.
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Falla(errores));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(errores, resultado.Errores);
        }

        [Fact]
        public async Task EjecutarAsync_NoEncuentraGrupo_RetornaFallo()
        {
            // Arrange
            // El repo de PAC devuelve un PAC para poder llegar al paso de buscar el grupo.
            var pac = new ProyectoAulaColaborativo { Estado = EstadoPAC.Activo };
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo> { pac }));

            var errores = new List<Error> { Error.NotFound };
            _repoGruposMock
                .Setup(r => r.GetByIdAsync(GrupoId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Grupo>.Falla(errores));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(errores, resultado.Errores);
        }

        [Fact]
        public async Task EjecutarAsync_SinProyectoActivo_RetornaFalloNotFound()
        {
            // Arrange: Simular que el repo no devuelve PACs o solo inactivos.
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo>()));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(Error.NotFound, resultado.Errores);
        }

        [Fact]
        public async Task EjecutarAsync_ConProyectoActivo_RetornaDtoConContribuciones()
        {
            // Arrange
            var fechaInicio = DateTime.UtcNow.AddDays(-10);
            var fechaFin = DateTime.UtcNow.AddDays(10);
            var pac = new ProyectoAulaColaborativo
            {
                Id = 1,
                GrupoId = GrupoId,
                Nombre = "PAC 1",
                CantidadMedallasNecesarias = 100,
                RecompensaClaseId = 7,
                Estado = EstadoPAC.Activo,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo> { pac }));

            // Mockear el grupo y la llamada a ContarMedallasEnPeriodo para aislar la prueba.
            const int medallasContadas = 42;
            var mockGrupo = new Mock<LogicaNegocio.Entidades.Grupo>();
            mockGrupo.Setup(g => g.ContarMedallasEnPeriodo(fechaInicio, fechaFin)).Returns(medallasContadas);

            _repoGruposMock
                .Setup(r => r.GetByIdAsync(GrupoId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Grupo>.Exitoso(mockGrupo.Object));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            var dto = resultado.Valor;
            Assert.NotNull(dto);

            Assert.Equal(pac.Id, dto.Id);
            Assert.Equal(pac.Nombre, dto.Nombre);
            Assert.Equal(pac.GrupoId, dto.GrupoId);
            Assert.Equal(pac.CantidadMedallasNecesarias, dto.CantidadMedallasNecesarias);
            Assert.Equal(pac.RecompensaClaseId, dto.RecompensaClaseId);
            Assert.Equal(pac.Estado, dto.Estado);

            // verificar que las contribuciones se calcularon y asignaron.
            Assert.Equal(medallasContadas, dto.TotalContribuciones);

            // Verificar que el método ContarMedallasEnPeriodo fue llamado con las fechas correctas.
            mockGrupo.Verify(g => g.ContarMedallasEnPeriodo(fechaInicio, fechaFin), Times.Once);
        }
    }
}
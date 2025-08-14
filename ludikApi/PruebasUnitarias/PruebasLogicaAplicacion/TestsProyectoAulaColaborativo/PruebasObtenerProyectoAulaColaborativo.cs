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
        private const string ProfesorId = "profX";

        public PruebasObtenerProyectoAulaColaborativo()
        {
            _repoPacMock = new Mock<IRepositorioProyectoAulaColaborativo>();
            _repoGruposMock = new Mock<IRepositorioGrupos>();
            _casoUso = new ObtenerProyectoAulaColaborativo(_repoPacMock.Object, _repoGruposMock.Object);

            // por defecto: ObtenerGruposPorProfesorId devuelve el grupo para que la validación pase
            _repoGruposMock
                .Setup(r => r.ObtenerGruposPorProfesorId(It.IsAny<string>()))
                .ReturnsAsync(new List<LogicaNegocio.Entidades.Grupo> { new LogicaNegocio.Entidades.Grupo { Id = GrupoId } });
        }

        [Fact]
        public async Task EjecutarAsync_RepoFalla_RetornaFalloConErrores()
        {
            // Arrange
            var errores = new List<Error> { new Error("RepoError", "Error en repositorio") };
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Falla(errores));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId, ProfesorId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal(errores, resultado.Errores);
        }

        [Fact]
        public async Task EjecutarAsync_SinProyecto_RetornaExitosoConListaVacia()
        {
            // Arrange: simular que no hay PAC
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo>()));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId, ProfesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Empty(resultado.Valor);
        }

        [Fact]
        public async Task EjecutarAsync_ConProyecto_RetornaDtoMapeado()
        {
            // Arrange: un solo PAC
            var pac = new ProyectoAulaColaborativo
            {
                Id = 1,
                GrupoId = GrupoId,
                Nombre = "PAC 1",
                Visual = MetaVisual.Piramide,
                CantidadMedallasNecesarias = 2,
                TotalContribuciones = 5,
                RecompensaClaseId = 7,
                Estado = EstadoPAC.Activo
            };

            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo> { pac }));

            // Act
            var resultado = await _casoUso.EjecutarAsync(GrupoId, ProfesorId);

            // Assert
            Assert.True(resultado.EsExitoso);
            var dtos = resultado.Valor!.ToList();
            Assert.Single(dtos);

            var dto = dtos[0];
            Assert.Equal(pac.Id, dto.Id);
            Assert.Equal(pac.Nombre, dto.Nombre);
            Assert.Equal(pac.GrupoId, dto.GrupoId);
            Assert.Equal(pac.Visual, dto.Visual);
            Assert.Equal(pac.CantidadMedallasNecesarias, dto.CantidadMedallasNecesarias);
            Assert.Equal(pac.TotalContribuciones, dto.TotalContribuciones);
            Assert.Equal(pac.RecompensaClaseId, dto.RecompensaClaseId);
            Assert.Equal(pac.Estado, dto.Estado);
        }

        
    }
}
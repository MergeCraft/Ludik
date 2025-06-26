using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Grupos;
using LogicaNegocio.Resultados;
using Entidad = LogicaNegocio.Entidades;


namespace PruebasUnitarias.PruebasLogicaAplicacion.Grupo
{
    public class PruebasObtenerInformacionGrupo
    {
        private readonly Mock<IRepositorioGrupos> _repoGruposMock;

        public PruebasObtenerInformacionGrupo()
        {
            _repoGruposMock = new Mock<IRepositorioGrupos>();
        }

        [Fact]
        public async Task EjecutarAsync_GrupoNoExiste_RetornaFallo()
        {
            // Arrange
            int grupoId = 1;
            var errores = new[] { new Error("Error.NotFound", "No existe el grupo.") };
            _repoGruposMock
                .Setup(r => r.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Falla(errores));

            var casoUso = new ObtenerInformacionGrupo(_repoGruposMock.Object);

            // Act
            var resultado = await casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.NotNull(resultado.Errores);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("No existe el grupo."));
        }

        [Fact]
        public async Task EjecutarAsync_GrupoExisteConEnlace_RetornaDtoMapeado()
        {
            // Arrange
            int grupoId = 2;
            var fechaCreacion = new DateTime(2025, 1, 15, 10, 30, 0);
            var tablaEq = new TablaEquivalencia { Id = 5 };
            var enlace = new EnlaceUnion("http://url.com/inv", "codigo123")
            {
                UrlCompleta = "http://url.com/inv"
            };
            var grupo = new Entidad.Grupo
            {
                Id = grupoId,
                Nombre = "Grupo Test",
                TablaEquivalencia = tablaEq,
                ProfesorId = "profX",
                Institucion = "InstX",
                Materia = "Matemáticas",
                FCreacion = fechaCreacion,
                EnlaceUnion = enlace,
                Tienda = new Entidad.Tienda { Id = 99 }
            };
            _repoGruposMock
                .Setup(r => r.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso(grupo));

            var casoUso = new ObtenerInformacionGrupo(_repoGruposMock.Object);

            // Act
            var resultado = await casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            var dto = resultado.Valor!;
            Assert.Equal("Grupo Test", dto.Nombre);
            Assert.Equal(5, dto.TablaEquivalenciaId);
            Assert.Equal("profX", dto.ProfesorId);
            Assert.Equal("InstX", dto.Institucion);
            Assert.Equal("Matemáticas", dto.Materia);
            Assert.Equal(fechaCreacion, dto.fCreacion);
            Assert.Equal("http://url.com/inv", dto.UrlCompleta);
            Assert.Equal(99, dto.IdTienda);
        }

        [Fact]
        public async Task EjecutarAsync_GrupoExisteSinEnlace_RetornaDtoConUrlCompletaNull()
        {
            // Arrange
            int grupoId = 3;
            var fechaCreacion = DateTime.UtcNow;
            var tablaEq = new TablaEquivalencia { Id = 7 };
            var grupo = new Entidad.Grupo
            {
                Id = grupoId,
                Nombre = "Otro Grupo",
                TablaEquivalencia = tablaEq,
                ProfesorId = "profY",
                Institucion = null,
                Materia = null,
                FCreacion = fechaCreacion,
                EnlaceUnion = null,
                Tienda = new Entidad.Tienda { Id = 0 }
            };
            _repoGruposMock
                .Setup(r => r.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso(grupo));

            var casoUso = new ObtenerInformacionGrupo(_repoGruposMock.Object);

            // Act
            var resultado = await casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            var dto = resultado.Valor!;
            Assert.Equal("Otro Grupo", dto.Nombre);
            Assert.Equal(7, dto.TablaEquivalenciaId);
            Assert.Equal("profY", dto.ProfesorId);
            Assert.Null(dto.Institucion);
            Assert.Null(dto.Materia);
            Assert.Equal(fechaCreacion, dto.fCreacion);
            Assert.Null(dto.UrlCompleta);
            Assert.Equal(0, dto.IdTienda);
        }
    }
}

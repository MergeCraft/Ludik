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
    public class PruebasEditarGrupo
    {
        private readonly Mock<IRepositorioGrupos> _repoGruposMock;
        private readonly Mock<IRepositorioTablasEquivalencia> _repoTablasMock;

        public PruebasEditarGrupo()
        {
            _repoGruposMock = new Mock<IRepositorioGrupos>();
            _repoTablasMock = new Mock<IRepositorioTablasEquivalencia>();
        }

        [Fact]
        public async Task Ejecutar_DtoNulo_RetornaErrorValidacion()
        {
            // Arrange
            var servicio = new EditarGrupo(_repoGruposMock.Object, _repoTablasMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(null!, "profesor123");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("No hay informacion"));
        }

        [Fact]
        public async Task Ejecutar_GrupoNoExiste_RetornaErrorNotFound()
        {
            // Arrange
            var dto = new GrupoEditarDto
            {
                Id = 1,
                Nombre = "Nuevo nombre",
                TablaEquivalenciaId = 5, // aunque no se use, asignamos un valor
                ProfesorId = "profesor123"
            };

            // Simulamos que GetByIdAsync retorna Resultado con Valor null
            _repoGruposMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso((Entidad.Grupo)null!));

            var servicio = new EditarGrupo(_repoGruposMock.Object, _repoTablasMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(dto, "profesor123");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("No se encontró el grupo"));
        }

        [Fact]
        public async Task Ejecutar_ProfesorNoAutorizado_LanzaUnauthorizedAccessException()
        {
            // Arrange
            var dto = new GrupoEditarDto
            {
                Id = 1,
                Nombre = "Nuevo nombre",
                TablaEquivalenciaId = 5,
                ProfesorId = "otroProfesor"
            };

            var grupo = new Entidad.Grupo
            {
                Id = 1,
                ProfesorId = "otroProfesor",
                Nombre = "Antiguo",
                Institucion = "X",
                Materia = "Y",
                TablaEquivalencia = new TablaEquivalencia() // dummy, no importa aquí
            };

            _repoGruposMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso(grupo));

            var servicio = new EditarGrupo(_repoGruposMock.Object, _repoTablasMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                servicio.EjecutarAsync(dto, "profesor123"));
        }

        [Fact]
        public async Task Ejecutar_TablaEquivalenciaNoValida_RetornaErrorValidacion()
        {
            // Arrange
            int grupoId = 2;
            int tablaId = 0; // inválido
            var dto = new GrupoEditarDto
            {
                Id = grupoId,
                Nombre = "Nombre",
                TablaEquivalenciaId = tablaId,
                ProfesorId = "profesor123"
            };

            var grupo = new Entidad.Grupo
            {
                Id = grupoId,
                ProfesorId = "profesor123",
                Nombre = "Antiguo",
                Institucion = "X",
                Materia = "Y",
                TablaEquivalencia = new TablaEquivalencia()
            };

            _repoGruposMock.Setup(r => r.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso(grupo));

            var servicio = new EditarGrupo(_repoGruposMock.Object, _repoTablasMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(dto, "profesor123");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("Debe seleccionar una tabla de equivalencia válida"));
        }

        [Fact]
        public async Task Ejecutar_TablaEquivalenciaNoExiste_RetornaErrorValidacion()
        {
            // Arrange
            int grupoId = 3;
            int tablaId = 99;
            var dto = new GrupoEditarDto
            {
                Id = grupoId,
                Nombre = "Nombre",
                TablaEquivalenciaId = tablaId,
                ProfesorId = "profesor123"
            };

            var grupo = new Entidad.Grupo
            {
                Id = grupoId,
                ProfesorId = "profesor123",
                Nombre = "Antiguo",
                Institucion = "X",
                Materia = "Y",
                TablaEquivalencia = new TablaEquivalencia()
            };

            _repoGruposMock.Setup(r => r.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso(grupo));

            // Simulamos que GetByIdAsync de tabla devuelve fallo o Valor null
            _repoTablasMock.Setup(r => r.GetByIdAsync(tablaId))
                .ReturnsAsync(Resultado<TablaEquivalencia>.Falla(new Error("Error.Validation", "No existe")));

            var servicio = new EditarGrupo(_repoGruposMock.Object, _repoTablasMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(dto, "profesor123");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("no existe"));
        }

        [Fact]
        public async Task Ejecutar_DatosValidos_ActualizaYRetornaExitoso()
        {
            // Arrange
            int grupoId = 4;
            int tablaId = 10;
            var dto = new GrupoEditarDto
            {
                Id = grupoId,
                Nombre = "NuevoNombre",
                TablaEquivalenciaId = tablaId,
                ProfesorId = "profesor123",
                Institucion = "Inst",
                Materia = "Mat"
            };

            var grupoExistente = new Entidad.Grupo
            {
                Id = grupoId,
                ProfesorId = "profesor123",
                Nombre = "Antiguo",
                Institucion = "X",
                Materia = "Y",
                TablaEquivalencia = new TablaEquivalencia(),
                Tienda = new Tienda()
            };

            _repoGruposMock.Setup(r => r.GetByIdAsync(grupoId))
                .ReturnsAsync(Resultado<Entidad.Grupo>.Exitoso(grupoExistente));

            // Simulamos tabla válida
            var tablaEq = new TablaEquivalencia();
            _repoTablasMock.Setup(r => r.GetByIdAsync(tablaId))
                .ReturnsAsync(Resultado<TablaEquivalencia>.Exitoso(tablaEq));

            // Simulamos UpdateAsync
            _repoGruposMock.Setup(r => r.UpdateAsync(It.IsAny<Entidad.Grupo>()))
                .ReturnsAsync(Resultado.Exitoso());

            var servicio = new EditarGrupo(_repoGruposMock.Object, _repoTablasMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(dto, "profesor123");

            // Assert
            Assert.True(resultado.EsExitoso);
            // Verificamos que se actualizó el grupo con los nuevos valores
            _repoGruposMock.Verify(r => r.UpdateAsync(It.Is<Entidad.Grupo>(g =>
                g.Id == grupoId
                && g.Nombre == "NuevoNombre"
                && g.TablaEquivalencia.Id == tablaEq.Id   // si TablaEquivalencia tiene Id; de lo contrario solo comparas referencia
            )), Times.Once);
        }
    }
}
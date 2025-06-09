using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.GrupoDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Grupos;
using LogicaNegocio.Resultados;

namespace PruebasUnitarias.PruebasLogicaAplicacion
{
    public class PruebasEditarGrupo
    {
        private readonly Mock<IRepositorioGrupos> _repoGruposMock;

        public PruebasEditarGrupo()
        {
            _repoGruposMock = new Mock<IRepositorioGrupos>();
        }

        [Fact]
        public async Task Ejecutar_DtoNulo_RetornaErrorValidacion()
        {
            // Arrange
            var servicio = new EditarGrupo(_repoGruposMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(null!, "profesor123");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains("informacion", resultado.Errores[0].Mensaje);
        }

        [Fact]
        public async Task Ejecutar_GrupoNoExiste_RetornaErrorNotFound()
        {
            // Arrange
            var dto = new GrupoEditarDto
            {
                Id = 1,
                Nombre = "Nuevo nombre"
            };

            _repoGruposMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(Resultado<Grupo>.Exitoso(null!));

            var servicio = new EditarGrupo(_repoGruposMock.Object);

            // Act
            var resultado = await servicio.EjecutarAsync(dto, "profesor123");

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Contains("No se encontró el grupo", resultado.Errores[0].Mensaje);
        }

        [Fact]
        public async Task Ejecutar_ProfesorNoAutorizado_LanzaExcepcion()
        {
            // Arrange
            var dto = new GrupoEditarDto
            {
                Id = 1,
                Nombre = "Nuevo nombre"
            };

            var grupo = new Grupo
            {
                Id = 1,
                ProfesorId = "otroProfesor"
            };

            _repoGruposMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(Resultado<Grupo>.Exitoso(grupo));

            var servicio = new EditarGrupo(_repoGruposMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await servicio.EjecutarAsync(dto, "profesor123"));
        }
        


    }
}
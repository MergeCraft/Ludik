using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaAplicacion.ImplementacionCasosUsos.PerfilEstudiante;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.PerfilEstudiante
{
    public class PruebasObtenerPerfilesPorGrupo
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _repoPerfilMock;

        public PruebasObtenerPerfilesPorGrupo()
        {
            _repoPerfilMock = new Mock<IRepositorioPerfilEstudianteGrupo>();
        }

        [Fact]
        public async Task EjecutarAsync_PerfilesEncontrados_RetornaDtosMapeados()
        {
            // Arrange
            int grupoId = 42;
            var perfiles = new List<Dominio.PerfilEstudiante>
            {
                new Dominio.PerfilEstudiante
                {
                    Id = 1,
                    MetaCalificacion = 5,
                    EstudianteId = "e1",
                    Monedas = 100,
                    GrupoId = grupoId
                },
                new Dominio.PerfilEstudiante
                {
                    Id = 2,
                    MetaCalificacion = 8,
                    EstudianteId = "e2",
                    Monedas = 200,
                    GrupoId = grupoId
                }
            };

            // Aquí: asegurarnos de usar PerfilEstudiante y Resultado<List<PerfilEstudiante>>
            _repoPerfilMock
                .Setup(r => r.ObtenerPorGrupoIdAsync(grupoId))
                .ReturnsAsync(Resultado<List<Dominio.PerfilEstudiante>>.Exitoso(perfiles));

            var casoUso = new ObtenerPerfilesPorGrupo(_repoPerfilMock.Object);

            // Act
            var resultado = await casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Equal(2, resultado.Valor.Count);

            // Verificar mapeo de cada propiedad en el DTO
            var dto1 = resultado.Valor[0];
            Assert.Equal(1, dto1.Id);
            Assert.Equal(10, dto1.AvatarGrupoId);
            Assert.Equal("http://avatar1", dto1.EnlaceAvatarCompleto);
            Assert.Equal(5, dto1.MetaCalificacion);
            Assert.Equal("e1", dto1.EstudianteId);
            Assert.Equal(100, dto1.Monedas);
            Assert.Equal(grupoId, dto1.GrupoId);

            var dto2 = resultado.Valor[1];
            Assert.Equal(2, dto2.Id);
            Assert.Equal(20, dto2.AvatarGrupoId);
            Assert.Equal("http://avatar2", dto2.EnlaceAvatarCompleto);
            Assert.Equal(8, dto2.MetaCalificacion);
            Assert.Equal("e2", dto2.EstudianteId);
            Assert.Equal(200, dto2.Monedas);
            Assert.Equal(grupoId, dto2.GrupoId);
        }

        [Fact]
        public async Task EjecutarAsync_ListaVacia_RetornaListaVacia()
        {
            // Arrange
            int grupoId = 100;
            var perfilesVacios = new List<Dominio.PerfilEstudiante>();

            _repoPerfilMock
                .Setup(r => r.ObtenerPorGrupoIdAsync(grupoId))
                .ReturnsAsync(Resultado<List<Dominio.PerfilEstudiante>>.Exitoso(perfilesVacios));

            var casoUso = new ObtenerPerfilesPorGrupo(_repoPerfilMock.Object);

            // Act
            var resultado = await casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Empty(resultado.Valor);
        }

        [Fact]
        public async Task EjecutarAsync_ErrorEnRepositorio_RetornaFalloConMismoMensaje()
        {
            // Arrange
            int grupoId = 5;
            var erroresRepo = new List<Error>
            {
                new Error("RepoError", "Fallo al obtener perfiles del grupo")
            };

            _repoPerfilMock
                .Setup(r => r.ObtenerPorGrupoIdAsync(grupoId))
                .ReturnsAsync(Resultado<List<Dominio.PerfilEstudiante>>.Falla(erroresRepo));

            var casoUso = new ObtenerPerfilesPorGrupo(_repoPerfilMock.Object);

            // Act
            var resultado = await casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.NotNull(resultado.Errores);
            Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("Fallo al obtener perfiles del grupo"));
        }
    }
}
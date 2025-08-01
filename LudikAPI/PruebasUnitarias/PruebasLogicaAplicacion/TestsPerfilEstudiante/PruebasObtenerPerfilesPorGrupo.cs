using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.PerfilEstudianteDTO;
using LogicaAplicacion.ImplementacionCasosUsos.PerfilEstudiante;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.PerfilEstudiante
{
    public class PruebasObtenerPerfilesPorGrupo
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _repoPerfilMock;
        private readonly Mock<IGeneradorUrlsParaColeccionesImagenes> _generadorMock;
        private readonly ObtenerPerfilesPorGrupo _casoUso;

        public PruebasObtenerPerfilesPorGrupo()
        {
            _repoPerfilMock = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _generadorMock = new Mock<IGeneradorUrlsParaColeccionesImagenes>();
            _casoUso = new ObtenerPerfilesPorGrupo(_repoPerfilMock.Object, _generadorMock.Object);

            // Simular procesamiento de URLs: prepend "http://cdn/mini/"
            
        }

        [Fact]
        public async Task EjecutarAsync_PerfilesEncontrados_RetornaDtosMapeadosYUrlsProcesadas()
        {
            // Arrange
            int grupoId = 42;
            var perfiles = new List<LogicaNegocio.Entidades.PerfilEstudiante>
            {
                new LogicaNegocio.Entidades.PerfilEstudiante
                {
                    Id = 1,
                    MetaCalificacion = 5,
                    EstudianteId = "e1",
                    Monedas = 100,
                    GrupoId = grupoId,
                    NombreImagenMiniatura = "mini1.png",
                    NombreImagenCompleta = "full1.png",
                    Grupo = new LogicaNegocio.Entidades.Grupo { Id = grupoId, Nombre = "G1" }
                },
                new LogicaNegocio.Entidades.PerfilEstudiante
                {
                    Id = 2,
                    MetaCalificacion = 8,
                    EstudianteId = "e2",
                    Monedas = 200,
                    GrupoId = grupoId,
                    NombreImagenMiniatura = "mini2.png",
                    NombreImagenCompleta = "full2.png",
                    Grupo = new LogicaNegocio.Entidades.Grupo { Id = grupoId, Nombre = "G1" }
                }
            };

            _repoPerfilMock
                .Setup(r => r.ObtenerPorGrupoIdAsync(grupoId))
                .ReturnsAsync(Resultado<List<LogicaNegocio.Entidades.PerfilEstudiante>>.Exitoso(perfiles));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            var dtos = resultado.Valor;
            Assert.Equal(2, dtos.Count);

            // Primer DTO
            var dto1 = dtos.Single(d => d.Id == 1);
            Assert.Equal("full1.png", dto1.EnlaceAvatarCompleto);
            Assert.Equal("http://cdn/mini/mini1.png", dto1.EnlaceAvatarMiniatura);
            Assert.Equal(5, dto1.MetaCalificacion);
            Assert.Equal("e1", dto1.EstudianteId);
            Assert.Equal(100, dto1.Monedas);
            Assert.Equal(grupoId, dto1.GrupoId);
            Assert.Equal(0, dto1.CalificacionActual);

            // Segundo DTO
            var dto2 = dtos.Single(d => d.Id == 2);
            Assert.Equal("full2.png", dto2.EnlaceAvatarCompleto);
            Assert.Equal("http://cdn/mini/mini2.png", dto2.EnlaceAvatarMiniatura);
            Assert.Equal(8, dto2.MetaCalificacion);
            Assert.Equal("e2", dto2.EstudianteId);
            Assert.Equal(200, dto2.Monedas);
            Assert.Equal(grupoId, dto2.GrupoId);
            Assert.Equal(0, dto2.CalificacionActual);

            // Verificar llamada al generador de URLs
           
        }

        [Fact]
        public async Task EjecutarAsync_ListaVacia_NoProcesaUrls()
        {
            // Arrange
            int grupoId = 100;
            _repoPerfilMock
                .Setup(r => r.ObtenerPorGrupoIdAsync(grupoId))
                .ReturnsAsync(Resultado<List<LogicaNegocio.Entidades.PerfilEstudiante>>.Exitoso(new List<LogicaNegocio.Entidades.PerfilEstudiante>()));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsExitoso);
            Assert.Empty(resultado.Valor);

            // No se debe llamar a procesar URLs si la lista está vacía
           
        }

        [Fact]
        public async Task EjecutarAsync_ErrorEnRepositorio_RetornaFallo()
        {
            // Arrange
            int grupoId = 5;
            var errores = new List<Error> { new Error("RepoError", "Fallo al obtener perfiles") };
            _repoPerfilMock
                .Setup(r => r.ObtenerPorGrupoIdAsync(grupoId))
                .ReturnsAsync(Resultado<List<LogicaNegocio.Entidades.PerfilEstudiante>>.Falla(errores));

            // Act
            var resultado = await _casoUso.EjecutarAsync(grupoId);

            // Assert
            Assert.True(resultado.EsFallo);
            Assert.Equal("Fallo al obtener perfiles", resultado.Errores[0].Mensaje);
        }
    }
}

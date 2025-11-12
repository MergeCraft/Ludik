using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObjects;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsTablaClasificacion
{
    public class PruebasObtenerTablaClasificacion
    {
        private readonly Mock<IRepositorioTablasClasificacion> _repoMock;
        private readonly ObtenerTablaClasificacion _casoUso;
        private const int TablaId = 7;

        public PruebasObtenerTablaClasificacion()
        {
            _repoMock = new Mock<IRepositorioTablasClasificacion>();
            _casoUso = new ObtenerTablaClasificacion(_repoMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_RepoFalla_RetornaFallo()
        {
            var error = new Error("Error.DB", "Error repositorio");
            _repoMock.Setup(r => r.GetByIdAsync(TablaId))
                     .ReturnsAsync(Resultado<TablaClasificacion>.Falla(error));

            var resultado = await _casoUso.EjecutarAsync(TablaId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == error.Codigo && e.Mensaje == error.Mensaje);
            _repoMock.Verify(r => r.GetByIdAsync(TablaId), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_TablaNull_RetornaFalloNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(TablaId))
                     .ReturnsAsync(Resultado<TablaClasificacion>.Exitoso((TablaClasificacion)null));

            var resultado = await _casoUso.EjecutarAsync(TablaId);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "TablaClasificacion.NoEncontrada");
        }

        [Fact]
        public async Task EjecutarAsync_Valido_RetornaDtoConParticipantesOrdenadosYNombreCompleto()
        {
            var medalla = new LogicaNegocio.Entidades.Medalla { Id = 100, Nombre = "M100" };
            var nombreAna = NombreCompleto.Crear("Ana", "Pérez").Valor;
            var nombreLuis = NombreCompleto.Crear("Luis", "Gómez").Valor;

            var tabla = new TablaClasificacion
            {
                Id = TablaId,
                Nombre = "TablaX",
                MedallaAsociadaId = medalla.Id,
                MedallaAsociada = medalla,
               
            };
            _repoMock.Setup(r => r.GetByIdAsync(TablaId))
                     .ReturnsAsync(Resultado<TablaClasificacion>.Exitoso(tabla));

            var resultado = await _casoUso.EjecutarAsync(TablaId);

            Assert.True(resultado.EsExitoso);
            var dto = resultado.Valor;
            Assert.NotNull(dto);
            Assert.Equal(tabla.Id, dto.Id);
            Assert.Equal(tabla.Nombre, dto.Nombre);
            Assert.Equal(1, dto.Participantes[0].PerfilEstudianteId);
            Assert.Equal("Ana Pérez", dto.Participantes[0].NombreEstudiante);
            Assert.Equal(2, dto.Participantes[1].PerfilEstudianteId);
            Assert.Equal("Luis Gómez", dto.Participantes[1].NombreEstudiante);
        }
    }
}

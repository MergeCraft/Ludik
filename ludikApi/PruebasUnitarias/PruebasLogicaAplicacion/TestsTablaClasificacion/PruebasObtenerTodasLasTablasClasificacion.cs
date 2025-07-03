using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.TablaClasificacionDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.TablaClasificacion;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObjects;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsTablaClasificacion
{
    public class PruebasObtenerTodasLasTablasClasificacion
    {
        private readonly Mock<IRepositorioTablasClasificacion> _repoMock;
        private readonly ObtenerTodasLasTablasClasificacion _casoUso;
        private const int Tabla1Id = 1;
        private const int Tabla2Id = 2;

        public PruebasObtenerTodasLasTablasClasificacion()
        {
            _repoMock = new Mock<IRepositorioTablasClasificacion>();
            _casoUso = new ObtenerTodasLasTablasClasificacion(_repoMock.Object);
        }

        [Fact]
        public async Task EjecutarAsync_RepoFalla_RetornaFallo()
        {
            var error = new Error("Error.DB", "Error obtención");
            _repoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(Resultado<IEnumerable<TablaClasificacion>>.Falla(error));

            var resultado = await _casoUso.EjecutarAsync();

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == error.Codigo && e.Mensaje == error.Mensaje);
            _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task EjecutarAsync_SinTablas_RetornaListaVacia()
        {
            _repoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(Resultado<IEnumerable<TablaClasificacion>>.Exitoso(Enumerable.Empty<TablaClasificacion>()));

            var resultado = await _casoUso.EjecutarAsync();

            Assert.True(resultado.EsExitoso);
            Assert.NotNull(resultado.Valor);
            Assert.Empty(resultado.Valor);
        }

        [Fact]
        public async Task EjecutarAsync_ConTablas_RetornaDtosOrdenados()
        {
            var medallaA = new LogicaNegocio.Entidades.Medalla { Id = 10, Nombre = "A" };
            var medallaB = new LogicaNegocio.Entidades.Medalla { Id = 20, Nombre = "B" };

            var nombre1 = NombreCompleto.Crear("Ana", "Perez").Valor;
            var nombre2 = NombreCompleto.Crear("Valentina", "Gomez").Valor;
            var nombre3 = NombreCompleto.Crear("Matias", "Gonzalez").Valor;
            var nombre4 = NombreCompleto.Crear("Pedro", "Martinez").Valor;

            var tabla1 = new TablaClasificacion
            {
                Id = Tabla1Id,
                Nombre = "T1",
                MedallaAsociadaId = medallaA.Id,
                MedallaAsociada = medallaA,
                Participantes = new List<LogicaNegocio.Entidades.PerfilEstudiante>
                {
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = 1, Estudiante = new LogicaNegocio.Entidades.Estudiante{NombreCompleto=nombre1}, PerfilMedallas = new List<PerfilEstudianteMedalla> { new PerfilEstudianteMedalla { MedallaId = medallaA.Id } } },
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = 2, Estudiante = new LogicaNegocio.Entidades.Estudiante{NombreCompleto=nombre2}, PerfilMedallas = new List<PerfilEstudianteMedalla>() }
                }
            };
            var tabla2 = new TablaClasificacion
            {
                Id = Tabla2Id,
                Nombre = "T2",
                MedallaAsociadaId = medallaB.Id,
                MedallaAsociada = medallaB,
                Participantes = new List<LogicaNegocio.Entidades.PerfilEstudiante>
                {
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = 3, Estudiante = new LogicaNegocio.Entidades.Estudiante{NombreCompleto=nombre3}, PerfilMedallas = new List<PerfilEstudianteMedalla> { new PerfilEstudianteMedalla { MedallaId = medallaB.Id }, new PerfilEstudianteMedalla { MedallaId = medallaB.Id } } },
                    new LogicaNegocio.Entidades.PerfilEstudiante { Id = 4, Estudiante = new LogicaNegocio.Entidades.Estudiante{NombreCompleto=nombre4}, PerfilMedallas = new List<PerfilEstudianteMedalla> { new PerfilEstudianteMedalla { MedallaId = medallaB.Id } } }
                }
            };
           

            _repoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(Resultado<IEnumerable<TablaClasificacion>>.Exitoso(new[] { tabla1, tabla2 }));

            var resultado = await _casoUso.EjecutarAsync();

            Assert.True(resultado.EsExitoso);
            var dtos = resultado.Valor.ToList();
            Assert.Equal(2, dtos.Count);

            var dto1 = dtos.Single(d => d.Id == Tabla1Id);
            Assert.Equal("T1", dto1.Nombre);
            Assert.Equal(1, dto1.Participantes[0].PerfilEstudianteId);
            Assert.Equal(2, dto1.Participantes[1].PerfilEstudianteId);

            var dto2 = dtos.Single(d => d.Id == Tabla2Id);
            Assert.Equal("T2", dto2.Nombre);
            Assert.Equal(3, dto2.Participantes[0].PerfilEstudianteId);
            Assert.Equal(4, dto2.Participantes[1].PerfilEstudianteId);
        }
    }
}



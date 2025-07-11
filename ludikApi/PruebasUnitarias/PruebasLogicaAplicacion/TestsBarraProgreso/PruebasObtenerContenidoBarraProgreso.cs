using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.BarraProgresoDTOs;
using LogicaAplicacion.DTOsMappers.MedallaMappers;
using LogicaAplicacion.ImplementacionCasosUsos.BarraProgreso;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Moq;
using Xunit;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsBarraProgreso
{
    public class PruebasObtenerContenidoBarraProgreso
    {
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockPerfilRepo;
        private readonly ObtenerContenidoBarraProgreso _casoUso;
        private const int PerfilId = 42;
        private const string UsuarioId = "user-1";

        public PruebasObtenerContenidoBarraProgreso()
        {
            _mockPerfilRepo = new Mock<IRepositorioPerfilEstudianteGrupo>();
            _casoUso = new ObtenerContenidoBarraProgreso(_mockPerfilRepo.Object);
        }

        [Fact]
        public async Task PerfilNoExiste_RetornaNotFound()
        {
            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Falla(Error.NotFound));

            var resultado = await _casoUso.EjecutarAsync(PerfilId, UsuarioId);

            Assert.True(resultado.EsFallo);
            Assert.Equal("Error.NotFound", resultado.Errores.First().Codigo);
        }

        [Fact]
        public async Task UsuarioNoPerteneceAlPerfil_RetornaForbidden()
        {
            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                EstudianteId = "otro-user",
                Grupo = new LogicaNegocio.Entidades.Grupo() 
            };

            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var resultado = await _casoUso.EjecutarAsync(PerfilId, UsuarioId);

            Assert.True(resultado.EsFallo);
            Assert.Equal("Error.Forbidden", resultado.Errores.First().Codigo);
        }

        [Fact]
        public async Task CaminoFeliz_RetornaBarraProgresoDtoCorrecto()
        {
            var medallaA = new LogicaNegocio.Entidades.Medalla { Id = 1 };
            var medallaB = new LogicaNegocio.Entidades.Medalla { Id = 2 };
            var eq1 = new Equivalencia { Nota = 1, MedallasNecesarias = new List<LogicaNegocio.Entidades.Medalla> { medallaA } };
            var eq2 = new Equivalencia { Nota = 2, MedallasNecesarias = new List<LogicaNegocio.Entidades.Medalla> { medallaA, medallaB } };
            var tabla = new TablaEquivalencia
            {
                Equivalencias = new List<Equivalencia> { eq1, eq2 }
            };

            var perfil = new LogicaNegocio.Entidades.PerfilEstudiante
            {
                Id = PerfilId,
                EstudianteId = UsuarioId,
                Grupo = new LogicaNegocio.Entidades.Grupo { TablaEquivalencia = tabla },
                PerfilMedallas = new List<PerfilEstudianteMedalla>
        {
            new PerfilEstudianteMedalla
            {
                PerfilEstudianteId = PerfilId,
                MedallaId = medallaA.Id,
                Medalla = medallaA
            }
        }
            };

            _mockPerfilRepo
                .Setup(r => r.GetByIdAsync(PerfilId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(perfil));

            var resultado = await _casoUso.EjecutarAsync(PerfilId, UsuarioId);

            Assert.True(resultado.EsExitoso);

            var dto = resultado.Valor;
            Assert.Equal(1, dto.CalificacionActual);
            Assert.Equal(1, dto.CalificacionMinima);
            Assert.Equal(2, dto.CalificacionMaxima);

            var ids = dto.MedallasNecesariasParaSiguienteNota.Select(m => m.Id).ToList();
            Assert.Contains(1, ids);
            Assert.Contains(2, ids);
        }
    }
}
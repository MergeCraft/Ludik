using System;
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
    public class PruebasAltaProyectoAulaColaborativo
    {
        private readonly Mock<IRepositorioProyectoAulaColaborativo> _repoPacMock;
        private readonly Mock<IRepositorioGrupos> _repoGruposMock;
        private readonly Mock<IRepositorioRecompensas> _repoRecompensasMock;
        private readonly AltaProyectoAulaColaborativo _casoUso;
        private const int GrupoId = 10;
        private const int RecompensaId = 20;

        public PruebasAltaProyectoAulaColaborativo()
        {
            _repoPacMock = new Mock<IRepositorioProyectoAulaColaborativo>();
            _repoGruposMock = new Mock<IRepositorioGrupos>();
            _repoRecompensasMock = new Mock<IRepositorioRecompensas>();
            _casoUso = new AltaProyectoAulaColaborativo(
                _repoPacMock.Object,
                _repoGruposMock.Object,
                _repoRecompensasMock.Object
            );

            // Default: grupo existe
            var grupo = new LogicaNegocio.Entidades.Grupo { Id = GrupoId };
            _repoGruposMock
                .Setup(r => r.GetByIdAsync(GrupoId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Grupo>.Exitoso(grupo));

            // Default: sin proyectos existentes (lista de tipo List<ProyectoAulaColaborativo>)
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo>()));

            // Default: recompensa existe
            var recompensa = new LogicaNegocio.Entidades.RecompensaSimple { Id = RecompensaId };
            _repoRecompensasMock
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Exitoso(recompensa));

            // Default: AddAsync devuelve exito
            _repoPacMock
                .Setup(r => r.AddAsync(It.IsAny<ProyectoAulaColaborativo>()))
                .ReturnsAsync(Resultado.Exitoso());
        }

        [Fact]
        public async Task EjecutarAsync_NullDto_RetornaValidationError()
        {
            var resultado = await _casoUso.EjecutarAsync(GrupoId, null);

            Assert.True(resultado.EsFallo);
            Assert.Contains(resultado.Errores, e => e.Codigo == "Error.Validation");
        }

        [Fact]
        public async Task EjecutarAsync_GrupoNoExiste_RetornaValidationError()
        {
            _repoGruposMock
                .Setup(r => r.GetByIdAsync(GrupoId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Grupo>.Falla(new Error("Error.Validation", "No existe")));

            var dto = new AltaProyectoAulaColaborativoDto
            {
                Nombre = "P1",
                Visual = new LogicaNegocio.ValueObject.MetaVisual(),
                CantidadMedallasNecesarias = 1,
                RecompensaClaseId = RecompensaId
            };

            var res = await _casoUso.EjecutarAsync(GrupoId, dto);
            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.Validation");
        }

        [Fact]
        public async Task EjecutarAsync_ProyectoExistenteActivo_RetornaValidationError()
        {
            // Existe un PAC activo
            var activo = new ProyectoAulaColaborativo { Estado = EstadoPAC.Activo };
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Exitoso(new List<ProyectoAulaColaborativo> { activo }));

            var dto = new AltaProyectoAulaColaborativoDto
            {
                Nombre = "P2",
                Visual = new LogicaNegocio.ValueObject.MetaVisual(),
                CantidadMedallasNecesarias = 2,
                RecompensaClaseId = RecompensaId
            };

            var res = await _casoUso.EjecutarAsync(GrupoId, dto);
            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.Validation"
                && e.Mensaje.Contains("activo"));
        }

        [Fact]
        public async Task EjecutarAsync_RecompensaNoExiste_RetornaValidationError()
        {
            _repoRecompensasMock
                .Setup(r => r.GetByIdAsync(RecompensaId))
                .ReturnsAsync(Resultado<LogicaNegocio.Entidades.Recompensa>.Falla(new Error("Error.Validation", "No hay recompensa")));

            var dto = new AltaProyectoAulaColaborativoDto
            {
                Nombre = "P3",
                Visual = new LogicaNegocio.ValueObject.MetaVisual(),
                CantidadMedallasNecesarias = 3,
                RecompensaClaseId = RecompensaId
            };

            var res = await _casoUso.EjecutarAsync(GrupoId, dto);
            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.Validation"
                && e.Mensaje.Contains(RecompensaId.ToString()));
        }

        [Fact]
        public async Task EjecutarAsync_RepositorioGetByGrupoError_RetornaUnexpected()
        {
            _repoPacMock
                .Setup(r => r.GetByGrupoAsync(GrupoId))
                .ReturnsAsync(Resultado<List<ProyectoAulaColaborativo>>.Falla(
                    new Error("Error.Unexpected", "falló pacs")));

            var dto = new AltaProyectoAulaColaborativoDto
            {
                Nombre = "P4",
                Visual = new LogicaNegocio.ValueObject.MetaVisual(),
                CantidadMedallasNecesarias = 4,
                RecompensaClaseId = RecompensaId
            };

            var res = await _casoUso.EjecutarAsync(GrupoId, dto);
            Assert.True(res.EsFallo);
            Assert.Contains(res.Errores, e => e.Codigo == "Error.Unexpected");
        }

        //[Fact]
        //public async Task EjecutarAsync_DatosValidos_LlamaAddAsync()
        //{
        //    var dto = new AltaProyectoAulaColaborativoDto
        //    {
        //        Nombre = "P5",
        //        Visual = MetaVisual.Piramide,
        //        CantidadMedallasNecesarias = 5,
        //        RecompensaClaseId = RecompensaId
        //    };

        //    var res = await _casoUso.EjecutarAsync(GrupoId, dto);

        //    Assert.True(res.EsExitoso);
        //    _repoPacMock.Verify(r => r.AddAsync(
        //        It.Is<ProyectoAulaColaborativo>(p =>
        //            p.GrupoId == GrupoId &&
        //            p.Nombre == dto.Nombre &&
        //            p.Visual == dto.Visual &&
        //            p.CantidadMedallasNecesarias == dto.CantidadMedallasNecesarias &&
        //            p.RecompensaClaseId == dto.RecompensaClaseId &&
        //            p.Estado == EstadoPAC.Activo
        //        )), Times.Once);
        //}
    }
}

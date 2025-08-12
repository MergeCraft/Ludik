using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObject;
using LogicaNegocio.Resultados;

namespace PruebasUnitarias.PruebasLogicaAplicacion.SolicitudUnion
{
    public class PruebasAceptarSolicitudUnion
    {
        private readonly Mock<IRepositorioSolicitudesUnion> _mockRepoSolicitudes;
        private readonly Mock<IRepositorioGrupos> _mockRepoGrupos;
        private readonly Mock<IRepositorioPerfilEstudianteGrupo> _mockRepoPerfil;
        private readonly AceptarSolicitudUnion _casoUso;

        //public PruebasAceptarSolicitudUnion()
        //{
        //    _mockRepoSolicitudes = new Mock<IRepositorioSolicitudesUnion>();
        //    _mockRepoGrupos = new Mock<IRepositorioGrupos>();
        //    _mockRepoPerfil = new Mock<IRepositorioPerfilEstudianteGrupo>();

        //    _casoUso = new AceptarSolicitudUnion(
        //        _mockRepoSolicitudes.Object,
        //        _mockRepoGrupos.Object,
        //        _mockRepoPerfil.Object);
        //}

        //[Fact]
        //public async Task EjecutarAsync_SolicitudNoExiste_RetornaNotFound()
        //{
        //    // Arrange
        //    _mockRepoSolicitudes
        //        .Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
        //        .ReturnsAsync((LogicaNegocio.Entidades.SolicitudUnion)null!);

        //    // Act
        //    var resultado = await _casoUso.EjecutarAsync(1);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("no existe"));
        //    _mockRepoPerfil.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.PerfilEstudiante>()), Times.Never);
        //    _mockRepoGrupos.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Grupo>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_SolicitudYaProcesada_RetornaFallo()
        //{
        //    // Arrange
        //    var solicitud = new LogicaNegocio.Entidades.SolicitudUnion { Estado = EstadoSolicitud.Aceptada };
        //    _mockRepoSolicitudes
        //        .Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
        //        .ReturnsAsync(solicitud);

        //    // Act
        //    var resultado = await _casoUso.EjecutarAsync(1);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("ya fue procesada"));
        //    _mockRepoPerfil.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.PerfilEstudiante>()), Times.Never);
        //    _mockRepoGrupos.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Grupo>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_Exitoso_RetornaResultadoExitoso()
        //{
        //    // Arrange
        //    var grupo = new LogicaNegocio.Entidades.Grupo { Alumnos = new List<LogicaNegocio.Entidades.PerfilEstudiante>() };
        //    var solicitud = new LogicaNegocio.Entidades.SolicitudUnion
        //    {
        //        Estado = EstadoSolicitud.Pendiente,
        //        EstudianteId = "estu123",
        //        Grupo = grupo
        //    };

        //    _mockRepoSolicitudes
        //        .Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
        //        .ReturnsAsync(solicitud);

        //    _mockRepoPerfil
        //        .Setup(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.PerfilEstudiante>()))
        //        .ReturnsAsync((LogicaNegocio.Entidades.PerfilEstudiante p) => Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(p));

        //    _mockRepoSolicitudes
        //        .Setup(r => r.UpdateAsync(solicitud))
        //        .ReturnsAsync(Resultado.Exitoso());

        //    _mockRepoGrupos
        //        .Setup(r => r.UpdateAsync(grupo))
        //        .ReturnsAsync(Resultado.Exitoso());

        //    // Act
        //    var resultado = await _casoUso.EjecutarAsync(1);

        //    // Assert
        //    Assert.True(resultado.EsExitoso);
        //    Assert.Equal(EstadoSolicitud.Aceptada, solicitud.Estado);
        //    Assert.Single(grupo.Alumnos);
        //    Assert.Equal("estu123", grupo.Alumnos[0].EstudianteId);
        //    _mockRepoPerfil.Verify(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.PerfilEstudiante>()), Times.Once);
        //    _mockRepoSolicitudes.Verify(r => r.UpdateAsync(solicitud), Times.Once);
        //    _mockRepoGrupos.Verify(r => r.UpdateAsync(grupo), Times.Once);
        //}

        //[Fact]
        //public async Task EjecutarAsync_ErrorAlActualizarSolicitud_RetornaFallo()
        //{
        //    // Arrange
        //    var grupo = new LogicaNegocio.Entidades.Grupo();
        //    var solicitud = new LogicaNegocio.Entidades.SolicitudUnion
        //    {
        //        Estado = EstadoSolicitud.Pendiente,
        //        EstudianteId = "estu123",
        //        Grupo = grupo
        //    };

        //    _mockRepoSolicitudes
        //        .Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
        //        .ReturnsAsync(solicitud);

        //    _mockRepoPerfil
        //        .Setup(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.PerfilEstudiante>()))
        //        .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(new LogicaNegocio.Entidades.PerfilEstudiante()));

        //    _mockRepoSolicitudes
        //        .Setup(r => r.UpdateAsync(solicitud))
        //        .ReturnsAsync(Resultado.Falla(new Error("Solicitud", "Error al actualizar")));

        //    // Act
        //    var resultado = await _casoUso.EjecutarAsync(1);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("Error al actualizar"));
        //    _mockRepoGrupos.Verify(r => r.UpdateAsync(It.IsAny<LogicaNegocio.Entidades.Grupo>()), Times.Never);
        //}

        //[Fact]
        //public async Task EjecutarAsync_ErrorAlActualizarGrupo_RetornaFallo()
        //{
        //    // Arrange
        //    var grupo = new LogicaNegocio.Entidades.Grupo();
        //    var solicitud = new LogicaNegocio.Entidades.SolicitudUnion
        //    {
        //        Estado = EstadoSolicitud.Pendiente,
        //        EstudianteId = "estu123",
        //        Grupo = grupo
        //    };

        //    _mockRepoSolicitudes
        //        .Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
        //        .ReturnsAsync(solicitud);

        //    _mockRepoPerfil
        //        .Setup(r => r.AddAsync(It.IsAny<LogicaNegocio.Entidades.PerfilEstudiante>()))
        //        .ReturnsAsync(Resultado<LogicaNegocio.Entidades.PerfilEstudiante>.Exitoso(new LogicaNegocio.Entidades.PerfilEstudiante()));

        //    _mockRepoSolicitudes
        //        .Setup(r => r.UpdateAsync(solicitud))
        //        .ReturnsAsync(Resultado.Exitoso());

        //    _mockRepoGrupos
        //        .Setup(r => r.UpdateAsync(grupo))
        //        .ReturnsAsync(Resultado.Falla(new Error("Grupo", "Error al actualizar grupo")));

        //    // Act
        //    var resultado = await _casoUso.EjecutarAsync(1);

        //    // Assert
        //    Assert.True(resultado.EsFallo);
        //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("Error al actualizar grupo"));
        //}
    }
}
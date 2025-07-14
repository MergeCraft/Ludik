using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;
using Moq;

public class PruebasAceptarSolicitudUnion
{
    //private readonly Mock<IRepositorioSolicitudesUnion> _mockRepoSolicitudes = new();
    //private readonly Mock<IRepositorioGrupos> _mockRepoGrupos = new();
    //private readonly AceptarSolicitudUnion _casoUso;

    //public PruebasAceptarSolicitudUnion()
    //{
    //    _casoUso = new AceptarSolicitudUnion(_mockRepoSolicitudes.Object, _mockRepoGrupos.Object);
    //}

    //[Fact]
    //public async Task EjecutarAsync_SolicitudNoExiste_RetornaNotFound()
    //{
    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync((SolicitudUnion)null!);

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsFallo);
    //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("no existe"));
    //}

    //[Fact]
    //public async Task EjecutarAsync_SolicitudYaProcesada_RetornaFallo()
    //{
    //    var solicitud = new SolicitudUnion { Estado = EstadoSolicitud.Aceptada };
    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync(solicitud);

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsFallo);
    //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("ya fue procesada"));
    //}

    //[Fact]
    //public async Task EjecutarAsync_Exitoso_RetornaResultadoExitoso()
    //{
    //    var grupo = new Grupo { Alumnos = new List<PerfilEstudiante>() };
    //    var solicitud = new SolicitudUnion
    //    {
    //        Estado = EstadoSolicitud.Pendiente,
    //        EstudianteId = "estu123",
    //        Grupo = grupo
    //    };

    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync(solicitud);
    //    _mockRepoSolicitudes.Setup(r => r.UpdateAsync(solicitud)).ReturnsAsync(Resultado.Exitoso());
    //    _mockRepoGrupos.Setup(r => r.UpdateAsync(grupo)).ReturnsAsync(Resultado.Exitoso());

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsExitoso);
    //    Assert.Equal(EstadoSolicitud.Aceptada, solicitud.Estado);
    //    Assert.Single(grupo.Alumnos);
    //    Assert.Equal("estu123", grupo.Alumnos[0].EstudianteId);
    //}

    //[Fact]
    //public async Task EjecutarAsync_ErrorAlActualizarSolicitud_RetornaFallo()
    //{
    //    var grupo = new Grupo();
    //    var solicitud = new SolicitudUnion
    //    {
    //        Estado = EstadoSolicitud.Pendiente,
    //        EstudianteId = "estu123",
    //        Grupo = grupo
    //    };

    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync(solicitud);
    //    _mockRepoSolicitudes.Setup(r => r.UpdateAsync(solicitud))
    //        .ReturnsAsync(Resultado.Falla(new Error("Solicitud", "Error al actualizar")));

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsFallo);
    //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("Error al actualizar"));
    //}

    //[Fact]
    //public async Task EjecutarAsync_ErrorAlActualizarGrupo_RetornaFallo()
    //{
    //    var grupo = new Grupo();
    //    var solicitud = new SolicitudUnion
    //    {
    //        Estado = EstadoSolicitud.Pendiente,
    //        EstudianteId = "estu123",
    //        Grupo = grupo
    //    };

    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync(solicitud);
    //    _mockRepoSolicitudes.Setup(r => r.UpdateAsync(solicitud)).ReturnsAsync(Resultado.Exitoso());
    //    _mockRepoGrupos.Setup(r => r.UpdateAsync(grupo))
    //        .ReturnsAsync(Resultado.Falla(new Error("Grupo", "Error al actualizar grupo")));

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsFallo);
    //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("Error al actualizar grupo"));
    //}
}
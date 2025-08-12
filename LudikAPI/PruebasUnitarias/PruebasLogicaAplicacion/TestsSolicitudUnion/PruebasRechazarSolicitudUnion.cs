using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.SolicitudUnion;
using LogicaNegocio.Resultados;
using LogicaNegocio.ValueObject;
using Moq;

public class PruebasRechazarSolicitudUnion
{
    private readonly Mock<IRepositorioSolicitudesUnion> _mockRepoSolicitudes = new();
    private readonly RechazarSolicitudUnion _casoUso;

    //public PruebasRechazarSolicitudUnion()
    //{
    //    _casoUso = new RechazarSolicitudUnion(_mockRepoSolicitudes.Object);
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
    //    var solicitud = new SolicitudUnion { Estado = EstadoSolicitud.Rechazada };
    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync(solicitud);

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsFallo);
    //    Assert.Contains(resultado.Errores, e => e.Mensaje.Contains("ya fue procesada"));
    //}

    //[Fact]
    //public async Task EjecutarAsync_ErrorAlActualizar_RetornaFallo()
    //{
    //    var solicitud = new SolicitudUnion
    //    {
    //        Estado = EstadoSolicitud.Pendiente
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
    //public async Task EjecutarAsync_Exitoso_RetornaResultadoExitoso()
    //{
    //    var solicitud = new SolicitudUnion
    //    {
    //        Estado = EstadoSolicitud.Pendiente
    //    };

    //    _mockRepoSolicitudes.Setup(r => r.GetSolicitudConEstudianteYGrupoPorIdAsync(It.IsAny<int>()))
    //        .ReturnsAsync(solicitud);
    //    _mockRepoSolicitudes.Setup(r => r.UpdateAsync(solicitud))
    //        .ReturnsAsync(Resultado.Exitoso());

    //    var resultado = await _casoUso.EjecutarAsync(1);

    //    Assert.True(resultado.EsExitoso);
    //    Assert.Equal(EstadoSolicitud.Rechazada, solicitud.Estado);
    //}
}

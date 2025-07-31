using InterfacesRepositorio;
using LogicaAplicacion.ImplementacionCasosUsos.Tienda;
using LogicaAplicacion.ImplementacionServicios;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;

public class PruebasObtenerListadoRecompensa
{
    private readonly Mock<IRepositorioTiendas> _mockRepoTiendas;
    private readonly Mock<IRepositorioAlmacenamientoArchivos> _mockRepoArchivos;
    private readonly IGeneradorUrlsParaColeccionesImagenes _generadorUrlsParaColecciones;
    private readonly ObtenerListadoRecompensa _useCase;

    public PruebasObtenerListadoRecompensa()
    {
        _mockRepoTiendas = new Mock<IRepositorioTiendas>();
        _mockRepoArchivos = new Mock<IRepositorioAlmacenamientoArchivos>();

        // Mock para las URLs de imágenes
        _mockRepoArchivos
            .Setup(r => r.ObtenerArchivoSasUrlAsync(It.IsAny<string>()))
            .ReturnsAsync((string nombreArchivo) =>
                Resultado<string>.Exitoso($"url-fake/{nombreArchivo}"));

        _generadorUrlsParaColecciones =
            new GeneradorUrlsParaColeccionesImagenes(
                new GeneradorUrlImagen(_mockRepoArchivos.Object));

        _useCase = new ObtenerListadoRecompensa(
            _mockRepoTiendas.Object,
            _generadorUrlsParaColecciones
        );
    }

    



    [Fact]
    public async Task CaminoFeliz_RetornaDtos()
    {
        var recompensas = new List<LogicaNegocio.Entidades.Recompensa>
        {
            new RecompensaSimple { Id = 1, Nombre = "R1", Precio = 5, NombreImagenCompleta = "c1", NombreImagenMiniatura = "m1" },
            new RecompensaSimple { Id = 2, Nombre = "R2", Precio = 10, NombreImagenCompleta = "c2", NombreImagenMiniatura = "m2" }
        };

        var tienda = new Tienda
        {
            Id = 20,
            Recompesas = recompensas
        };

        _mockRepoTiendas
            .Setup(r => r.GetByIdAsync(20))
            .ReturnsAsync(Resultado<Tienda>.Exitoso(tienda));

        var resultado = await _useCase.EjecutarAsync(20);

        Assert.True(resultado.EsExitoso);
        var lista = resultado.Valor!.ToList();
        Assert.Equal(2, lista.Count);

        Assert.Contains(lista, dto =>
            dto.Nombre == "R1" &&
            dto.Precio == 5 &&
            dto.EnlaceImagenCompleta == "url-fake/c1" &&
            dto.EnlaceImagenMiniatura == "url-fake/m1");

        Assert.Contains(lista, dto =>
            dto.Nombre == "R2" &&
            dto.Precio == 10 &&
            dto.EnlaceImagenCompleta == "url-fake/c2" &&
            dto.EnlaceImagenMiniatura == "url-fake/m2");
    }
}

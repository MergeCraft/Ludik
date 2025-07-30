using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.ImplementacionCasosUsos.Kudo;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Moq;

namespace PruebasUnitarias.PruebasLogicaAplicacion.TestsKudo;

public class PruebasObtenerKudos
{

    private readonly Mock<IRepositorioTiposKudo> _mockRepositorioKudos;
    private readonly Mock<IGeneradorUrlsParaColeccionesImagenes> _mockGeneradorUrls;
    private readonly ObtenerKudos _casoUso;

    public PruebasObtenerKudos()
    {
        _mockRepositorioKudos = new Mock<IRepositorioTiposKudo>();
        _mockGeneradorUrls = new Mock<IGeneradorUrlsParaColeccionesImagenes>();
        _casoUso = new ObtenerKudos(
            _mockRepositorioKudos.Object,
            _mockGeneradorUrls.Object
        );
    }

    [Fact]
    public async Task EjecutarAsync_RepositorioFalla_DebeRetornarResultadoDeFallo()
    {
        // Arrange (Organizar)
        var errorEsperado = Error.Unexpected;
        _mockRepositorioKudos
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(Resultado<IEnumerable<TipoKudo>>.Falla(errorEsperado));

        // Act (Actuar)
        var resultado = await _casoUso.EjecutarAsync();

        // Assert (Afirmar)
        Assert.True(resultado.EsFallo);
        Assert.Equal(errorEsperado.Codigo, resultado.Errores.First().Codigo);

        // Verificamos que el generador de URLs nunca fue llamado si el repositorio falló.
        _mockGeneradorUrls.Verify(
            gen => gen.EjecutarProcesarUrlsAsync(
                It.IsAny<IEnumerable<TipoKudoDto>>(),
                It.IsAny<(Func<TipoKudoDto, string>, Action<TipoKudoDto, string>)[]>()),
            Times.Never);
    }

    [Fact]
    public async Task EjecutarAsync_NoExistenKudos_DebeRetornarListaVaciaExitosamente()
    {
        // Arrange (Organizar)
        // FA 1: No hay kudos configurados
        var listaVaciaDeKudos = new List<TipoKudo>();
        _mockRepositorioKudos
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(Resultado<IEnumerable<TipoKudo>>.Exitoso(listaVaciaDeKudos));

        // Act (Actuar)
        var resultado = await _casoUso.EjecutarAsync();

        // Assert (Afirmar)
        Assert.True(resultado.EsExitoso);
        Assert.NotNull(resultado.Valor);
        Assert.Empty(resultado.Valor);
    }

    [Fact]
    public async Task EjecutarAsync_ExistenKudos_DebeRetornarListaDeDtosConUrlsGeneradas()
    {
        // Arrange (Organizar)
        // Flujo Principal
        var kudosDesdeRepo = new List<TipoKudo>
            {
                new TipoKudo { Id = 1, Nombre = "Trabajo en Equipo", NombreImagenMiniatura = "equipo.png" },
                new TipoKudo { Id = 2, Nombre = "Gran Ayuda", NombreImagenMiniatura = "ayuda.png" }
            };
        _mockRepositorioKudos
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(Resultado<IEnumerable<TipoKudo>>.Exitoso(kudosDesdeRepo));

        // Simulamos el comportamiento del generador de URLs.
        // Usamos .Callback para modificar la colección de DTOs tal como lo haría el servicio real.
        _mockGeneradorUrls
            .Setup(gen => gen.EjecutarProcesarUrlsAsync(
                It.IsAny<IEnumerable<TipoKudoDto>>(),
                It.IsAny<(Func<TipoKudoDto, string>, Action<TipoKudoDto, string>)[]>()))
            .Callback<IEnumerable<TipoKudoDto>, (Func<TipoKudoDto, string>, Action<TipoKudoDto, string>)[]>((dtos, _) =>
            {
                foreach (var dto in dtos)
                {
                    // Simulamos que le asignamos una URL completa
                    dto.EnlaceImagenMiniatura = "https://storage.azure.com/" + dto.EnlaceImagenMiniatura;
                }
            });


        // Act (Actuar)
        var resultado = await _casoUso.EjecutarAsync();

        // Assert (Afirmar)
        Assert.True(resultado.EsExitoso);
        Assert.NotNull(resultado.Valor);
        Assert.Equal(2, resultado.Valor.Count());

        // Verificamos el primer kudo
        var primerKudo = resultado.Valor.First();
        Assert.Equal(1, primerKudo.Id);
        Assert.Equal("Trabajo en Equipo", primerKudo.Nombre);
        // Comprobamos que la URL fue modificada por nuestro Callback del mock
        Assert.Equal("https://storage.azure.com/equipo.png", primerKudo.EnlaceImagenMiniatura);

        // Verificamos que el servicio para generar URLs fue llamado exactamente una vez.
        _mockGeneradorUrls.Verify(
           gen => gen.EjecutarProcesarUrlsAsync(
               It.IsAny<IEnumerable<TipoKudoDto>>(),
               It.IsAny<(Func<TipoKudoDto, string>, Action<TipoKudoDto, string>)[]>()),
           Times.Once);
    }

}
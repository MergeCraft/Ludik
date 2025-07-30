using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.DTOsMappers.KudoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Kudo;
using LogicaAplicacion.Servicios;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Kudo;

public class ObtenerKudos: IObtenerKudos
{
    private readonly IRepositorioTiposKudo _repositorioTiposKudo;
    private readonly IGeneradorUrlsParaColeccionesImagenes _generadorUrlsParaColecciones;

    public ObtenerKudos(
        IRepositorioTiposKudo repositorioTiposKudo,
        IGeneradorUrlsParaColeccionesImagenes generadorUrlsParaColecciones)
    {
        _repositorioTiposKudo = repositorioTiposKudo;
        _generadorUrlsParaColecciones = generadorUrlsParaColecciones;
    }
    public async Task<Resultado<IEnumerable<TipoKudoDto>>> EjecutarAsync()
    {
        var resultadoRepo = await _repositorioTiposKudo.GetAllAsync();

        if (resultadoRepo.EsFallo)
            return Resultado<IEnumerable<TipoKudoDto>>.Falla(resultadoRepo.Errores);
        
        var kudosDto = resultadoRepo.Valor.Select(kudo => KudoMapper.toDto(kudo)).ToList();
        await _generadorUrlsParaColecciones.EjecutarProcesarUrlsAsync(kudosDto,
            (dto => dto.EnlaceImagenMiniatura, (dto, url) => dto.EnlaceImagenMiniatura = url));

        return Resultado<IEnumerable<TipoKudoDto>>.Exitoso(kudosDto);
    }
}
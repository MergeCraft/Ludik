using InterfacesRepositorio;
using LogicaAplicacion.DTOs.MedallaDTOs;
using LogicaAplicacion.InterfacesCasosUsos.Medalla;
using LogicaAplicacion.DTOsMappers.MedallaMappers;
using LogicaAplicacion.Servicios;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.Medallas;

public class ObtenerMedallaPorId : IObtenerMedallaPorId
{
	private readonly IRepositorioMedallas _repositorioMedallas;
	private readonly IGeneradorUrlImagen _generadorUrlImagen;

    public ObtenerMedallaPorId(IRepositorioMedallas repositorioMedallas
        , IGeneradorUrlImagen generadorUrlImagen)
	{
		_repositorioMedallas = repositorioMedallas;
		_generadorUrlImagen = generadorUrlImagen;
    }

	public async Task<Resultado<MedallaDto>> EjecutarAsync(int idMedalla)
	{
		var resultadoRepo = await _repositorioMedallas.GetByIdAsync(idMedalla);

		if (resultadoRepo.EsFallo)
			return Resultado<MedallaDto>.Falla(resultadoRepo.Errores);

		var medalla = resultadoRepo.Valor;

		var medallaDto = MedallaMapper.toDto(medalla);
		medallaDto.UrlImagen = await _generadorUrlImagen.GenerarUrlLecturaAsync(medallaDto.UrlImagen);

        return Resultado<MedallaDto>.Exitoso(medallaDto);
	}
}
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.PreguntasDeSeguridadDTOs;
using LogicaAplicacion.DTOsMappers.PreguntaDeSeguridadMappers;
using LogicaAplicacion.InterfacesCasosUsos.RecuperarContrasena;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;

namespace LogicaAplicacion.ImplementacionCasosUsos.RecuperarContrasena;

public class ObtenerPreguntasDeSeguridadPorNombreUsuario: IObtenerPreguntasDeSegurididadPorNombreUsuario
{

    private readonly IRepositorioEstudiantes _repositorioEstudiantes;
    public ObtenerPreguntasDeSeguridadPorNombreUsuario(IRepositorioEstudiantes repositorioEstudiantes)
    {
        _repositorioEstudiantes = repositorioEstudiantes;
    }
    public async Task<Resultado<PreguntasDto>> EjecutarAsync(string nombreUsuario)
    {
        Resultado<Estudiante> resultadoEstudiante = await _repositorioEstudiantes.GetByStringIdAsync(nombreUsuario);

  
        if (resultadoEstudiante.EsFallo)
            return Resultado<PreguntasDto>.Falla(resultadoEstudiante.Errores);
        

        var estudiante = resultadoEstudiante.Valor;

        PreguntasDto dto = PreguntaDeSeguridadMapper.ToDto(estudiante.PreguntasSeguridad);

        return Resultado<PreguntasDto>.Exitoso(dto);
    }
}
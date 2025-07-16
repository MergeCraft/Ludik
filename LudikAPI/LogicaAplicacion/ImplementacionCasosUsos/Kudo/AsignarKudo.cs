using System.ComponentModel;
using InterfacesRepositorio;
using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.DTOsMappers.KudoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Kudo;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Entidades = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.Kudo;

public class AsignarKudo: IAsignarKudo
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfil;
    private readonly IRepositorioEstudiantes _repositorioEstudiantes;
    private readonly IRepositorioTiposKudo _repositorioTiposKudo;
    private readonly IUnitOfWork _unitOfWork;

    public AsignarKudo(IRepositorioPerfilEstudianteGrupo repositorioPerfil, IRepositorioTiposKudo repositorioTipoKudo, IRepositorioEstudiantes repositorioEstudiantes, IUnitOfWork unitOfWork)
    {
        _repositorioPerfil = repositorioPerfil;
        _repositorioEstudiantes = repositorioEstudiantes;
        _repositorioTiposKudo = repositorioTipoKudo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Resultado> EjecutarAsync(string idEstudianteEmisor, AsignarKudoDto dto)
    {
        var resultadoEstudianteEmisor = await _repositorioEstudiantes.GetByStringIdAsync(idEstudianteEmisor);
        if (resultadoEstudianteEmisor.EsFallo)
            return Resultado.Falla(resultadoEstudianteEmisor.Errores);

        Estudiante estudianteEmisor = resultadoEstudianteEmisor.Valor;

        Resultado<Entidades.PerfilEstudiante> resultadoPerfilEstudianteEmisor= estudianteEmisor.ObtenerPerfilEstudiantePor(dto.IdPerfilEstudianteEmisor);

        if (resultadoPerfilEstudianteEmisor.EsFallo)
            return Resultado.Falla(resultadoPerfilEstudianteEmisor.Errores);
        
        Entidades.PerfilEstudiante perfilEstudianteEmisor = resultadoPerfilEstudianteEmisor.Valor;

        if (perfilEstudianteEmisor.Id == dto.IdPerfilEstudianteRecibe)
            return Resultado.Falla(Error.Forbidden);
        

        var resultadoPerfilReceptor = await _repositorioPerfil.GetByIdAsync(dto.IdPerfilEstudianteRecibe);
        if (resultadoPerfilReceptor.EsFallo)
            return Resultado.Falla(resultadoPerfilReceptor.Errores);
        

        Entidades.PerfilEstudiante perfilEstudianteReceptor = resultadoPerfilReceptor.Valor;

        var resultadoTipoKudo = await _repositorioTiposKudo.GetByIdAsync(dto.Kudo.Id);

        if (resultadoTipoKudo.EsFallo)
            return Resultado.Falla(resultadoTipoKudo.Errores);
        

        var resultadoOtorgar = perfilEstudianteEmisor.OtorgarKudo(resultadoTipoKudo.Valor, perfilEstudianteReceptor);
        if (resultadoOtorgar.EsFallo)
            return resultadoOtorgar;
        perfilEstudianteReceptor.KudosRecibidos.Add(resultadoOtorgar.Valor);

        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Resultado.Falla(Error.Unexpected); 
        }

        // 7. Evaluar si se debe asignar medalla (este será el siguiente paso)
        // ... lógica de observer/eventos para notificar al sistema de medallas ...

        return Resultado.Exitoso();
    }
}
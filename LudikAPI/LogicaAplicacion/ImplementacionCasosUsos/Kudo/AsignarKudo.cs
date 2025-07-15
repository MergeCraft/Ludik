using InterfacesRepositorio;
using LogicaAplicacion.DTOs.KudoDTOs;
using LogicaAplicacion.DTOsMappers.KudoMappers;
using LogicaAplicacion.InterfacesCasosUsos.Kudo;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Entidades = LogicaNegocio.Entidades;

namespace LogicaAplicacion.ImplementacionCasosUsos.Kudo;

public class AsignarKudo: IAsignarKudo
{
    private readonly IRepositorioPerfilEstudianteGrupo _repositorioPerfil;
    private readonly IRepositorioTipoKudo _repositorioTipoKudo;
    private readonly IUnitOfWork _unitOfWork;

    public AsignarKudo(IRepositorioPerfilEstudianteGrupo repositorioPerfil, IRepositorioTipoKudo repositorioTipoKudo, IUnitOfWork unitOfWork)
    {
        _repositorioPerfil = repositorioPerfil;
        _repositorioTipoKudo = repositorioTipoKudo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Resultado> EjecutarAsync(string idEstudianteEmisor, AsignarKudoDto dto)
    {
        // 1. Validar que no se auto-asigne el kudo
        var perfilEmisor = await _repositorioPerfil.ObtenerPorIdUsuario(idEstudianteEmisor);
        if (perfilEmisor == null)
        {
            return Resultado.Falla(Error.NotFound); // O un error más específico
        }

        if (perfilEmisor.Id == dto.IdPerfilEstudianteReceptor)
        {
            return Resultado.Falla(Error.Forbidden);
        }

        // 2. Obtener el perfil del receptor
        var resultadoPerfilReceptor = await _repositorioPerfil.GetByIdAsync(dto.IdPerfilEstudianteReceptor);
        if (resultadoPerfilReceptor.EsFallo)
        {
            return Resultado.Falla(resultadoPerfilReceptor.Errores);
        }

        Entidades.PerfilEstudiante perfilEstudianteReceptor = resultadoPerfilReceptor.Valor;
        // 3. Obtener el tipo de Kudo (la razón)
        var tipoKudo = await _repositorioTipoKudo.GetByIdAsync(dto.Kudo.Id);
        if (tipoKudo == null)
        {
            return Resultado.Falla(new Error("Error.Invalid", $"El  kudo  no es válido."));
        }

        // 4. Ejecutar la lógica de negocio en la entidad Emisor
        var resultadoOtorgar = perfilEmisor.OtorgarKudo();
        if (resultadoOtorgar.EsFallo)
        {
            return resultadoOtorgar; // Req B.6: Devolverá el error "Sin saldo"
        }

        // 5. Crear el registro del evento
        var kudoOtorgado = new KudoOtorgado(perfilEmisor, perfilEstudianteReceptor, tipoKudo, DateTime.UtcNow);

        // ¡Esta parte es crucial! El kudo debe ser añadido a la colección del receptor
        // para que EF Core cree la relación en la base de datos.
        perfilEstudianteReceptor.KudosRecibidos.Add(kudoOtorgado);

        // 6. Persistir todos los cambios en una única transacción
        try
        {
            // El UnitOfWork guardará el cambio en perfilEmisor.CantidadKudosDisponibles
            // y creará el nuevo registro en la tabla KudoOtorgado.
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Lógica de logging del error real 'ex'
            return Resultado.Falla(Error.Unexpected); // Req: Error del sistema
        }

        // 7. Evaluar si se debe asignar medalla (este será el siguiente paso)
        // ... lógica de observer/eventos para notificar al sistema de medallas ...

        return Resultado.Exitoso();
    }
}
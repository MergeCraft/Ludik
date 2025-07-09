using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace AccesoDatos.RepositoriosEF;

public class RepositorioPreguntasDeSeguridadDelSistemaEF: IRepositorioPreguntasDeSeguridadDelSistema
{
    private readonly ContextoDb _db;
    public RepositorioPreguntasDeSeguridadDelSistemaEF(ContextoDb db)
    {
        _db = db;
    }

    public Task<Resultado> AddAsync(PreguntaDeSeguridad unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(PreguntaDeSeguridad unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> UpdateAsync(PreguntaDeSeguridad unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<PreguntaDeSeguridad>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<IEnumerable<PreguntaDeSeguridad>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
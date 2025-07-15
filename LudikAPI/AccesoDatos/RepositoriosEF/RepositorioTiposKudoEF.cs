using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace AccesoDatos.RepositoriosEF;

public class RepositorioTiposKudoEF: IRepositorioTiposKudo
{
    public Task<Resultado> AddAsync(TipoKudo unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(TipoKudo unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> UpdateAsync(TipoKudo unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<TipoKudo>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<IEnumerable<TipoKudo>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
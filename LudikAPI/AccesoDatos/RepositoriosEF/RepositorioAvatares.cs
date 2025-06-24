using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace AccesoDatos.RepositoriosEF;

public class RepositorioAvatares: IRepositorioAvatares
{
    public Task<Resultado> AddAsync(Avatar unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(Avatar unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> UpdateAsync(Avatar unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<Avatar>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<IEnumerable<Avatar>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<Avatar>> GetByPerfilIdAsync(int idPerfilEstudiante)
    {
        throw new NotImplementedException();
    }
}
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF;

public class RepositorioRecompensasDeProfesoresEF: IRepositorioRecompensasDeProfesores
{
    private readonly ContextoDb _db;
    public RepositorioRecompensasDeProfesoresEF(ContextoDb db)
    {
        _db = db;
    }
    public Task<Resultado> AddAsync(ProfesorRecompensa unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> RemoveAsync(ProfesorRecompensa unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado> UpdateAsync(ProfesorRecompensa unObjeto)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<ProfesorRecompensa>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Resultado<IEnumerable<ProfesorRecompensa>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Resultado<IEnumerable<ProfesorRecompensa>>> GetByProfesorIdAsync(string profesorId)
    {
        try
        {
            var listaRecompensasDelProfesor = await _db.RecompensasDeProfesores
                .Include(pr => pr.Recompensa) 
                .Where(pr => pr.ProfesorId == profesorId)
                .ToListAsync();

            return Resultado<IEnumerable<ProfesorRecompensa>>.Exitoso(listaRecompensasDelProfesor);
        }
        catch (Exception e)
        {
            return Resultado<IEnumerable<ProfesorRecompensa>>.Falla(new Error("Error.Unexpected",
                "Ha ocurrido un error en la base de datos. Error: " + e.Message));
        }
    }
}
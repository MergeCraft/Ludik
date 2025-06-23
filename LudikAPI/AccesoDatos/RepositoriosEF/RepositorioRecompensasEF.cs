using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioRecompensasEF : IRepositorioRecompensas
    {
        private readonly ContextoDb _db;
        public RepositorioRecompensasEF(ContextoDb db)
        {
            _db = db;
        }

        public async Task<Resultado> AddAsync(Recompensa unObjeto)
        {
            try
            {
                // Si la entidad Recompensa tiene navegación a Tienda ya asignada, simplemente:
                await _db.Recompensas.AddAsync(unObjeto);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (Exception ex)
            {
                return Resultado.Falla(new Error("Error.Unexpected", ex.Message));
            }
        }

        public Task<Resultado<IEnumerable<Recompensa>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<Recompensa>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioRecompensasEF : IRepositorioRecompensas
    {
        private readonly ContextoDb _db;
        public RepositorioRecompensasEF(ContextoDb db)
        {
            _db = db;
        }

        public Task AddAsync(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recompensa>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Recompensa> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

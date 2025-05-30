using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioHitosEF : IRepositorioHitos
    {
        private readonly ContextoDb _db;
        public RepositorioHitosEF(ContextoDb db)
        {
            _db = db;
        }

        public Task AddAsync(Hito unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Hito>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hito> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Hito unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Hito unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

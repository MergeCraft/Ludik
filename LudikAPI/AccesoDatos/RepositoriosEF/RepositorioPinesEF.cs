using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioPinesEF : IRepositorioPines
    {
        private readonly ContextoDb _db;
        public RepositorioPinesEF(ContextoDb db)
        {
            _db = db;
        }

        public Task AddAsync(Pin unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pin>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pin> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Pin unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Pin unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

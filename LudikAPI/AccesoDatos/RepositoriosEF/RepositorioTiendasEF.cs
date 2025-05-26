using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTiendasEF : IRepositorioTiendas
    {
        private readonly Context _db;
        public RepositorioTiendasEF(Context db)
        {
            _db = db;
        }

        public Task AddAsync(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tienda>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tienda> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

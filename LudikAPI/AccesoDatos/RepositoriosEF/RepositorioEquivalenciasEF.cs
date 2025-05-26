using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioEquivalenciasEF : IRepositorioEquivalencias
    {
        private readonly Context _db;
        public RepositorioEquivalenciasEF(Context db)
        {
            _db = db;
        }

        public Task AddAsync(Equivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Equivalencia>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Equivalencia> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Equivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Equivalencia unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

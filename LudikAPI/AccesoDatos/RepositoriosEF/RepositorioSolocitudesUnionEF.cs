using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioSolocitudesUnionEF : IRepositorioSolicitudesUnion
    {
        private readonly Context _db;
        public RepositorioSolocitudesUnionEF(Context db)
        {
            _db = db;
        }

        public Task AddAsync(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SolicitudUnion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SolicitudUnion> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTiendasEF : IRepositorioTiendas
    {
        private readonly ContextoDb _db;
        public RepositorioTiendasEF(ContextoDb db)
        {
            _db = db;
        }

        public Task<Resultado> AddAsync(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<Tienda>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<Tienda>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

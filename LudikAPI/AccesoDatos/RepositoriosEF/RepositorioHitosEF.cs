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
    public class RepositorioHitosEF : IRepositorioHitos
    {
        private readonly ContextoDb _db;
        public RepositorioHitosEF(ContextoDb db)
        {
            _db = db;
        }

        public Task<Resultado> AddAsync(Hito unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<Hito>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<Hito>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(Hito unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(Hito unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

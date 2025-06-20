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
    public class RepositorioPinesEF : IRepositorioPines
    {
        private readonly ContextoDb _db;
        public RepositorioPinesEF(ContextoDb db)
        {
            _db = db;
        }

        public Task<Resultado> AddAsync(Pin unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<Pin>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<Pin>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(Pin unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(Pin unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

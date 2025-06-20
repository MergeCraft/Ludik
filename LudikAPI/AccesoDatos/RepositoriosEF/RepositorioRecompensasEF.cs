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

        public Task<Resultado> AddAsync(Recompensa unObjeto)
        {
            throw new NotImplementedException();
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

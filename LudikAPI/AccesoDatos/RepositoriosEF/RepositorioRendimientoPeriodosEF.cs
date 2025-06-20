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
    public class RepositorioRendimientoPeriodosEF : IRepositorioRendimientoPeriodos
    {
        private readonly ContextoDb _db;
        public RepositorioRendimientoPeriodosEF(ContextoDb db)
        {
            _db = db;
        }

        public Task<Resultado> AddAsync(RendimientoPeriodo unObjeto)
        {
            throw new NotImplementedException();
        }

        public void almacenarLogrosPrevios(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<RendimientoPeriodo>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<RendimientoPeriodo>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(RendimientoPeriodo unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(RendimientoPeriodo unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

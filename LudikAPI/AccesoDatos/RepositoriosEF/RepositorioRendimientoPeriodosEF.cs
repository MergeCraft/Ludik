using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioRendimientoPeriodosEF : IRepositorioRendimientoPeriodos
    {
        private readonly Context _db;
        public RepositorioRendimientoPeriodosEF(Context db)
        {
            _db = db;
        }

        public Task AddAsync(RendimientoPeriodo unObjeto)
        {
            throw new NotImplementedException();
        }

        public void almacenarLogrosPrevios(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RendimientoPeriodo>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RendimientoPeriodo> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(RendimientoPeriodo unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RendimientoPeriodo unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

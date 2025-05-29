using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTablasEquivalenciaEF : IRepositorioTablasEquivalencia
    {
        private readonly ContextoDb _db;
        public RepositorioTablasEquivalenciaEF(ContextoDb db)
        {
            _db = db;
        }

        public Task AddAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TablaEquivalencia>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       
        public async Task<TablaEquivalencia> GetByIdAsync(int id)
        {
            return await _db.TablasEquivalencia.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

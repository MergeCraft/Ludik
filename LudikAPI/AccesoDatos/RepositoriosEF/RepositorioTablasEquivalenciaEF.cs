using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Resultados;
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

        public Task<Resultado> AddAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<TablaEquivalencia>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       
        public async Task<Resultado<TablaEquivalencia>> GetByIdAsync(int id)
        {
            return await _db.TablasEquivalencia.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

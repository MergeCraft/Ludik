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
    public class RepositorioEnlacesUnionGrupoEF : IRepositorioEnlacesUnionGrupo
    {
        private readonly ContextoDb _db;
        public RepositorioEnlacesUnionGrupoEF(ContextoDb db)
        {
            _db = db;
        }
        public Task AddAsync(EnlaceUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExisteSolicitudPendiente(int idEstudiante, int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnlaceUnion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EnlaceUnion> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public string obtenerCodigoInvitacion(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public async Task<EnlaceUnion> ObtenerPorCodigoAsync(string codigo)
        {
            return await _db.EnlacesUnion
                     .FirstOrDefaultAsync(e => e.codigoBase == codigo);

        }   

        public Task<Grupo> ObtenerPorEnlaceAsync(string codigoBase)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(EnlaceUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EnlaceUnion unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

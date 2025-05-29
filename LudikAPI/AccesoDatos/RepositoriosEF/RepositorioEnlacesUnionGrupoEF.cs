using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioEnlacesUnionGrupoEF : IRepositorioEnlacesUnionGrupo
    {
        public Task AddAsync(EnlaceUnion unObjeto)
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

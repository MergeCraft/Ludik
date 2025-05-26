using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioPreguntasSeguridadEF : IRepositorioPreguntasSeguridad
    {
        private readonly Context _db;
        public RepositorioPreguntasSeguridadEF(Context db)
        {
            _db = db;
        }

        public Task AddAsync(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PreguntaRespuestaSeguridad>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PreguntaRespuestaSeguridad> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

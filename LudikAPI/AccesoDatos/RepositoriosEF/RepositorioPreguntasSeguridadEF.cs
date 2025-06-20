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
    public class RepositorioPreguntasSeguridadEF : IRepositorioPreguntasSeguridad
    {
        private readonly ContextoDb _db;
        public RepositorioPreguntasSeguridadEF(ContextoDb db)
        {
            _db = db;
        }

        public Task<Resultado> AddAsync(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<PreguntaRespuestaSeguridad>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<PreguntaRespuestaSeguridad>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

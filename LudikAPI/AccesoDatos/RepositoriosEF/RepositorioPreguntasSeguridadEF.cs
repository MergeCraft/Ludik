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
        private readonly ContextoDb _db;
        public RepositorioPreguntasSeguridadEF(ContextoDb db)
        {
            _db = db;
        }
        public void Add(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PreguntaRespuestaSeguridad> GetAll()
        {
            throw new NotImplementedException();
        }

        public PreguntaRespuestaSeguridad GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(PreguntaRespuestaSeguridad unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

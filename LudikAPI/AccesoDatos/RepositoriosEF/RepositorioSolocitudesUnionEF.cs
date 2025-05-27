using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioSolocitudesUnionEF : IRepositorioSolicitudesUnion
    {
        private readonly ContextoDb _db;
        public RepositorioSolocitudesUnionEF(ContextoDb db)
        {
            _db = db;
        }
        public void Add(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitudUnion> GetAll()
        {
            throw new NotImplementedException();
        }

        public SolicitudUnion GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(SolicitudUnion unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

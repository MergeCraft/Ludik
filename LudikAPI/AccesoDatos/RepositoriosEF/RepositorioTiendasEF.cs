using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTiendasEF : IRepositorioTiendas
    {
        private readonly ContextoDb _db;
        public RepositorioTiendasEF(ContextoDb db)
        {
            _db = db;
        }
        public void Add(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tienda> GetAll()
        {
            throw new NotImplementedException();
        }

        public Tienda GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Tienda unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

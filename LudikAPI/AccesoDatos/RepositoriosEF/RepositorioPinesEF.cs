using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioPinesEF : IRepositorioPines
    {
        private readonly Context _db;
        public RepositorioPinesEF(Context db)
        {
            _db = db;
        }
        public void Add(Pin unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pin> GetAll()
        {
            throw new NotImplementedException();
        }

        public Pin GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Pin unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Pin unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

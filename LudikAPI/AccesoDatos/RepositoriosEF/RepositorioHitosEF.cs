using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioHitosEF : IRepositorioHitos
    {
        private readonly Context _db;
        public RepositorioHitosEF(Context db)
        {
            _db = db;
        }
        public void Add(Hito unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hito> GetAll()
        {
            throw new NotImplementedException();
        }

        public Hito GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hito unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Hito unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioRecompensasEF : IRepositorioRecompensas
    {
        private readonly Context _db;
        public RepositorioRecompensasEF(Context db)
        {
            _db = db;
        }
        public void Add(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recompensa> GetAll()
        {
            throw new NotImplementedException();
        }

        public Recompensa GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Recompensa unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

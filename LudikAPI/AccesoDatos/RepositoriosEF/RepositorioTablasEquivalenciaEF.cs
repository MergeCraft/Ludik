using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTablasEquivalenciaEF : IRepositorioTablasEquivalencia
    {
        private readonly Context _db;
        public RepositorioTablasEquivalenciaEF()
        {
            _db = new Context();
        }
        public void Add(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TablaEquivalencia> GetAll()
        {
            throw new NotImplementedException();
        }

        public TablaEquivalencia GetById(int id)
        {
            return _db.TablasEquivalencia.FirstOrDefault(t => t.Id == id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

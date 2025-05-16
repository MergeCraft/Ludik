using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioMedallasEF : IRepositorioMedallas
    {
        public void Add(Medalla unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medalla> GetAll()
        {
            throw new NotImplementedException();
        }

        public Medalla GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> obtenerMedallasAsignablesMutuamente(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Medalla unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(Medalla unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

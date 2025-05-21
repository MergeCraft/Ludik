using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioMedallasEF : IRepositorioMedallas
    {
        private readonly Context _db;
        public RepositorioMedallasEF()
        {
            _db = new Context();
        }
        public void Add(Medalla unaMedalla)
        {
            try
            {
                unaMedalla.EsValido();
                _db.Medallas.Add(unaMedalla);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MedallaNoValidaException(e.Message);
            }
        }

        public IEnumerable<Medalla> GetAll()
        {
            throw new NotImplementedException();
        }

        public Medalla GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> ObtenerMedallasAsignablesMutuamente(int idGrupo)
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

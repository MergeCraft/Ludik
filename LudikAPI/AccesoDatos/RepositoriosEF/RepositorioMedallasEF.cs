using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioMedallasEF : IRepositorioMedallas
    {
        private readonly ContextoDb _db;
        public RepositorioMedallasEF(ContextoDb db)
        {
            _db = db;
        }
        public void Add(Medalla unaMedalla)
        {
            try
            {
                unaMedalla.EsValido();
                _db.Medallas.Add(unaMedalla);
                _db.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Si tiene InnerException, muestra el detalle en la respuesta HTTP
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new MedallaNoValidaException($"Error al guardar en la BD: {detalle}");
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

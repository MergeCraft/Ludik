using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioProfesoresEF : IRepositorioProfesores
    {
        private readonly ContextoDb _db;
        public RepositorioProfesoresEF(ContextoDb db)
        {
            _db = db;
        }
        public async Task AddAsync(Profesor profesorNuevo)
        {
            try
            {
                if (profesorNuevo == null)
                {
                    throw new UsuarioNoValidoException();
                }

                _db.Profesores.Add(profesorNuevo);
                await _db.SaveChangesAsync();
            }
            catch (UsuarioNoValidoException)
            {
                throw new UsuarioNoValidoException("El Usuario no es valido.");
            }
        }


        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Profesor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(Profesor unObjeto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Profesor unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

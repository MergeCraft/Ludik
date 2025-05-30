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

            if (profesorNuevo == null)
                throw new UsuarioNoValidoException();
            
            await _db.Profesores.AddAsync(profesorNuevo);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new UsuarioNoValidoException(
                    "Error al persistir el profesor en la base de datos. " + ex.Message
                );
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

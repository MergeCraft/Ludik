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
            //Todo: eliminar metodo si se comprueba que no se usa este codigo
            /*
            if (profesorNuevo == null)
                throw new UsuarioNoValidoException();
            
            await _db.Profesores.AddAsync(profesorNuevo);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var sqlMsg = ex.InnerException?.Message ?? ex.Message;
                throw new UsuarioNoValidoException("Error al intentar guardar los datos en la base de datos. " + sqlMsg);
            }
            */
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

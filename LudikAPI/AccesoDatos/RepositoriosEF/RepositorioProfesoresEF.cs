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
        private readonly Context _db;
        public RepositorioProfesoresEF(Context db)
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

                await _db.Profesores.AddAsync(profesorNuevo);
                await _db.SaveChangesAsync();
            }
            catch (UsuarioNoValidoException)
            {
                throw new UsuarioNoValidoException("El Usuario no es valido.");
            }
        }

        public async Task<bool> ExisiteMailProfesorAsync(string emailUsuario)
        {
            return await _db.Profesores
                .OfType<Profesor>()
                .AnyAsync(p => p.email.Valor == emailUsuario);
        }

        public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
        {
            return await _db.Profesores
                .AnyAsync(u => u.NombreUsuario.Valor == nombreUsuario);
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

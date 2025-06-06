using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
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
        public async Task<Resultado> AddAsync(Profesor profesorNuevo)
        {
            try
            {
                // Placeholder
                await Task.CompletedTask;
                return Resultado.Falla(new Error("Repositorio.Estudiante.NoImplementado", "Sin implementar"));
            }
            catch (Exception e)
            {
                return Resultado.Falla(Error.Unexpected);
            }
        }


        public async Task<Resultado<IEnumerable<Profesor>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado<Profesor>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado> RemoveAsync(Profesor unObjeto)
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado> UpdateAsync(Profesor unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

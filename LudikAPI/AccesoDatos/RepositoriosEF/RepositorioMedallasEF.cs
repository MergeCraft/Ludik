using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.Resultados;
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
        public async Task AddAsync(Medalla unaMedalla)
        {
            try
            {
                unaMedalla.EsValido();
                await _db.Medallas.AddAsync(unaMedalla);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new MedallaNoValidaException($"Error al guardar en la BD: {detalle}");
            }
            catch (Exception e)
            {
                throw new MedallaNoValidaException(e.Message);
            }
        }

        public async Task<IEnumerable<Medalla>> GetAllAsync()
        {
            throw new NotImplementedException("Este método aún no está implementado en el repositorio de Medallas.");

        }

        public async Task<Medalla> GetByIdAsync(int id)
        {
            throw new NotImplementedException("Este método aún no está implementado en el repositorio de Medallas.");

        }


        public async Task RemoveAsync(int id)
        {
            throw new NotImplementedException("Este método aún no está implementado en el repositorio de Medallas.");

        }

        public async Task RemoveAsync(Medalla unObjeto)
        {
            throw new NotImplementedException("Este método aún no está implementado en el repositorio de Medallas.");

        }

        public async Task UpdateAsync(Medalla unObjeto)
        {
            throw new NotImplementedException("Este método aún no está implementado en el repositorio de Medallas.");

        }

        public async Task<List<Medalla>> ObtenerMedallasAsignablesMutuamenteAsync(int idGrupo)
        {
            throw new NotImplementedException("Este método aún no está implementado en el repositorio de Medallas.");
        }
    }
}

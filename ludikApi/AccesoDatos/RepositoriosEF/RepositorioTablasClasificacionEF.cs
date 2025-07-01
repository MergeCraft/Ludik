using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTablasClasificacionEF : IRepositorioTablasClasificacion
    {
        private readonly ContextoDb _db;
        public RepositorioTablasClasificacionEF(ContextoDb db)
        {
            _db = db;
        }

        public async Task<Resultado> AddAsync(TablaClasificacion tabla)
        {
            try
            {
                await _db.TablasClasificacion.AddAsync(tabla);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado.Falla(new Error("Error.Unexpected", $"Error al guardar la tabla: {detalle}"));
            }
            
        }

        public Task<Resultado<IEnumerable<TablaClasificacion>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Resultado<TablaClasificacion>> GetByIdAsync(int id)
        {
            try
            {
                var tabla = await _db.TablasClasificacion
                    .Include(tc => tc.MedallaAsociada)
                    .Include(tc => tc.Participantes)
                        .ThenInclude(p => p.Estudiante)
                    .Include(tc => tc.Participantes)
                        .ThenInclude(p => p.PerfilMedallas)
                    .FirstOrDefaultAsync(tc => tc.Id == id);

                if (tabla == null)
                    return Resultado<TablaClasificacion>.Falla(new Error("NotFound", "Tabla no encontrada."));

                return Resultado<TablaClasificacion>.Exitoso(tabla);
            }
            catch (Exception ex)
            {
                return Resultado<TablaClasificacion>.Falla(new Error("Error.DB", ex.Message));
            }
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(TablaClasificacion unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(TablaClasificacion unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

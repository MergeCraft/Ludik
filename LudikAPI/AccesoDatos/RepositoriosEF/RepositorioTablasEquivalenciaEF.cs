using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Resultados;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioTablasEquivalenciaEF : IRepositorioTablasEquivalencia
    {
        private readonly ContextoDb _db;
        public RepositorioTablasEquivalenciaEF(ContextoDb db)
        {
            _db = db;
        }

        public async Task<Resultado> AddAsync(TablaEquivalencia tabla)
        {
            try
            {
                await _db.TablasEquivalencia.AddAsync(tabla);
                await _db.SaveChangesAsync();
                return Resultado.Exitoso();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                return Resultado.Falla(new Error("Repositorio.Tabla.Add.DbError", $"Error al guardar la tabla: {detalle}"));
            }
        }

        public Task<Resultado<IEnumerable<TablaEquivalencia>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       
        public async Task<Resultado<TablaEquivalencia>> GetByIdAsync(int id)
        {
            try
            {
                var tablaEquivalencia = await _db.TablasEquivalencia.FirstOrDefaultAsync(t => t.Id == id);

                if (tablaEquivalencia == null)
                    return Resultado<TablaEquivalencia>.Falla(Error.NotFound); 
                
                return Resultado<TablaEquivalencia>.Exitoso(tablaEquivalencia); 
            }
            catch (Exception e)
            {
                return Resultado<TablaEquivalencia>.Falla(new Error ("Unexpected", e.Message));
            }
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(TablaEquivalencia unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

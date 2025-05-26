using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioGruposEF : IRepositorioGrupos

    {
        private readonly Context _db;
        public RepositorioGruposEF(Context db)
        {
            _db = db;
        }
        public void aceptarSolicitud(SolicitudUnion idSolicitud)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(Grupo unGrupo)
        {
            try
            {
                if (unGrupo == null)
                {
                    throw new GrupoNoValidoExeption();
                }

                if (unGrupo.tablaEquivalencia != null)
                {
                    _db.Entry(unGrupo.tablaEquivalencia).State = EntityState.Unchanged;
                }
                if (unGrupo.enlaceUnion != null)
                {
                    _db.Entry(unGrupo.enlaceUnion).State = EntityState.Added;
                }
                if (unGrupo.tienda != null)
                {
                    _db.Entry(unGrupo.tienda).State = EntityState.Added;
                }

                await _db.Grupos.AddAsync(unGrupo);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new GrupoNoValidoExeption($"Error al guardar en la BD: {detalle}");
            }
            catch (GrupoNoValidoExeption)
            {
                throw new GrupoNoValidoExeption("El Grupo no es válido.");
            }
        }

        public async Task<Grupo> GetByIdAsync(int id)
        {
            return await _db.Grupos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Grupo>> GetAllAsync()
        {
            return await _db.Grupos.ToListAsync();
        }

        public async Task UpdateAsync(Grupo grupoNuevo)
        {
            try
            {
                if (grupoNuevo == null)
                {
                    throw new GrupoNoValidoExeption("El grupo no puede ser null.");
                }

                var grupoExistente = await _db.Grupos.FindAsync(grupoNuevo.Id);
                if (grupoExistente == null)
                {
                    throw new Exception("Grupo no encontrado.");
                }

                _db.Entry(grupoExistente).CurrentValues.SetValues(grupoNuevo);
                await _db.SaveChangesAsync();
            }
            catch (GrupoNoValidoExeption ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<TablaEquivalencia> obtenerTablaDelGrupoAsync(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task<int> calcularNotaEstudianteAsync(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task aceptarSolicitudAsync(SolicitudUnion idSolicitud)
        {
            throw new NotImplementedException();
        }

        public Task rechazarSolicitudAsync(SolicitudUnion idSolictud)
        {
            throw new NotImplementedException();
        }

        public Task<List<Grupo>> obtenerGruposPorProfesorAsync(int idProfesor)
        {
            throw new NotImplementedException();
        }

        public Task unirseAGrupoAsync(int idAlumno, Grupo grupo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Estudiante>> obtenerAlumnosDelGrupoAsync(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task reiniciarLogrosDeGrupoAsync(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task<List<TablaClasificacion>> obtenerTablasDeClasificacionDeGrupoAsync(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Grupo unObjeto)
        {
            throw new NotImplementedException();
        }



        // Métodos aún no implementados asincrónicamente (podemos discutir su diseño si querés)


    }
    
}

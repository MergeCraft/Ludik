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

        public void Add(Grupo unGrupo)
        {
            try
            {
                if (unGrupo == null)
                {
                    throw new GrupoNoValidoExeption();
                }
                if (unGrupo.TablaEquivalencia != null)
                {
                    _db.Entry(unGrupo.TablaEquivalencia).State = EntityState.Unchanged;
                }
                if (unGrupo.EnlaceUnion != null)
                {
                    _db.Entry(unGrupo.EnlaceUnion).State = EntityState.Added;
                }
                if (unGrupo.Tienda != null)
                {
                    _db.Entry(unGrupo.Tienda).State = EntityState.Added;
                }
                _db.Grupos.Add(unGrupo);
                _db.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                var detalle = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new GrupoNoValidoExeption($"Error al guardar en la BD: {detalle}");
            }
            catch (GrupoNoValidoExeption ex)
            {
                throw new GrupoNoValidoExeption("El Grupo no es válido.");
            }
        }

        public int calcularNotaEstudiante(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Grupo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Grupo GetById(int id)
        {
            return _db.Grupos.FirstOrDefault(t => t.Id == id);
        }

        public List<Estudiante> obtenerAlumnosDelGrupo(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public List<Grupo> obtenerGruposPorProfesor(int idProfesor)
        {
            throw new NotImplementedException();
        }

        public TablaEquivalencia obtenerTablaDelGrupo(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public List<TablaClasificacion> obtenerTablasDeClasificacionDeGrupo(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public void rechazarSolicitud(SolicitudUnion idSolictud)
        {
            throw new NotImplementedException();
        }

        public void reiniciarLogrosDeGrupo(int idGrupo)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Grupo unObjeto)
        {
            throw new NotImplementedException();
        }

        public void unirseAGrupo(int idAlumno, Grupo grupo)
        {
            throw new NotImplementedException();
        }

        public void Update(Grupo grupoNuevo)
        {
            try
            {
                if (grupoNuevo == null)
                {
                    throw new GrupoNoValidoExeption("El usuario no puede ser null.");
                }
               // grupoNuevo.EsValido();
                var grupoExistente = _db.Grupos.Find(grupoNuevo.Id);
                if (grupoExistente == null)
                {
                    throw new Exception("grupo no encontrado");
                }
                _db.Entry(grupoExistente).CurrentValues.SetValues(grupoNuevo);
                _db.SaveChanges();
            }
            catch (GrupoNoValidoExeption ex)
            {
                throw ex;
            }
        }
    }
}

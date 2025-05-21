using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Excepciones;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioGruposEF : IRepositorioGrupos

    {
        private readonly Context _db;
        public RepositorioGruposEF()
        {
            _db = new Context();
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

                unGrupo.EsValido();
                _db.Grupos.Add(unGrupo);
                _db.SaveChanges();
            }
            catch (GrupoNoValidoExeption ex)
            {
                throw new GrupoNoValidoExeption("El Usuario no es valido.");
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
            throw new NotImplementedException();
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

        public void Update(Grupo unObjeto)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioPerfilEstudianteGrupoEF : IRepositorioPerfilEstudianteGrupo
    {
        private readonly Context _db;
        public RepositorioPerfilEstudianteGrupoEF(Context db)
        {
            _db = db;
        }
        public void Add(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PerfilEstudiante> GetAll()
        {
            throw new NotImplementedException();
        }

        public PerfilEstudiante GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public void Update(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }
    }
}

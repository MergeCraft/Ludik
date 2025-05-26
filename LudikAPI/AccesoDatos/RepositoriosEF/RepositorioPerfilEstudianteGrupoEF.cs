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

        public Task AddAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PerfilEstudiante>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PerfilEstudiante> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }
    }
}

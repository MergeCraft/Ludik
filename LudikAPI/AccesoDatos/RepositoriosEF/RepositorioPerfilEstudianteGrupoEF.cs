using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace AccesoDatos.RepositoriosEF
{
    public class RepositorioPerfilEstudianteGrupoEF : IRepositorioPerfilEstudianteGrupo
    {
        private readonly ContextoDb _db;
        public RepositorioPerfilEstudianteGrupoEF(ContextoDb db)
        {
            _db = db;
        }

        public Task<Resultado> AddAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<IEnumerable<PerfilEstudiante>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Resultado<PerfilEstudiante>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> RemoveAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> UpdateAsync(PerfilEstudiante unObjeto)
        {
            throw new NotImplementedException();
        }

        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo)
        {
            throw new NotImplementedException();
        }
    }
}

using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public class IRepositorioPerfilEstudianteGrupo : IRepositorio<PerfilEstudiante>
	{
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
			return null;
		}

	}

}


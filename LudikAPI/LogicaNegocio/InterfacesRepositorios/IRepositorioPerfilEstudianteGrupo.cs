using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioPerfilEstudianteGrupo : IRepositorio<PerfilEstudiante>
	{
        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo);

	}

}


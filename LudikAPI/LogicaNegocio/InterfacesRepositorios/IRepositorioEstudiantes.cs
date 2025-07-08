using InterfacesRepositorio;
using System.Collections.Generic;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioEstudiantes : IRepositorio<Estudiante>
	{


		List<Medalla> getMedallasAlumno(int idAlumno, int idGrupo);
		Task<Resultado<Estudiante>> GetByStringIdAsync(string id);
		


    }

}


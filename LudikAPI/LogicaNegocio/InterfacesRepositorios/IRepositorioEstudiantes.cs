using InterfacesRepositorio;
using System.Collections.Generic;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioEstudiantes : IRepositorio<Estudiante>
	{
		Task<Resultado> asignarMedalla(int idAlumno, int idMedalla);

        Task<Resultado> asignarMedallaEntreAlumnos(int idAlumnoOrigen, int idAlumnoDestino, int idMedalla);

        Task<Resultado> quitarMedalla(int idAlumno, int idMedalla);

		List<Medalla> getMedallasAlumno(int idAlumno, int idGrupo);
		Task<Estudiante> GetByIdAsyncString(string id);


    }

}


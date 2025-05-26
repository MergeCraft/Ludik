using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioEstudiantes : IRepositorio<Estudiante>
	{
		void asignarMedalla(int idAlumno, int idMedalla);

		void asignarMedallaEntreAlumnos(int idAlumnoOrigen, int idAlumnoDestino, int idMedalla);

		void quitarMedalla(int idAlumno, int idMedalla);

		List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo);
        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario);

    }

}


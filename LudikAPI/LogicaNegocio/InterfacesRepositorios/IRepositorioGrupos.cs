using InterfacesRepositorio;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioGrupos : IRepositorio<Grupo>
	{
		TablaEquivalencia obtenerTablaDelGrupo(int idGrupo);

		int calcularNotaEstudiante(int idAlumno, int idGrupo);

		void aceptarSolicitud(SolicitudUnion idSolicitud);

		void rechazarSolicitud(SolicitudUnion idSolictud);

		List<Grupo> obtenerGruposPorProfesor(int idProfesor);

		void unirseAGrupo(int idAlumno, Grupo grupo);

		List<Estudiante> obtenerAlumnosDelGrupo(int idGrupo);

		void reiniciarLogrosDeGrupo(int idGrupo);

		List<TablaClasificacion> obtenerTablasDeClasificacionDeGrupo(int idGrupo);

	}

}


using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using System.Collections.Generic;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioGrupos : IRepositorio<Grupo>
	{
        Task<TablaEquivalencia> obtenerTablaDelGrupoAsync(int idGrupo);

        Task<int> calcularNotaEstudianteAsync(int idAlumno, int idGrupo);

        Task aceptarSolicitudAsync(SolicitudUnion idSolicitud);

        Task rechazarSolicitudAsync(SolicitudUnion idSolictud);

        Task<Resultado<IEnumerable<Grupo>>> obtenerGruposPorProfesorAsync(string idProfesor);

        Task unirseAGrupoAsync(int idAlumno, Grupo grupo);

        Task<List<Estudiante>> obtenerAlumnosDelGrupoAsync(int idGrupo);

        Task reiniciarLogrosDeGrupoAsync(int idGrupo);

        Task<List<TablaClasificacion>> obtenerTablasDeClasificacionDeGrupoAsync(int idGrupo);
        Task<Grupo> ObtenerPorEnlaceAsync(string codigoBase);

        Task<List<Grupo>> ObtenerGruposPorEstudianteId(string idEstudiante);
        Task<List<Grupo>> ObtenerGruposPorProfesorId(string idProfesor);

    }

}


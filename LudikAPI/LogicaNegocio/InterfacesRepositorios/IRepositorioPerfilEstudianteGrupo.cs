using InterfacesRepositorio;
using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioPerfilEstudianteGrupo : IRepositorio<PerfilEstudiante>
	{
        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo);
        public Task<Resultado<List<PerfilEstudiante>>> ObtenerPorGrupoIdAsync(int grupoId);


    }

}


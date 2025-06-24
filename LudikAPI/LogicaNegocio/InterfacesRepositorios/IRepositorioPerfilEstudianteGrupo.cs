using InterfacesRepositorio;
using System.Collections.Generic;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace InterfacesRepositorio
{
	public interface IRepositorioPerfilEstudianteGrupo : IRepositorio<PerfilEstudiante>
	{
        public List<Medalla> verMedallasAlumno(int idAlumno, int idGrupo);
        public Task<Resultado<List<PerfilEstudiante>>> ObtenerPorGrupoIdAsync(int grupoId);

        Task<Resultado<PerfilEstudiante>> GetByEstudianteYGrupoConMedallasAsync(string estudianteId, int grupoId);
        public Task<Resultado<IEnumerable<Recompensa>>> ObtenerItemsAvatarAdquiridosAsync(int idPerfilEstudiante);



    }

}


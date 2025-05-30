using Dominio;
using InterfacesRepositorio;
using LogicaNegocio.InterfacesRepositorio;

namespace InterfacesRepositorio
{
	public interface IRepositorioSolicitudesUnion : IRepositorio<SolicitudUnion>
	{
        Task<bool> ExisteSolicitudPendiente(string idEstudiante, int idGrupo);
    }

}


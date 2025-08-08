using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.InterfacesRepositorios;

public interface IRepositorioRecompensasDeProfesores: IRepositorio<ProfesorRecompensa>
{
    Task<Resultado<IEnumerable<ProfesorRecompensa>>> GetByProfesorIdAsync(string profesorId);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioPerfilEstudianteMedalla : IRepositorio<PerfilEstudianteMedalla>
    {
        Task<Resultado> RemoveByIdAsync(int id);
        Task<Resultado<PerfilEstudianteMedalla>> GetByPerfilYMedallaAsync(int perfilId, int medallaId);
    }
}

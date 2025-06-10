using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class Hito : IEntity, IValidable
    {
        public int Id { get; set; }

        public int CantMedallasRequeridas { get; set; }

        public Recompensa Recompensa { get; set; }

        public bool cumple(int cantMedallasPerfiles)
		{
			return true;
		}

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}


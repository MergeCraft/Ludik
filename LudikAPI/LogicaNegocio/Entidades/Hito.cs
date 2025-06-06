using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Hito : IEntity, IValidable
    {
        public int Id { get; set; }

        public int cantMedallasRequeridas { get; set; }

        public Recompensa recompensa { get; set; }

        public bool cumple(int cantMedallasPerfiles)
		{
			return true;
		}

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


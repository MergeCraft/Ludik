using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Hito : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int cantMedallasRequeridas;

        public Recompensa recompensa;

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


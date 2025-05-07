using System.Collections.Generic;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Tienda : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Recompensa> Recompesas;

        public Recompensa[] recompensa;

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


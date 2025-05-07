using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class SolicitudUnion : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Estudiante estudiante;

        public Estudiante estudiante2;
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Hito : IEntity, IValidable
    {
        public int Id { get; set; }

        public int CantMedallasRequeridas { get; set; }

        public Recompensa Recompensa { get; set; }

        public bool Otorgado { get; set; }

        public bool Cumple(int totalMedallas) => totalMedallas >= CantMedallasRequeridas;

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}


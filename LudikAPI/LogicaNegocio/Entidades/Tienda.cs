using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class Tienda : IEntity, IValidable
    {
        public int Id { get; set; }

        public List<Recompensa> Recompesas { get; set; } = new();

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }


        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}


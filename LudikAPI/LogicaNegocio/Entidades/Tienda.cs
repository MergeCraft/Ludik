using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Tienda : IEntity, IValidable
    {
        public int Id { get; set; }

        public List<Recompensa> Recompesas { get; set; }

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; } // Clave foránea

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


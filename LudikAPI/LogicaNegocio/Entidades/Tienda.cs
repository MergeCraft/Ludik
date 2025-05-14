using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class Tienda : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<Recompensa> Recompesas;

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; } // Clave foránea

        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Tienda : IEntity, IValidable
    {
        public int Id { get; set; }

        public List<Recompensa> Recompesas { get; set; }

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; }

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}


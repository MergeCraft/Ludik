using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class SolicitudUnion : IEntity, IValidable
    {
        public int Id { get; set; }

        public Estudiante estudiante { get; set; }

        public Estudiante estudiante2 { get; set; }

        [ForeignKey(nameof(Grupo))]
        public int grupoId { get; set; } // Clave foránea
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


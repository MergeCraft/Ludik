using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class SolicitudUnion : IEntity, IValidable
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Estudiante estudiante;

        public Estudiante estudiante2;

        [ForeignKey(nameof(Grupo))]
        public int grupoId { get; set; } // Clave foránea
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;

namespace Dominio
{
	public class SolicitudUnion : IEntity, IValidable
    {
        public int Id { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


using System.ComponentModel.DataAnnotations.Schema;
using Dominio;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.ValueObject;

namespace Dominio
{
	public class SolicitudUnion : IEntity, IValidable
    {
        public int Id { get; set; }

        public string estudianteId { get; set; }
        public Estudiante estudiante { get; set; }

        public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
        public DateOnly fecha { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public SolicitudUnion()
        {
            
        }
        public void EsValido()
        {
            throw new NotImplementedException();
        }
    }

}


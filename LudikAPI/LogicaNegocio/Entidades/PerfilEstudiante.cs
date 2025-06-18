using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.Resultados;

namespace Dominio
{
	public class PerfilEstudiante : IEntity, IValidable
    {
        public int Id { get; set; }

        public int AvatarGrupoId { get; set; }

        public String? EnlaceAvatar { get; set; }

        public int MetaCalificacion { get; set; }

        public String EstudianteId { get; set; }

        public int Monedas { get; set; }
        public List<PerfilEstudianteMedalla> PerfilMedallas { get; set; } = new();
        // (Opcional) Para acceso directo a Medalla:
        [NotMapped]
        public IEnumerable<Medalla> MedallasObtenidas => PerfilMedallas.Select(pm => pm.Medalla);

        public List<RendimientoPeriodo> HistorialRendimientoPeriodos { get; set; }

        [ForeignKey(nameof(Grupo))]
        public int GrupoId { get; set; }

        public List<Recompensa> Inventario { get; set; }

        public BarraProgreso BarraProgreso { get; set; }


        public void asignarMedalla(Medalla m) { }

        public void quitarMedalla(Medalla m) { }

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
       

	}

}


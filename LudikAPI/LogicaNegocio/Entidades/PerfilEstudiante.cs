using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using LogicaNegocio.Resultados;
using LogicaNegocio.Entidades;

namespace Dominio
{
	public class PerfilEstudiante : IEntity, IValidable
    {
        public int Id { get; set; }

        public Avatar Avatar { get; private set; }

        public int MetaCalificacion { get; set; }

        public String EstudianteId { get; set; }

        [ForeignKey(nameof(EstudianteId))]
        public Estudiante Estudiante { get; set; }

        public int Monedas { get; set; }
        public string RutaImagenCompleta { get; set; }
        public string RutaImagenMiniatura { get; set; }

        public List<Medalla> MedallasObtenidas { get; set; }
        public List<PerfilEstudianteMedalla> PerfilMedallas { get; set; } = new();
        // (Opcional) Para acceso directo a Medalla:
        [NotMapped]
        public IEnumerable<Medalla> MedallasObtenidas => PerfilMedallas.Select(pm => pm.Medalla);

        public List<RendimientoPeriodo> HistorialRendimientoPeriodos { get; set; }

        public int GrupoId { get; set; }
        [ForeignKey(nameof(GrupoId))]
        public Grupo Grupo { get; set; }

        public List<Recompensa> Inventario { get; set; }

        public BarraProgreso BarraProgreso { get; set; }



        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
       

	}

}


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
        public string RutaImagenCompleta { get; set; }
        public string RutaImagenMiniatura { get; set; }

        public List<Medalla> MedallasObtenidas { get; set; }

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


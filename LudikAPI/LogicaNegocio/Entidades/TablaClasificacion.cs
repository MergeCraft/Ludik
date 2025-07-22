
using System;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class TablaClasificacion : IEntity, IValidable
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la Tabla Clasificacion  debe tener entre 3 y 50 caracteres.")]
        public String Nombre { get; set; }
        public int MedallaAsociadaId { get; set; }
        public Medalla MedallaAsociada { get; set; }

        public List<PerfilEstudiante> Participantes { get; set; }

        public int GrupoId { get; set; } 
        public Grupo Grupo { get; set; }


        public void actualizar()
		{

		}

        public void OrdenarParticipantesPorMedallaAsociada()
        {
            if (Participantes == null)
                return;

            foreach (var p in Participantes)
            {
                if (p.MedallasObtenidas == null)
                    p.MedallasObtenidas = new List<PerfilEstudianteMedalla>();
            }

            Participantes = Participantes
                .OrderByDescending(p =>
                    p.MedallasObtenidas.Count(pm => pm.MedallaId == MedallaAsociadaId)
                )
                .ToList();
        }
        public Resultado esValido()
        {
            var errores = new List<Error>();

            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3 || Nombre.Length > 50)
                errores.Add(new Error("Error.Validation", "El nombre debe tener entre 3 y 50 caracteres."));

            if (MedallaAsociadaId <= 0)
                errores.Add(new Error("Error.Validation", "Debe seleccionarse una medalla."));

            if (GrupoId <= 0)
                errores.Add(new Error("Error.Validation", "La tabla debe pertenecer a un grupo."));

            return errores.Any()
                ? Resultado.Falla(errores)
                : Resultado.Exitoso();
        }
    }

}


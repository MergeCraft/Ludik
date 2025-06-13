
using System;
using Dominio;
using System.Collections.Generic;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LogicaNegocio.Resultados;

namespace Dominio
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

        public void actualizar()
		{

		}

        public Resultado esValido()
        {
            throw new NotImplementedException();
        }
    }

}


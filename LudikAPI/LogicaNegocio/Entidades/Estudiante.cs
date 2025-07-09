using System.Collections.Generic;
using System;

namespace LogicaNegocio.Entidades
{
	public class Estudiante: Usuario
	{
        public List<PerfilEstudiante> Perfiles { get; set; }

        public List<Hito> Hitos { get; set; }

        public List<PreguntaRespuestaSeguridad> PreguntasSeguridad { get; set; }
        

        public Boolean constrastarRespuestas(PreguntaRespuestaSeguridad pRS)
		{
			return true;
		}

        public int ContarCantidadMedallasTotales()
        {
            if (Perfiles == null || !Perfiles.Any())
                return 0;

            return Perfiles.Sum(perfil => perfil.PerfilMedallas?.Count ?? 0);
        }


    }

}


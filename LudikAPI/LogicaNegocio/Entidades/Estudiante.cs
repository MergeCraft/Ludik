using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;

namespace LogicaNegocio.Entidades
{
	public class Estudiante: Usuario
	{
        public List<PerfilEstudiante> Perfiles { get; set; }

        public List<Hito> Hitos { get; set; }

        public List<PreguntaRespuestaSeguridad> PreguntasSeguridad { get; set; }


        public bool CoincidenLasRespuestas(List<PreguntaRespuestaSeguridad> respuestasIngresadas, IPasswordHasher<Usuario> hasher)
        {
            if (respuestasIngresadas == null || this.PreguntasSeguridad == null) return false;

            // Comprueba que se haya enviado el mismo número de respuestas que las almacenadas.
            if (respuestasIngresadas.Count != this.PreguntasSeguridad.Count) return false;

            foreach (var respuestaIngresada in respuestasIngresadas)
            {
                //Todo: hay que cargar el id de la preguntaRespuestaSeguridad
                var preguntaAlmacenada = this.PreguntasSeguridad.FirstOrDefault(p => p.Id == respuestaIngresada.Id);

                if (preguntaAlmacenada == null) return false;

                // verificar la respuesta ingresada contra la respuesta hasheada almacenada.
                var resultadoVerificacion = hasher.VerifyHashedPassword(this, preguntaAlmacenada.Respuesta, respuestaIngresada.Respuesta);

                if (resultadoVerificacion == PasswordVerificationResult.Failed)
                    return false;
                
            }

            // Si todas las respuestas coinciden, la validación es exitosa
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


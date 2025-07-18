using System;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Potenciador : Recompensa
	{
        public DateTime FechaActivacion { get; set; }

        public TimeSpan Duracion { get; set; }

        public double Multiplicador { get; set; } = 2.0; 

        public bool EstaActivo =>
            DateTime.UtcNow >= FechaActivacion && DateTime.UtcNow <= FechaActivacion + Duracion;

        public override Resultado Otorgar(PerfilEstudiante perfil){

            perfil.ActivarPotenciador(this);
            return Resultado.Exitoso();
        }
    }

}


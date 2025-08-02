using System;
using LogicaNegocio.EntidadesAuxiliares;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Potenciador : Recompensa
	{
        public DateTime FechaActivacion { get; set; }

        public TimeSpan Duracion { get; set; }

        public double Multiplicador { get; set; } 

        public bool EstaActivo =>
            DateTime.UtcNow >= FechaActivacion && DateTime.UtcNow <= FechaActivacion + Duracion;

        public Potenciador()
        {
            Representacion = new RepresentacionImagen();
        }

        public override Resultado Otorgar(PerfilEstudiante perfil)
        {

            perfil.ActivarPotenciador(this);
            return Resultado.Exitoso();
        }
    }

}


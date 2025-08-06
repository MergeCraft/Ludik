using System;
using LogicaNegocio.EntidadesAuxiliares;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Potenciador : Recompensa
	{
       

        public TimeSpan Duracion { get; set; }

        public double Multiplicador { get; set; }

        public override RepresentacionVisualBase Representacion { get; protected set; }
        
        public Potenciador()
        {
            Representacion = new RepresentacionImagen();
        }

        public override Resultado Otorgar(PerfilEstudiante estudiante)
        {
            return Resultado.Exitoso();
        }
        
    }
     
}


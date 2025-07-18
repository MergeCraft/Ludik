using System;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
	public class Tema : Recompensa
	{
        public String CodigoHexadecimal { get; set; }

        public override Resultado Otorgar(PerfilEstudiante perfil)
        {
            throw new NotImplementedException();
        }
    }

}


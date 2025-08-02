using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.EntidadesAuxiliares;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
    public class RecompensaSimple : Recompensa
    {
        public override Resultado Otorgar(PerfilEstudiante perfil){
            var pr = new PerfilEstudianteRecompensa
            {
                PerfilEstudianteId = perfil.Id,
                RecompensaId = this.Id
            };
            perfil.InventarioRecompensas.Add(pr);
            return Resultado.Exitoso();
        }

        public RecompensaSimple()
        {
            Representacion = new RepresentacionIcono();
        }
    }
}

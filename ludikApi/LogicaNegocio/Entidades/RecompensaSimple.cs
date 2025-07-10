using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Resultados;

namespace LogicaNegocio.Entidades
{
    public class RecompensaSimple : Recompensa
    {
        public override Resultado Otorgar(PerfilEstudiante perfil,IRepositorioPerfilEstudianteRecompensa repoRecompensa){
            var pr = new PerfilEstudianteRecompensa
            {
                PerfilEstudianteId = perfil.Id,
                RecompensaId = this.Id
            };
            var res = repoRecompensa.AddAsync(pr).GetAwaiter().GetResult();
            if (res.EsFallo)
                return res;

            perfil.InventarioRecompensas.Add(pr);
            return Resultado.Exitoso();
        }
    }
}

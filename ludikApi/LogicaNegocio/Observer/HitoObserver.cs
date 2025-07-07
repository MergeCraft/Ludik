using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesRepositorio;
using LogicaNegocio.Entidades;
using Microsoft.Extensions.Logging;

namespace LogicaNegocio.Observer
{
    public class HitoObserver : IObserver<PerfilEstudianteMedalla>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(PerfilEstudianteMedalla value)
        {
            throw new NotImplementedException();
        }
    }
}

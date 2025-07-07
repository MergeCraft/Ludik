using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Observer
{
    public class Observable<TEvent>
    {
        private readonly List<IObserver<TEvent>> _observers = new();

        
        public void Subscribe(IObserver<TEvent> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        
        public void Unsubscribe(IObserver<TEvent> observer)
            => _observers.Remove(observer);

        
        protected void Notify(TEvent evt)
        {
            foreach (var obs in _observers)
                obs.Update(evt);
        }
    }
}

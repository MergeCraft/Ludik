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
        public void Subscribe(IObserver<TEvent> o)
        { if (!_observers.Contains(o)) _observers.Add(o); }
        public void Unsubscribe(IObserver<TEvent> o)
            => _observers.Remove(o);
        protected void Notify(TEvent evt)
            => _observers.ForEach(o => o.OnNext(evt));
    }
}

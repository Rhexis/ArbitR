using System.Collections.Generic;
using System.Linq;
using ArbitR.Internal.Extensions;
using ArbitR.Pipeline;
using ArbitR.Pipeline.Read;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Internal.Pipeline
{
    internal sealed class Arbiter : IArbiter
    {
        private readonly ServiceFactory _serviceFactory;

        public Arbiter(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public void Invoke(ICommand cmd)
        {
            var handler = _serviceFactory
                .GetInstances(typeof(IHandleCommand<>).MakeGenericType(cmd.GetType()))
                .Single()
                .Unbox<WriteService>();
            handler.Handle(cmd);
        }
        
        public T Invoke<T>(IQuery<T> query)
        {
            var handler = _serviceFactory
                .GetInstances(typeof(IHandleQuery<,>).MakeGenericType(query.GetType(), typeof(T)))
                .Single()
                .Unbox<ReadService>();
            return handler.Handle(query).Unbox<T>();
        }
        
        public void Raise(IEvent eEvent)
        {
            IEnumerable<object> handlers = _serviceFactory
                .GetInstances(typeof(IHandleEvent<>).MakeGenericType(eEvent.GetType()));
            foreach (var handler in handlers)
            {
                handler.Unbox<ReadModelManager>().Handle(eEvent);
            }
        }
    }
}
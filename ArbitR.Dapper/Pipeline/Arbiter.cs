using System.Collections.Generic;
using ArbitR.Core;
using ArbitR.Core.Command;
using ArbitR.Core.Event;
using ArbitR.Core.Extensions;
using ArbitR.Core.Query;
using ArbitR.Dapper.Handlers;
using ArbitR.Dapper.Services;

namespace ArbitR.Dapper.Pipeline
{
    internal sealed class Arbiter : IArbiter
    {
        public void Invoke(ICommand command)
        {
            var handler = ServiceFactory
                .GetInstance(typeof(IHandleCommand<>).MakeGenericType(command.GetType()))
                .Unbox<WriteService>();
            handler.Handle(command);
        }
        
        public T Invoke<T>(IQuery<T> query)
        {
            var handler = ServiceFactory
                .GetInstance(typeof(IHandleQuery<,>).MakeGenericType(query.GetType(), typeof(T)))
                .Unbox<ReadService>();
            return handler.Handle(query).Unbox<T>();
        }
        
        public void Raise(IEvent eEvent)
        {
            IEnumerable<object> handlers = ServiceFactory
                .GetInstances(typeof(IHandleEvent<>).MakeGenericType(eEvent.GetType()));
            foreach (var handler in handlers)
            {
                handler.Unbox<ServiceBase>().Handle(eEvent);
            }
        }
    }
}
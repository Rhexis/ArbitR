using System.Linq;
using ArbitR.Internal.Extensions;
using ArbitR.Internal.Pipeline.Background;
using ArbitR.Pipeline;
using ArbitR.Pipeline.Read;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Internal.Pipeline
{
    internal sealed class Arbiter : IArbiter
    {
        private readonly ServiceFactory _serviceFactory;
        private readonly IEventService _eventService;

        public Arbiter(ServiceFactory serviceFactory, IEventService eventService)
        {
            _serviceFactory = serviceFactory;
            _eventService = eventService;
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
            _eventService.Handle(eEvent);
        }

        public T Begin<T>(Workflow<T> workflow)
        {
            workflow.Configure(this);
            workflow.Run();
            return workflow.GetResult();
        }
    }
}
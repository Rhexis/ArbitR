using System.Collections.Generic;
using ArbitR.Internal.Extensions;
using ArbitR.Pipeline.ReadModel;
using Microsoft.Extensions.DependencyInjection;

namespace ArbitR.Internal.Pipeline.Background
{
    internal sealed class SynchronousEventService : IEventService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SynchronousEventService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public void Handle(IEvent eEvent)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            ServiceFactory serviceFactory = scope.ServiceProvider.GetService!;
                    
            IEnumerable<object> handlers = serviceFactory
                .GetInstances(typeof(IHandleEvent<>).MakeGenericType(eEvent.GetType()));
            foreach (var handler in handlers)
            {
                handler.Unbox<ReadModelManager>().Handle(eEvent);
            }
        }
    }
}
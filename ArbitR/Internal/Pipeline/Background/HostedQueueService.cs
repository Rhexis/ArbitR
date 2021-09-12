using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArbitR.Internal.Extensions;
using ArbitR.Pipeline.ReadModel;
using Microsoft.Extensions.Hosting;

namespace ArbitR.Internal.Pipeline.Background
{
    internal sealed class HostedQueueService : BackgroundService
    {
        private readonly ServiceFactory _serviceFactory;
        private readonly IBackgroundEventQueue _eventQueue;

        public HostedQueueService(ServiceFactory serviceFactory, IBackgroundEventQueue eventQueue)
        {
            _serviceFactory = serviceFactory;
            _eventQueue = eventQueue;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    IEvent eEvent = await _eventQueue.DequeueAsync(stoppingToken);

                    IEnumerable<object> handlers = _serviceFactory
                        .GetInstances(typeof(IHandleEvent<>).MakeGenericType(eEvent.GetType()));
                    foreach (var handler in handlers)
                    {
                        handler.Unbox<ReadModelManager>().Handle(eEvent);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if stoppingToken was signaled
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
    }
}
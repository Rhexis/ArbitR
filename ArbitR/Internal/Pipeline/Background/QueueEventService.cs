using System.Threading.Tasks;
using ArbitR.Pipeline.ReadModel;

namespace ArbitR.Internal.Pipeline.Background
{
    internal sealed class QueueEventService : IEventService
    {
        private readonly IBackgroundEventQueue _eventQueue;

        public QueueEventService(IBackgroundEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public void Handle(IEvent eEvent)
        {
            Task.Run(async () => await _eventQueue.QueueBackgroundWorkItemAsync(eEvent));
        }
    }
}
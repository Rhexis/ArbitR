using System.Threading.Tasks;
using ArbitR.Pipeline.ReadModel;

namespace ArbitR.Internal.Pipeline.Background
{
    internal sealed class EventService
    {
        private readonly IBackgroundEventQueue _eventQueue;

        public EventService(IBackgroundEventQueue eventQueue)
        {
            _eventQueue = eventQueue;
        }

        public void Enqueue(IEvent eEvent)
        {
            Task.Run(async () => await _eventQueue.QueueBackgroundWorkItemAsync(eEvent));
        }
    }
}
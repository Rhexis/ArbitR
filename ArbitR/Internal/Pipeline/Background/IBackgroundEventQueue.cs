using System.Threading;
using System.Threading.Tasks;
using ArbitR.Pipeline.ReadModel;

namespace ArbitR.Internal.Pipeline.Background
{
    internal interface IBackgroundEventQueue
    {
        ValueTask QueueBackgroundWorkItemAsync(IEvent workItem);
        ValueTask<IEvent> DequeueAsync(CancellationToken cancellationToken);
    }
}
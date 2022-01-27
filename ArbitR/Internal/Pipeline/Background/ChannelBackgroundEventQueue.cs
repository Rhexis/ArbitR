using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using ArbitR.Pipeline.ReadModel;

namespace ArbitR.Internal.Pipeline.Background
{
    internal sealed class ChannelBackgroundEventQueue : IBackgroundEventQueue
    {
        private readonly Channel<IEvent> _queue;

        public ChannelBackgroundEventQueue(int capacity)
        {
            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<IEvent>(options);
        }

        public async ValueTask QueueBackgroundWorkItemAsync(IEvent workItem)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await _queue.Writer.WriteAsync(workItem);
        }

        public async ValueTask<IEvent> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
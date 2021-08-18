using ArbitR.Internal.Extensions;
using ArbitR.Pipeline.Read;

namespace ArbitR.Pipeline.ReadModel
{
    /// <summary>
    /// Used for managing read-only views built from one or more tables.
    /// Can consume Events and Queries.
    /// </summary>
    public abstract class ReadModelManager : ReadService
    {
        private bool CanHandle(IEvent eEvent)
        {
            var type = typeof(IMightHandleEvent<>).MakeGenericType(eEvent.GetType());
            if (!GetType().Implements(type)) return true;
            return Invoke(nameof(CanHandle), new object[] {eEvent}).Unbox<bool>();
        }
        
        public void Handle(IEvent eEvent)
        {
            if (!CanHandle(eEvent)) return;
            Invoke(nameof(IHandleEvent<IEvent>.Handle), new object[] {eEvent});
        }
    }
}
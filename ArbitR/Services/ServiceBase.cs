using System.Reflection;
using ArbitR.Core.Event;
using ArbitR.Core.Extensions;
using ArbitR.Handlers;

namespace ArbitR.Services
{
    public class ServiceBase : IService
    {
        internal ServiceBase()
        {
        }
        
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
        
        protected object? Invoke(string method, object[] args)
        {
            return GetType().InvokeMember
            (
                method,
                BindingFlags.InvokeMethod,
                null,
                this,
                args
            );
        }
    }
}
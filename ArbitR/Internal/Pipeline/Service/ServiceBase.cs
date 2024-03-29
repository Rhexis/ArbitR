using System.Reflection;

namespace ArbitR.Internal.Pipeline.Service
{
    public abstract class ServiceBase
    {
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
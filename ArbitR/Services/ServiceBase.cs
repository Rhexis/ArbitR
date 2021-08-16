using System.Reflection;

namespace ArbitR.Services
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
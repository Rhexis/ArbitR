using ArbitR.Pipeline.Read;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline
{
    public interface IArbiter
    {
        void Invoke(ICommand cmd);
        T Invoke<T>(IQuery<T> query);
        void Raise(IEvent eEvent);
        T Begin<T>(Workflow<T> workflow);
    }
}
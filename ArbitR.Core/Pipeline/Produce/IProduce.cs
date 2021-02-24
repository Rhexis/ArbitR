using ArbitR.Core.Pipeline.Perform;

namespace ArbitR.Core.Pipeline.Produce
{
    public interface IProduce : IPerform {}
    
    public interface IProduce<out TOut> : IProduce
    {
        TOut Produce();
    }
}
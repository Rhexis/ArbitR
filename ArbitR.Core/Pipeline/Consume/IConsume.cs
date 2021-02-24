using ArbitR.Core.Pipeline.Perform;

namespace ArbitR.Core.Pipeline.Consume
{
    public interface IConsume : IPerform {}

    public interface IConsume<in TIn> : IConsume
    {
        void Consume(TIn data);
    }
    
    public interface IConsume<in TIn, in TArg> : IConsume
    {
        void Consume(TIn data, TArg arg);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2, in TArg3> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2, in TArg3, in TArg4> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6, in TArg7> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7);
    }
    
    public interface IConsume<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6, in TArg7, in TArg8> : IConsume
    {
        void Consume(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8);
    }
}
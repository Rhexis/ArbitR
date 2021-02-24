using ArbitR.Core.Pipeline.Perform;

namespace ArbitR.Core.Pipeline.Transform
{
    public interface ITransform : IPerform {}
    
    public interface ITransform<in TIn, out TOut> : ITransform
    {
        TOut Transform(TIn data);
    }
    
    public interface ITransform<in TIn, in TArg, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg arg);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, in TArg3, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6, in TArg7, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7);
    }
    
    public interface ITransform<in TIn, in TArg1, in TArg2, in TArg3, in TArg4, in TArg5, in TArg6, in TArg7, in TArg8, out TOut> : ITransform
    {
        TOut Transform(TIn data, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8);
    }
}
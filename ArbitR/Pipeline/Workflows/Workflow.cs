using System;
using System.Collections.Generic;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public interface IWorkflow<out TResult>
    {
        IArbiter Arbiter { get; set; }
        List<Step<ICommand>> Steps { get; set; }
        TResult GetResult();
    }
    
    public abstract class Workflow<TResult> : IWorkflow<TResult>
    {
        public IArbiter Arbiter { get; set; } = null!;
        public List<Step<ICommand>> Steps { get; set; } = new();
        
        protected Step<ICommand> AddStep(Func<ICommand> commandFunc)
        {
            var step = new Step<ICommand>(commandFunc);
            Steps.Add(step);
            return step;
        }
        
        public abstract TResult GetResult();
    }
}
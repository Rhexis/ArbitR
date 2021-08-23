using System;
using System.Collections.Generic;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public abstract class Workflow<TResult>
    {
        public IArbiter Arbiter { get; set; } = null!;
        public List<Step<ICommand>> Steps { get; } = new();
        
        protected Step<ICommand> AddStep(Func<ICommand> commandFunc)
        {
            var step = new Step<ICommand>(commandFunc);
            Steps.Add(step);
            return step;
        }
        
        public abstract TResult GetResult();
    }
}
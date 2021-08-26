using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public abstract class Workflow<TResult>
    {
        protected IArbiter Arbiter { get; private set; } = null!;
        internal List<Step<ICommand>> Steps { get; } = new();
        
        protected Step<ICommand> AddStep(Expression<Func<ICommand>> commandFunc)
        {
            var step = new Step<ICommand>(commandFunc);
            Steps.Add(step);
            return step;
        }

        internal void Configure(IArbiter arbiter)
        {
            Arbiter = arbiter;

            foreach (Step<ICommand> step in Steps)
            {
                step.Configure();
            }
        }
        
        public abstract TResult GetResult();
    }
}
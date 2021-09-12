using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ArbitR.Internal.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public abstract class Workflow<TResult>
    {
        protected IArbiter Arbiter { get; private set; } = null!;
        internal readonly List<Step<ICommand>> Steps = new();
        
        protected Step<ICommand> AddStep(Expression<Func<ICommand>> commandFunc)
        {
            var step = new Step<ICommand>(commandFunc);
            Steps.Add(step);
            return step;
        }

        internal void Configure(IArbiter arbiter)
        {
            Arbiter = arbiter;
        }

        internal void Run()
        {
            foreach (Step<ICommand> step in Steps)
            {
                try
                {
                    Arbiter.Invoke(step.Command);
                    if (step.Success is not null) Arbiter.Raise(step.Success);
                }
                catch (Exception e)
                {
                    if (step.Failure is not null) Arbiter.Raise(step.Failure);
                    throw step.GetException(e);
                }
            }
        }
        
        public abstract TResult GetResult();

        public WorkflowDefinition GetDefinition() => WorkflowDefinition.CreateInstance(this);
    }
}
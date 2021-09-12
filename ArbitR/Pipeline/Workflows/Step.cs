using System;
using System.Linq.Expressions;
using ArbitR.Pipeline.Definitions;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public class Step<TCommand> where TCommand : ICommand
    {
        // ReSharper disable once MemberCanBePrivate.Global
        internal StepDefinition<TCommand> Definition { get; }
        internal TCommand Command => Definition.CommandExpr.Compile().Invoke();
        internal IEvent? Success => Definition.SuccessExpr?.Compile().Invoke();
        internal IEvent? Failure => Definition.FailureExpr?.Compile().Invoke();

        internal Step(Expression<Func<TCommand>> command)
        {
            Definition = new StepDefinition<TCommand>(command);
        }
        
        internal Exception GetException(Exception exception)
        {
            return Definition.ThrowExpr?.Compile().Invoke(exception) ?? exception;
        }

        public Step<TCommand> OnSuccess(Expression<Func<IEvent>> raise)
        {
            Definition.SuccessExpr = raise;
            return this;
        }
            
        public Step<TCommand> OnFailure(Expression<Func<IEvent>> raise)
        {
            Definition.FailureExpr = raise;
            return this;
        }
        
        public Step<TCommand> OnFailureThrow(Expression<Func<Exception, Exception>> e)
        {
            Definition.ThrowExpr = e;
            return this;
        }
    }
}
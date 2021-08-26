using System;
using System.Linq.Expressions;
using ArbitR.Internal.Pipeline.Workflows;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public class Step<TCommand> where TCommand : ICommand
    {
        // ReSharper disable once MemberCanBePrivate.Global
        internal StepDefinition<TCommand> Definition { get; }
        internal TCommand Command { get; private set; } = default!;
        internal IEvent? Success { get; private set; }
        internal IEvent? Failure { get; private set; }

        public Step(Expression<Func<TCommand>> command)
        {
            Definition = new StepDefinition<TCommand>(command);
        }
        
        internal void Configure()
        {
            Command = Definition.CommandExpr.Compile().Invoke() ?? throw new NullReferenceException("Misconfigured step in workflow, had no command");
            Success = Definition.SuccessExpr?.Compile().Invoke();
            Failure = Definition.FailureExpr?.Compile().Invoke();
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
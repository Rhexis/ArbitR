using System;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    public class Step<TCommand> where TCommand : ICommand
    {
        public readonly Func<TCommand>? Command;
        public Func<IEvent>? Success;
        public Func<IEvent>? Failure;
        public Func<Exception, Exception>? Throw;

        public Step(Func<TCommand> command)
        {
            Command = command;
        }

        public Step<TCommand> OnSuccess(Func<IEvent> raise)
        {
            Success = raise;
            return this;
        }
            
        public Step<TCommand> OnFailure(Func<IEvent> raise)
        {
            Failure = raise;
            return this;
        }
        
        public Step<TCommand> OnFailureThrow(Func<Exception, Exception> e)
        {
            Throw = e;
            return this;
        }
    }
}
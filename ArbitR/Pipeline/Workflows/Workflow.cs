using System;
using System.Collections.Generic;
using ArbitR.Internal.Extensions;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Workflows
{
    internal interface IWorkflow<TStart, out TResult>  where TStart : ICommand
    {
        TStart Start { get; set; }
        Step<TStart> StartStep { get; set; }
        List<Step<ICommand>> Steps { get; set; }
        TResult GetResult();
    }
    
    public abstract class Workflow<TStart, TResult> : IWorkflow<TStart, TResult> where TStart : ICommand
    {
        public TStart Start { get; set; } = default!;
        public Step<TStart> StartStep { get; set; } = default!;
        public List<Step<ICommand>> Steps { get; set; } = new();

        protected Step<TStart> ForStart()
        {
            StartStep = new Step<TStart>(null);
            return StartStep;
        }

        protected Step<TStep> AddStep<TStep>(Func<TStep> commandFunc) where TStep : ICommand
        {
            var step = new Step<TStep>(commandFunc);
            Steps.Add(step.Box().Unbox<Step<ICommand>>());
            return step;
        }
        
        public abstract TResult GetResult();
    }

    // Step
    public class Step<TCommand> where TCommand : ICommand
    {
        public Func<TCommand>? CommandFunc;
        public Func<TCommand, IEvent>? SuccessFunc;
        public Func<TCommand, IEvent>? FailureFunc;

        public Step(Func<TCommand>? commandFunc)
        {
            CommandFunc = commandFunc;
        }

        public Step<TCommand> OnSuccess(Func<TCommand, IEvent> raise)
        {
            SuccessFunc = raise;
            return this;
        }
            
        public Step<TCommand> OnFailure(Func<TCommand, IEvent> raise)
        {
            FailureFunc = raise;
            return this;
        }
    }
}

/*
public class RegisterUserResult
{
    public User User { get; set; }
    public Owner Owner { get; set; }
}

public class RegisterUserWorkflow : Workflow<RegisterUserResult>
{ 
    private User _user = null!;
    private Owner _owner = null!;

    public RegisterUserWorkflow()
    {
        ForStart()
            .OnSuccess(cmd => new UserCreatedEvent(cmd))
            .OnFailure(cmd => new CommandFailedEvent(cmd));
            
        AddStep<CreateOwnerCommand>(() => 
        {
            _user = Arbiter.Invoke(new GetUserQuery(Start.Login));
            return new CreateOwnerCommand(user.Id, Enums.OwnerType.User);
        }).OnSuccess(cmd => new OwnerCreatedEvent(cmd));
        
        AddStep<AuthenticateUserCommand>(() => 
        {
            _owner = Arbiter.Invoke(new GetOwnerQuery(Start.Login));
            return new AuthenticateUserCommand
            (
                Start.Login,
                Start.Password,
                _securityContext.ConnectionRemoteIpAddress
            );
        }).OnSuccess(cmd => new userAuthenticatedEvent(cmd));
    }
    
    public RegisterUserResult GetResult()
    {
        return new RegisterUserResult
        {
            User = _user,
            Owner = _owner
        }
    }
}
*/
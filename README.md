# ArbitR
A simple light weight CQRS implementation built in .NET

Has support for Commands, Queries and Event notifications.

ArbitR is built on top of the .NET Service Collection, after configuration inject an `IArbiter` where you need it and you are good to go!

## Configuration
Within a C# .NET 5 Web Applications Startup.cs or the project that contains the write/read services, read model managers & workflows.
```c#
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddArbitR(Assembly.GetExecutingAssembly());
    //...
}
```

## Usage
There are 4 types of services available in ArbitR.
1. WriteService
2. ReadService
3. ReadModelManager
4. Workflow (Beta)

This architecture is based off my interpretation of the Microsoft docs found here: 

https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs

### WriteService
Used for managing a single tables Create/Remove/Update actions. This is done through the invocation of commands. When making a command inherit `ICommand`.

###### Sample
```c#
public class ExampleWriteService : WriteService,
    IHandleCommand<ExampleCommand>
{
    /// ...
    
    public void Handle(ExampleCommand cmd)
    {
        // ...
    }
}
```

### ReadService
Used for managing a single tables Display actions. This is done through the invocation of queries. When making a query inherit `IQuery`.

###### Sample
```c#
public class ExampleReadService : ReadService,
    IHandleQuery<ExampleQuery, ExampleModel>
{
    /// ...
    
    public ExampleModel Handle(ExampleQuery query)
    {
        // ...
    }
}
```

### ReadModelManager
Used for managing read-only views built from one or more tables. This is done through the raising of events. When making an event, inherit `IEvent`.

###### Sample
```c#
public class LoginAttemptReadModelManager : ReadModelManager,
    IHandleEvent<LoginSuccessEvent>,
    IHandleEvent<LoginFailedEvent>,
    IHandleQuery<GetLoginAttempts, IEnumerable<LoginAttempt>>
{
    public void Handle(LoginSuccessEvent eEvent)
    {
        // Save login attempt
    }

    public void Handle(LoginFailedEvent eEvent)
    {
        // Save login attempt
    }
    
    public IEnumerable<LoginAttempt> Handle(GetLoginAttempts query)
    {
        // Get Login Attempts
    }
}
```

### Workflow (Beta)
A workflow is meant to be an elegant way of describing a process for which Arbiter will then handle its orchestration.
Use it when you need to chain together multiple commands, queries & events in a sequential order.
When making a workflow, inherit `Workflow<T>` where T is the returned result upon success of the workflow.
###### Sample
```c#
public class RegisterUserWorkflow : Workflow<UserRegisteredResult>
{
    private User _user = default!;
    
    public RegisterUserWorkflow(string email, string password, string firstname, string surname)
    {
        AddStep(() => new CreateUserCommand{Email = email, Password = password, Firstname = firstname, Surname = surname})
            .OnSuccess(() => new UserCreatedEvent(email));

        AddStep(() => AuthUser(email))
            .OnSuccess(() => new UserAuthenticatedEvent(firstname))
            .OnFailure(() => new UserFailedAuthenticationEvent($"{email} failed Authentication!"))
            .OnFailureThrow(e => new FailedAuthException(e));
    }
    
    public ICommand AuthenticateUser(string email)
    {
        _user = _arbiter.Invoke(new GetUserQuery(email));
        return new AuthenticateUserCommand
        {
            Email = _user.Email,
            Password = _user.Password
        };
    }
    
    public override UserRegisteredResult GetResult()
    {
        return new UserRegisteredResult(_user);
    }
}
```
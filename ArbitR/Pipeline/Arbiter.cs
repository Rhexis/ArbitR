using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Core;
using ArbitR.Core.Command;
using ArbitR.Core.Event;
using ArbitR.Core.Extensions;
using ArbitR.Core.Pipeline;
using ArbitR.Core.Query;
using ArbitR.Handlers;
using ArbitR.Services;

namespace ArbitR.Pipeline
{
    internal sealed class Arbiter : IArbiter
    {
        public void Invoke(ICommand command)
        {
            var handler = ServiceFactory
                .GetInstance(typeof(IHandleCommand<>).MakeGenericType(command.GetType()))
                .Unbox<WriteService>();
            handler.Handle(command);
        }
        
        public T Invoke<T>(IQuery<T> query)
        {
            var handler = ServiceFactory
                .GetInstance(typeof(IHandleQuery<,>).MakeGenericType(query.GetType(), typeof(T)))
                .Unbox<ReadService>();
            return handler.Handle(query).Unbox<T>();
        }
        
        public void Raise(IEvent eEvent)
        {
            IEnumerable<object> handlers = ServiceFactory
                .GetInstances(typeof(IHandleEvent<>).MakeGenericType(eEvent.GetType()));
            foreach (var handler in handlers)
            {
                handler.Unbox<ServiceBase>().Handle(eEvent);
            }
        }

        public void Execute(IPipeline pipeline)
        {
            var onFirstStep = true;
            object? previousStepResult = null;
            foreach (PipelineStep step in pipeline.Steps)
            {
                object? service = ServiceFactory.GetInstance(step.Handle.Service);
                object?[] args;
                
                if (onFirstStep)
                {
                    onFirstStep = false;
                    args = step.Args.ToArray().Box<object[]>();
                }
                else
                {
                    args = step.Args.ToList().Prepend(previousStepResult).ToArray();
                }
                
                previousStepResult = step.Handle.Service.InvokeMember
                (
                    step.Handle.Method,
                    BindingFlags.InvokeMethod,
                    null,
                    service,
                    args
                );
            }
        }
    }
}
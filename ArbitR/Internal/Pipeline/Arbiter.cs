using System;
using System.Collections.Generic;
using System.Linq;
using ArbitR.Internal.Extensions;
using ArbitR.Pipeline;
using ArbitR.Pipeline.Read;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Internal.Pipeline
{
    internal sealed class Arbiter : IArbiter
    {
        private readonly ServiceFactory _serviceFactory;

        public Arbiter(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public void Invoke(ICommand cmd)
        {
            var handler = _serviceFactory
                .GetInstances(typeof(IHandleCommand<>).MakeGenericType(cmd.GetType()))
                .Single()
                .Unbox<WriteService>();
            handler.Handle(cmd);
        }
        
        public T Invoke<T>(IQuery<T> query)
        {
            var handler = _serviceFactory
                .GetInstances(typeof(IHandleQuery<,>).MakeGenericType(query.GetType(), typeof(T)))
                .Single()
                .Unbox<ReadService>();
            return handler.Handle(query).Unbox<T>();
        }
        
        public void Raise(IEvent eEvent)
        {
            IEnumerable<object> handlers = _serviceFactory
                .GetInstances(typeof(IHandleEvent<>).MakeGenericType(eEvent.GetType()));
            foreach (var handler in handlers)
            {
                handler.Unbox<ReadModelManager>().Handle(eEvent);
            }
        }

        public T Begin<T>(ICommand cmd)
        {
            var workflow = _serviceFactory
                .GetInstances(typeof(IWorkflow<,>).MakeGenericType(cmd.GetType(), typeof(T)))
                .Single()
                .Unbox<IWorkflow<ICommand, T>>();

            try
            {
                Invoke(workflow.Start);
                workflow.StartStep.SuccessFunc?.Invoke(workflow.Start);

                foreach (Step<ICommand> step in workflow.Steps)
                {
                    // TODO :: Add better exceptions
                    ICommand stepCmd = step.CommandFunc?.Invoke() ?? throw new InvalidOperationException("Misconfigured workflow step");
                    try
                    {
                        Invoke(stepCmd);
                        step.SuccessFunc?.Invoke(stepCmd);
                    }
                    catch (Exception)
                    {
                        step.FailureFunc?.Invoke(stepCmd);
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                workflow.StartStep.FailureFunc?.Invoke(workflow.Start);
                throw;
            }

            return workflow.GetResult();
        }
    }
}
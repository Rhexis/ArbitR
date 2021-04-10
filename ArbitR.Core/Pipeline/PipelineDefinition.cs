using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ArbitR.Core.Extensions;
using ArbitR.Core.Pipeline.Consume;
using ArbitR.Core.Pipeline.Perform;
using ArbitR.Core.Pipeline.Transform;

namespace ArbitR.Core.Pipeline
{
    public class PipelineDefinition : IPipeline
    {
        private readonly List<PipelineStep> _steps = new();
        public IEnumerable<PipelineStep> Steps => _steps;

        protected Operation<T> For<T>() where T : IPerform
        {
            return new(this);
        }

        protected class Operation<T>
        {
            private readonly PipelineDefinition _definition;

            public Operation(PipelineDefinition definition)
            {
                _definition = definition;
            }

            public void AddStep(Expression<Action<T>> method)
            {
                var methodCallExpr = (MethodCallExpression) method.Body;
                List<object?> args = methodCallExpr.Arguments
                    .Select(x => x.GetArgumentValue()).ToList();
                if (args.First() == null)
                {
                    args.RemoveAt(0);
                }

                AddStep(methodCallExpr.Method.Name, args);
            }
            
            public void AddStep(string method)
            {
                AddStep(method, Array.Empty<object?>());
            }

            public void AddStep(string method, IEnumerable<object?> args)
            {
                _definition._steps.Add(new PipelineStep
                (
                    new PipelineStepHandle(typeof(T), method),
                    args
                ));
            }
        }
    }
    
    // Example
    public class TestPipelineDefinition : PipelineDefinition
    {
        public TestPipelineDefinition()
        {
            // The first argument of each step is the result of the previous step
            // only if the previous step returns a value, this occurs magically.
            // Steps are executed in the order in which they are added.
            For<TransformOperationProvider>().AddStep(x => x.Transform("Tyler", "Clement"));
            For<ConsumeOperationProvider>().AddStep(x => x.Consume(null!, true));
            //For<TransformOperationProvider>().AddStep("Transform", new []{"Tyler", "Clement"});
            //For<ConsumeOperationProvider>().AddStep("Consume", new object?[] {true});

            // Equivalent code.
            var consumer = new ConsumeOperationProvider();
            var transformer = new TransformOperationProvider();
            consumer.Consume(transformer.Transform("Tyler", "Clement"), true);
        }
    }
    
    public class TransformOperationProvider :
        ITransform<string, string, string>
    {
        public string Transform(string firstname, string surname)
        {
            return $"Hello, {firstname} {surname}!";
        }
    }

    public class ConsumeOperationProvider :
        IConsume<string, bool>
    {
        public void Consume(string data, bool useless)
        {
            Console.WriteLine(data);
        }
    }
}
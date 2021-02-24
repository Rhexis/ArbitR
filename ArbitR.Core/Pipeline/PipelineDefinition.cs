using System;
using System.Collections.Generic;
using ArbitR.Core.Pipeline.Consume;
using ArbitR.Core.Pipeline.Perform;
using ArbitR.Core.Pipeline.Transform;

namespace ArbitR.Core.Pipeline
{
    public class PipelineDefinition : IPipeline
    {
        private readonly List<PipelineStep> _steps = new();
        protected enum Method
        {
            Transform,
            Consume,
            Produce
        }
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
            
            public void AddStep(Method method)
            {
                AddStep(method, Array.Empty<object?>());
            }

            public void AddStep(Method method, IEnumerable<object?> args)
            {
                _definition._steps.Add(new PipelineStep
                (
                    new PipelineStepHandle(typeof(T), method.ToString()),
                    args
                ));
            }
        }
    }

    // Really Dumbed down example...
    public class TestPipelineDefinition : PipelineDefinition
    {
        public TestPipelineDefinition()
        {
            // The first argument of each step is the result of the previous step
            // only if the previous step returns a value, this occurs magically.
            // Steps are executed in the order in which they are added.
            For<TransformOperationProvider>().AddStep(Method.Transform, new []{"Tyler", "Clement"});
            For<ConsumeOperationProvider>().AddStep(Method.Consume);
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
        IConsume<string>
    {
        public void Consume(string data)
        {
            Console.WriteLine(data);
        }
    }
}
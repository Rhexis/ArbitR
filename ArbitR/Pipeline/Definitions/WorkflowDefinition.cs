using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Definitions
{
    public class WorkflowDefinition
    {
        public List<ConstructorDefinition> Constructors { get; }
        public IEnumerable<StepDefinition<ICommand>> Steps { get; }
        public string ReturnType { get; }

        private WorkflowDefinition(ConstructorInfo[] ctors, IEnumerable<StepDefinition<ICommand>> steps, Type returnType)
        {
            Constructors = ctors.Select(x => new ConstructorDefinition(x.GetParameters().Select(y => new ParameterDefinition(y.ParameterType.ToString(), y.Name!)).ToList())).ToList();
            Steps = steps;
            ReturnType = returnType.Name;
        }
        
        internal static WorkflowDefinition CreateInstance<T>(Workflow<T> workflow)
        {
            return new WorkflowDefinition
            (
                workflow.GetType().GetConstructors(),
                workflow.Steps.Select(step => step.Definition),
                typeof(T)
            );
        }
    }
}
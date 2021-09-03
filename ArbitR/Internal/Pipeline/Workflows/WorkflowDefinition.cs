using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArbitR.Pipeline.Workflows;
using ArbitR.Pipeline.Write;

namespace ArbitR.Internal.Pipeline.Workflows
{
    public class WorkflowDefinition
    {
        public List<Ctor> Constructors { get; }
        private readonly IEnumerable<StepDefinition<ICommand>> _steps;
        public string ReturnType { get; }

        private WorkflowDefinition(ConstructorInfo[] ctors, IEnumerable<StepDefinition<ICommand>> steps, Type returnType)
        {
            Constructors = ctors.Select(x => new Ctor(x.GetParameters().Select(y => new Parameter(y.ParameterType.ToString(), y.Name!)).ToList())).ToList();
            _steps = steps;
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

    public class Ctor
    {
        public List<Parameter> Parameters { get; }
        
        public Ctor(List<Parameter> parameters)
        {
            Parameters = parameters;
        }
    }
    
    public class Parameter
    {
        public string Type { get; }
        public string Name { get; }
        
        public Parameter(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
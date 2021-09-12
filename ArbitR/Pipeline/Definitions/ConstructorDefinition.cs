using System.Collections.Generic;

namespace ArbitR.Pipeline.Definitions
{
    public class ConstructorDefinition
    {
        public List<ParameterDefinition> Parameters { get; }
        
        public ConstructorDefinition(List<ParameterDefinition> parameters)
        {
            Parameters = parameters;
        }
    }
}
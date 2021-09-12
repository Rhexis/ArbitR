namespace ArbitR.Pipeline.Definitions
{
    public class ParameterDefinition
    {
        public string Type { get; }
        public string Name { get; }
        
        public ParameterDefinition(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
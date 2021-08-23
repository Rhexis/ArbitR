using ArbitR.Pipeline.Write;

namespace ArbitR.Tester.Workflows.Commands
{
    public class Step1Command : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class Step2Command : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
namespace ArbitR.Tester.Workflows.Results
{
    public class TestResult
    {
        public string Stage1 { get; }
        public string Stage2 { get; }
        
        public TestResult(string stage1, string stage2)
        {
            Stage1 = stage1;
            Stage2 = stage2;
        }
    }
}
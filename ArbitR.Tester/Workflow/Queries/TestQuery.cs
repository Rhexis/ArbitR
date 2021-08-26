using ArbitR.Pipeline.Read;

namespace ArbitR.Tester.Workflow.Queries
{
    public class TestQuery : IQuery<int>
    {
        public int Id { get; }

        public TestQuery(int id)
        {
            Id = id;
        }
    }
}
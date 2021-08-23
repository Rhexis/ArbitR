using ArbitR.Pipeline.Read;
using ArbitR.Tester.Read.Queries;

namespace ArbitR.Tester.Read.Handlers
{
    public class BadQueryHandler : ReadService,
        IHandleQuery<BadQuery, string>
    {
        public string Handle(BadQuery query)
        {
            return "";
        }
    }
    public class BadQuery2Handler : ReadService,
        IHandleQuery<BadQuery, string>
    {
        public string Handle(BadQuery query)
        {
            return "";
        }
    }
}
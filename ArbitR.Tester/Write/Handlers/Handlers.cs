using ArbitR.Pipeline.Write;
using ArbitR.Tester.Write.Commands;

namespace ArbitR.Tester.Write.Handlers
{
    public class BadCommandHandler : WriteService,
        IHandleCommand<BadCommand>
    {
        public void Handle(BadCommand cmd)
        {
            
        }
    }
    public class BadCommand2Handler : WriteService,
        IHandleCommand<BadCommand>
    {
        public void Handle(BadCommand cmd)
        {
            
        }
    }
}
using ArbitR.Core.Command;
using ArbitR.Handlers;

namespace ArbitR.Services
{
    public abstract class WriteService : ServiceBase, IWriteService
    {
        public void Handle(ICommand command)
        {
            Invoke(nameof(IHandleCommand<ICommand>.Handle), new object[] {command});
        }
    }
}
using ArbitR.Core.Command;
using ArbitR.Handlers;

namespace ArbitR.Services
{
    /// <summary>
    /// Used for managing a single tables Create/Remove/Update actions.
    /// Can consume only Commands.
    /// </summary>
    public abstract class WriteService : ServiceBase, IWriteService
    {
        public void Handle(ICommand cmd)
        {
            Invoke(nameof(IHandleCommand<ICommand>.Handle), new object[] {cmd});
        }
    }
}
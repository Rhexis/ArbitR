using ArbitR.Core.Command;

namespace ArbitR.Handlers
{
    public interface IHandleCommand<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand cmd);
    }
}
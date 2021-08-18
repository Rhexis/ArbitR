namespace ArbitR.Pipeline.Write
{
    public interface IHandleCommand<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand cmd);
    }
}
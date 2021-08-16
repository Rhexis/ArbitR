using ArbitR.Core.Command;

namespace ArbitR.Services
{
    internal interface IWriteService
    {
        void Handle(ICommand cmd);
    }
}
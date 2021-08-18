using ArbitR.Pipeline.Write;

namespace ArbitR.Internal.Pipeline.Service
{
    internal interface IWriteService
    {
        void Handle(ICommand cmd);
    }
}
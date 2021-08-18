using ArbitR.Pipeline.Read;

namespace ArbitR.Internal.Pipeline.Service
{
    internal interface IReadService
    {
        object? Handle(IQuery query);
    }
}
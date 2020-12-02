using ArbitR.Core.Query;

namespace ArbitR.Services
{
    internal interface IReadService
    {
        object? Handle(IQuery query);
    }
}
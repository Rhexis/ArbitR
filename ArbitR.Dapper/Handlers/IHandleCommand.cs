using System.Data;
using ArbitR.Core.Command;

namespace ArbitR.Dapper.Handlers
{
    public interface IHandleCommand<in TCommand> where TCommand : ICommand
    {
        void Handle(IDbConnection db, TCommand command);
    }
}
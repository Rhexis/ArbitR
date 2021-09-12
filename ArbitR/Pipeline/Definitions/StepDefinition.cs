using System;
using System.Linq.Expressions;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Pipeline.Definitions
{
    public class StepDefinition<TCommand> where TCommand : ICommand
    {
        // To get the actual type name from returned object == Expr.Body.Type.Name
        public Expression<Func<TCommand>> CommandExpr { get; }
        public Expression<Func<IEvent>>? SuccessExpr { get; internal set; }
        public Expression<Func<IEvent>>? FailureExpr { get; internal set; }
        public Expression<Func<Exception, Exception>>? ThrowExpr { get; internal set; }
        
        internal StepDefinition(Expression<Func<TCommand>> commandExpr)
        {
            CommandExpr = commandExpr;
        }
    }
}
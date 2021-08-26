using System;
using System.Linq.Expressions;
using ArbitR.Pipeline.ReadModel;
using ArbitR.Pipeline.Write;

namespace ArbitR.Internal.Pipeline.Workflows
{
    internal class StepDefinition<TCommand> where TCommand : ICommand
    {
        // To get the actual type name from returned object == Expr.Body.Type.Name
        public Expression<Func<TCommand>> CommandExpr { get; }
        public Expression<Func<IEvent>>? SuccessExpr { get; set; }
        public Expression<Func<IEvent>>? FailureExpr { get; set; }
        public Expression<Func<Exception, Exception>>? ThrowExpr { get; set; }
        
        public StepDefinition(Expression<Func<TCommand>> commandExpr)
        {
            CommandExpr = commandExpr;
        }
    }
}
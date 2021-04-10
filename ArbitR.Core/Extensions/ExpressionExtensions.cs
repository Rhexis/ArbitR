using System.Linq.Expressions;

namespace ArbitR.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static object? GetArgumentValue(this Expression expr)
        {
            if (expr is ConstantExpression constant)
            {
                return constant.Value;
            }

            LambdaExpression lambda = Expression.Lambda(Expression.Convert(expr, expr.Type));
            return lambda.Compile().DynamicInvoke();
        }
    }
}
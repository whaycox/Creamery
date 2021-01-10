using System.Linq.Expressions;

namespace Curds.Expressions.Abstraction
{
    public interface IExpressionNodeFactory
    {
        IExpressionNode Build(Expression expression);
    }
}

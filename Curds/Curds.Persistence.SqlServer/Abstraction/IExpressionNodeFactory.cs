using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    public interface IExpressionNodeFactory
    {
        IExpressionNode Build(Expression expression);
    }
}

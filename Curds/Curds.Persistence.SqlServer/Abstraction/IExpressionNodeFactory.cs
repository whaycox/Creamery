using System.Linq.Expressions;

namespace Curds.Persistence.Abstraction
{
    internal interface IExpressionNodeFactory
    {
        IExpressionNode<TReturn> Build<TReturn>(Expression expression);
    }
}

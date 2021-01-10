using System.Reflection;

namespace Curds.Expressions.Abstraction
{
    public interface IExpressionVisitorFactory
    {
        IExpressionVisitor<PropertyInfo> CreatePropertySelectionVisitor();
    }
}

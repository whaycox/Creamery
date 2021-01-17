using System.Reflection;

namespace Curds.Expressions.Implementation
{
    using Abstraction;

    internal class ExpressionVisitorFactory : IExpressionVisitorFactory
    {
        public IExpressionVisitor<PropertyInfo> CreatePropertySelectionVisitor() => new PropertySelectionVisitor();
    }
}

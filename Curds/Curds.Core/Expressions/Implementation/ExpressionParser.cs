using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Implementation
{
    using Abstraction;

    public class ExpressionParser : IExpressionParser
    {
        private static ExpressionParser _singleton = null;
        public static IExpressionParser Singleton
        {
            get
            {
                if (_singleton == null)
                    _singleton = new ExpressionParser(
                        new ExpressionNodeFactory(),
                        new ExpressionVisitorFactory());
                return _singleton;
            }
        }

        private IExpressionNodeFactory ExpressionNodeFactory { get; }
        private IExpressionVisitorFactory ExpressionVisitorFactory { get; }

        public ExpressionParser(
            IExpressionNodeFactory expressionNodeFactory,
            IExpressionVisitorFactory expressionVisitorFactory)
        {
            ExpressionNodeFactory = expressionNodeFactory;
            ExpressionVisitorFactory = expressionVisitorFactory;
        }

        public PropertyInfo ParsePropertyExpression<TEntity, TValue>(Expression<Func<TEntity, TValue>> propertyExpression)
        {
            IExpressionNode parsedPropertyExpression = ExpressionNodeFactory.Build(propertyExpression);
            IExpressionVisitor<PropertyInfo> propertySelectionVisitor = ExpressionVisitorFactory.CreatePropertySelectionVisitor();
            parsedPropertyExpression.AcceptVisitor(propertySelectionVisitor);

            return propertySelectionVisitor.Build();
        }
    }
}

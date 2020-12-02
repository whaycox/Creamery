using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Abstraction
{
    public interface IExpressionBuilder
    {
        ParameterExpression AddParameter<TEntity>(string name);
        ParameterExpression CreateObject<TEntity>(string name);

        Expression CallMethod(ParameterExpression variable, MethodInfo method, params Expression[] arguments);
        Expression GetProperty(ParameterExpression variable, PropertyInfo property);

        void SetProperty(ParameterExpression variable, PropertyInfo property, Expression value);

        void ReturnObject(ParameterExpression variable);

        TDelegate Build<TDelegate>();
    }
}

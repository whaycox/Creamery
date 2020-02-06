using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    internal delegate Expression AssignValueDelegate(ParameterExpression valueParameter, PropertyInfo valueProperty, ParameterExpression entityParameter);

    internal interface IValueExpressionBuilder
    {
        Type ValueType(Type propertyType);
        AssignValueDelegate AssignValue(Type valueType);

    }
}

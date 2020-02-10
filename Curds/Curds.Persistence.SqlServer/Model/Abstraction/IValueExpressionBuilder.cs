using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    internal interface IValueExpressionBuilder
    {
        ValueEntityDelegate BuildValueEntityDelegate(Type entityType, IEnumerable<PropertyInfo> valueProperties);
    }
}

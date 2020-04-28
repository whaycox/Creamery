using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;

    public interface IProjectEntityExpressionBuilder
    {
        ProjectEntityDelegate<IEntity> BuildProjectEntityDelegate(Type entityType, IEnumerable<PropertyInfo> valueProperties);
    }
}

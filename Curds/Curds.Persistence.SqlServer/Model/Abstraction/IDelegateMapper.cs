using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Query.Domain;
    using Persistence.Abstraction;
    using Query.Abstraction;

    public delegate ValueEntity ValueEntityDelegate(IEntity entity);
    public delegate void AssignIdentityDelegate(ISqlQueryReader queryReader, IEntity entity);
    public delegate TEntity ProjectEntityDelegate<out TEntity>(ISqlQueryReader queryReader) where TEntity : IEntity;

    public interface IDelegateMapper
    {
        ValueEntityDelegate MapValueEntityDelegate<TModel>(Type entityType)
            where TModel : IDataModel;
        AssignIdentityDelegate MapAssignIdentityDelegate<TModel>(Type entityType)
            where TModel : IDataModel;
        ProjectEntityDelegate<IEntity> MapProjectEntityDelegate<TModel>(Type entityType)
            where TModel : IDataModel;
    }
}

using System;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Query.Abstraction;
    using Query.Domain;

    public delegate ValueEntity ValueEntityDelegate(IEntity entity);
    public delegate void AssignIdentityDelegate(ISqlQueryReader queryReader, IEntity entity);
    public delegate IEntity ProjectEntityDelegate(ISqlQueryReader queryReader);

    public interface IDelegateMapper
    {
        ValueEntityDelegate MapValueEntityDelegate(IEntityModel entityModel);
        AssignIdentityDelegate MapAssignIdentityDelegate(IEntityModel entityModel);
        ProjectEntityDelegate MapProjectEntityDelegate(IEntityModel entityModel);
    }
}

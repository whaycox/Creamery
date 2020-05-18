using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Domain;
    using Query.Domain;

    public interface IEntityModel
    {
        Type EntityType { get; }

        string Schema { get; }
        string Table { get; }

        IEnumerable<IValueModel> Values { get; }
        IList<IValueModel> Keys { get; }
        IValueModel KeyValue { get; }
        IValueModel Identity { get; }
        IEnumerable<IValueModel> NonIdentities { get; }

        AssignIdentityDelegate AssignIdentity { get; }
        ProjectEntityDelegate ProjectEntity { get; }
        ValueEntityDelegate ValueEntity { get; }
    }
}

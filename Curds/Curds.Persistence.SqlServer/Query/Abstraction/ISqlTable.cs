﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Abstraction
{
    using Model.Abstraction;
    using Persistence.Abstraction;
    using Query.Domain;

    public interface ISqlTable
    {
        string Schema { get; }
        string Name { get; }

        IList<ISqlColumn> Values { get; }
        IList<ISqlColumn> Keys { get; }
        ISqlColumn Identity { get; }
        IEnumerable<ISqlColumn> NonIdentities { get; }

        ValueEntity BuildValueEntity(IEntity entity);
        void AssignIdentities(ISqlQueryReader reader, IEntity entity);
        IEntity ProjectEntity(ISqlQueryReader reader);
    }
}

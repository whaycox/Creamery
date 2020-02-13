using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    using Model.Domain;

    public interface IEntityConfiguration : IGlobalConfiguration
    {
        Type EntityType { get; }

        string Table { get; }
        List<IColumnConfiguration> Columns { get; }
    }
}

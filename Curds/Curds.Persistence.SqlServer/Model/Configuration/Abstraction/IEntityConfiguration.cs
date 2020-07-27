using System;
using System.Collections.Generic;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    using Model.Domain;

    public interface IEntityConfiguration : IGlobalConfiguration
    {
        Type EntityType { get; }

        string Table { get; }
        IList<IColumnConfiguration> Columns { get; }
        IList<string> Keys { get; }
    }
}

using System;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    public interface IEntityConfiguration : IGlobalConfiguration
    {
        Type EntityType { get; }

        string Table { get; }
        string Identity { get; }
    }
}

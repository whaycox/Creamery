using System;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    public interface IModelConfiguration : IGlobalConfiguration
    {
        Type ModelType { get; }
    }
}

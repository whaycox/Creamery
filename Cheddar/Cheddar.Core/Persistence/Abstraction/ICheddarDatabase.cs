using Curds.Persistence.Abstraction;

namespace Cheddar.Persistence.Abstraction
{
    using Cheddar.Domain;

    public interface ICheddarDatabase : IDatabase
    {
        ISimpleRepository<ICheddarDataModel, Organization> Organizations { get; }
    }
}

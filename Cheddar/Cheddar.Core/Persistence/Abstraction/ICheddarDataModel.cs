using Curds.Persistence.Abstraction;

namespace Cheddar.Persistence.Abstraction
{
    using Cheddar.Domain;

    public interface ICheddarDataModel : IDataModel
    {
        Organization Organizations { get; }
    }
}

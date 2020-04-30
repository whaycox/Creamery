using System.Collections.Generic;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Persistence.Domain;

    public interface IWrongGenericTypePropertyModel : IDataModel
    {
        TestEntity Test { get; }
        List<OtherEntity> Other { get; }
    }
}

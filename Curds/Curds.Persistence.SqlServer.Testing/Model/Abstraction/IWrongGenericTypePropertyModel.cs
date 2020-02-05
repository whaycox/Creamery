using System.Collections.Generic;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Persistence.Domain;

    public interface IWrongGenericTypePropertyModel : IDataModel
    {
        ITable<TestEntity> Test { get; }
        List<OtherEntity> Other { get; }
    }
}

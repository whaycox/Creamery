using System.Collections.Generic;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Persistence.Domain;

    public interface IPlainEntityPropertyModel : IDataModel
    {
        ITable<TestEntity> Test { get; }
        OtherEntity Other { get; }
    }
}

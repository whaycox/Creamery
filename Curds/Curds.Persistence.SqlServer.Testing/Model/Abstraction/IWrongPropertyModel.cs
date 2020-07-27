using System.Collections.Generic;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;
    using Persistence.Domain;

    public interface IWrongPropertyModel : IDataModel
    {
        TestEntity Test { get; }
        List<OtherEntity> Other { get; }
    }
}

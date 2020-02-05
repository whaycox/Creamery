using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface IOtherDataModel : IDataModel
    {
        ITable<TestEntity> TestEntities { get; }
        ITable<OtherEntity> Others { get; }
    }
}

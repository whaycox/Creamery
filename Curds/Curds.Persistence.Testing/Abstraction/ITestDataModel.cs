using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Domain;

    public interface ITestDataModel : IDataModel
    {
        ITable<TestEntity> Test { get; }
    }
}

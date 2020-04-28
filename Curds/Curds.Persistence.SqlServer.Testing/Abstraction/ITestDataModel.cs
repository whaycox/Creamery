using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Domain;
    using Model.Abstraction;

    public interface ITestDataModel : IDataModel
    {
        TestEntity Test { get; }
        OtherEntity Other { get; }
    }
}

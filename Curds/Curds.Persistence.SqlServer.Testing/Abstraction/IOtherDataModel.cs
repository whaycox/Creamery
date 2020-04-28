using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    using Domain;
    using Model.Abstraction;

    public interface IOtherDataModel : IDataModel
    {
        TestEntity TestEntities { get; }
        OtherEntity Others { get; }
    }
}

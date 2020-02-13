using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Configuration.Abstraction
{
    public interface IColumnConfiguration
    {
        string ValueName { get; }
        string Name { get; }
        bool? IsIdentity { get; }
    }
}

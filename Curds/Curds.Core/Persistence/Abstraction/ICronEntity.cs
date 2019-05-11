using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Abstraction
{
    public interface ICronEntity
    {
        string CronString { get; }
    }
}

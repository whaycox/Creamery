using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public interface ICronEntity
    {
        string CronString { get; }
    }
}

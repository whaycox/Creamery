using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Domain.Persistence
{
    public interface ICronEntity
    {
        int ID { get; }
        string CronString { get; }
    }
}

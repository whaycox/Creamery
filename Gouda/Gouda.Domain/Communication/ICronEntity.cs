using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    public interface ICronEntity
    {
        string CronString { get; }
    }
}

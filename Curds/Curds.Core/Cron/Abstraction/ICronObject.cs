using System;

namespace Curds.Cron.Abstraction
{
    public interface ICronObject
    {
        bool IsActive(DateTime testTime);
    }
}

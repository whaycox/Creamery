using System;

namespace Curds.Cron.Abstraction
{
    public interface ICronObject
    {
        bool Test(DateTime testTime);
    }
}

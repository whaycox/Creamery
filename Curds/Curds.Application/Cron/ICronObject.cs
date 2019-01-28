using System;

namespace Curds.Application.Cron
{
    public interface ICronObject
    {
        bool Test(DateTime testTime);
    }
}

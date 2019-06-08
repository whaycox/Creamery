using System;

namespace Curds.Cron.Mock
{
    public class ICronObject : Abstraction.ICronObject
    {
        public bool Results { get; set; }

        public bool Test(DateTime testTime) => Results;
    }
}

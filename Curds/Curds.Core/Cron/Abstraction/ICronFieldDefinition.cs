using System;

namespace Curds.Cron.Abstraction
{
    public interface ICronFieldDefinition
    {
        int AbsoluteMin { get; }
        int AbsoluteMax { get; }

        int SelectDatePart(DateTime testTime);
    }
}

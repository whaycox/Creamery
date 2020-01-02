using System;

namespace Curds.Cron.Abstraction
{
    public interface ICronFieldDefinition
    {
        int AbsoluteMin { get; }
        int AbsoluteMax { get; }

        int Parse(string value);
        int SelectDatePart(DateTime testTime);
    }
}

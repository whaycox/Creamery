using System;

namespace Curds.Cron.Abstraction
{
    public interface ICronFieldDefinition
    {
        int AbsoluteMin { get; }
        int AbsoluteMax { get; }

        string LookupAlias(string value);
        int SelectDatePart(DateTime testTime);
    }
}

using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class DayOfMonthFieldDefinition : BaseFieldDefinition
    {
        public override int AbsoluteMin => 1;
        public override int AbsoluteMax => 31;

        public override int SelectDatePart(DateTime testTime) => testTime.Day;
    }
}

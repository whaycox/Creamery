using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class DayOfWeekFieldDefinition : BaseFieldDefinition
    {
        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 6;

        public override int SelectDatePart(DateTime testTime) => (int)testTime.DayOfWeek;
    }
}

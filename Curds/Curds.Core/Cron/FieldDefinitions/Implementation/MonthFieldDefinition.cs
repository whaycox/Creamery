using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class MonthFieldDefinition : BaseFieldDefinition
    {
        public override int AbsoluteMin => 1;
        public override int AbsoluteMax => 12;

        public override int SelectDatePart(DateTime testTime) => testTime.Month;
    }
}

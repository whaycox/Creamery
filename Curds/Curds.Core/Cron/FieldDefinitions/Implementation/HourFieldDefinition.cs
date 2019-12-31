using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class HourFieldDefinition : BaseFieldDefinition
    {
        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 23;

        public override int SelectDatePart(DateTime testTime) => testTime.Hour;
    }
}

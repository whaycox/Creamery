using System;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class MinuteFieldDefinition : BaseFieldDefinition
    {
        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 59;

        public override int SelectDatePart(DateTime testTime) => testTime.Minute;
    }
}

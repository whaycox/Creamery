using System;
using System.Collections.Generic;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class DayOfWeekFieldDefinition : AliasedFieldDefinition
    {
        private static readonly Dictionary<string, string> _aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "SUN", DayOfWeekAlias(DayOfWeek.Sunday) },
            { "MON", DayOfWeekAlias(DayOfWeek.Monday) },
            { "TUE", DayOfWeekAlias(DayOfWeek.Tuesday) },
            { "WED", DayOfWeekAlias(DayOfWeek.Wednesday) },
            { "THU", DayOfWeekAlias(DayOfWeek.Thursday) },
            { "FRI", DayOfWeekAlias(DayOfWeek.Friday) },
            { "SAT", DayOfWeekAlias(DayOfWeek.Saturday) },
        };
        private static string DayOfWeekAlias(DayOfWeek dayOfWeek) => ((int)dayOfWeek).ToString();

        public override int AbsoluteMin => 0;
        public override int AbsoluteMax => 6;

        protected override Dictionary<string, string> Aliases => _aliases;

        public override int SelectDatePart(DateTime testTime) => (int)testTime.DayOfWeek;
    }
}

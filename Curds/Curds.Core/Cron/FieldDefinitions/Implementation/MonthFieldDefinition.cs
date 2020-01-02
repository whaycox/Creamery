using System;
using System.Collections.Generic;

namespace Curds.Cron.FieldDefinitions.Implementation
{
    internal class MonthFieldDefinition : AliasedFieldDefinition
    {
        private static readonly Dictionary<string, string> _aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "JAN", "1" },
            { "FEB", "2" },
            { "MAR", "3" },
            { "APR", "4" },
            { "MAY", "5" },
            { "JUN", "6" },
            { "JUL", "7" },
            { "AUG", "8" },
            { "SEP", "9" },
            { "OCT", "10" },
            { "NOV", "11" },
            { "DEC", "12" },
        };

        public override int AbsoluteMin => 1;
        public override int AbsoluteMax => 12;

        protected override Dictionary<string, string> Aliases => _aliases;

        public override int SelectDatePart(DateTime testTime) => testTime.Month;
    }
}

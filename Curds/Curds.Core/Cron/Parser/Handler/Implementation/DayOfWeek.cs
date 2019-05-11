using System;
using System.Collections.Generic;

namespace Curds.Cron.Parser.Handler.Implementation
{
    using Domain;

    public class DayOfWeek : Definite
    {
        private static readonly Dictionary<string, int> DaysOfWeek = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "SUN", 0 },
            { "MON", 1 },
            { "TUE", 2 },
            { "WED", 3 },
            { "THU", 4 },
            { "FRI", 5 },
            { "SAT", 6 },
        };

        protected override Dictionary<string, int> Lookups => DaysOfWeek;

        public DayOfWeek(ParsingHandler successor)
            : base(successor)
        { }
    }
}

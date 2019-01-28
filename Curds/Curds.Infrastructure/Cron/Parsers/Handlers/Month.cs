using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron.Parsers.Handlers
{
    internal class Month : Definite
    {
        private static readonly Dictionary<string, int> Months = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "JAN", 1 },
            { "FEB", 2 },
            { "MAR", 3 },
            { "APR", 4 },
            { "MAY", 5 },
            { "JUN", 6 },
            { "JUL", 7 },
            { "AUG", 8 },
            { "SEP", 9 },
            { "OCT", 10 },
            { "NOV", 11 },
            { "DEC", 12 },
        };

        protected override Dictionary<string, int> Lookups => Months;

        public Month(ParsingHandler successor)
            : base(successor)
        { }
    }
}

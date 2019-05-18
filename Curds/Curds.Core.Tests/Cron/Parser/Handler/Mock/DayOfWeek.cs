using System.Collections.Generic;

namespace Curds.Cron.Parser.Handler.Mock
{
    using Domain;

    public class DayOfWeek : Implementation.DayOfWeek
    {
        public DayOfWeek(ParsingHandler successor)
            : base(successor)
        { }

        public static readonly Dictionary<System.DayOfWeek, string> Map = new Dictionary<System.DayOfWeek, string>
        {
            { System.DayOfWeek.Sunday, "SUN" },
            { System.DayOfWeek.Monday, "MON" },
            { System.DayOfWeek.Tuesday, "TUE" },
            { System.DayOfWeek.Wednesday, "WED" },
            { System.DayOfWeek.Thursday, "THU" },
            { System.DayOfWeek.Friday, "FRI" },
            { System.DayOfWeek.Saturday, "SAT" },
        };
    }
}

namespace Curds.Cron.Parser
{
    using Handler.Domain;
    using Handler.Implementation;

    internal static class Extensions
    {
        public static ParsingHandler AddWildcard(this ParsingHandler chain) => new Wildcard(chain);
        public static ParsingHandler AddDefinite(this ParsingHandler chain) => new Definite(chain);
        public static ParsingHandler AddMonth(this ParsingHandler chain) => new Month(chain);
        public static ParsingHandler AddDayOfWeek(this ParsingHandler chain) => new DayOfWeek(chain);
        public static ParsingHandler AddLastDayOfMonth(this ParsingHandler chain) => new LastDayOfMonth(chain);
        public static ParsingHandler AddLastDayOfWeek(this ParsingHandler chain) => new LastDayOfWeek(chain);
        public static ParsingHandler AddNthDayOfWeek(this ParsingHandler chain) => new NthDayOfWeek(chain);
        public static ParsingHandler AddWeekdayNearest(this ParsingHandler chain) => new WeekdayNearest(chain);
    }
}

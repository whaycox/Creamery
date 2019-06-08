namespace Curds.Cron.Parser.Implementation
{
    using Domain;
    using Handler.Domain;

    public class DayOfMonth : Basic
    {
        protected override ParsingHandler Chain => base.Chain
            .AddWeekdayNearest()
            .AddLastDayOfMonth();
    }
}

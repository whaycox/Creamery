namespace Curds.Cron.Parser.Implementation
{
    using Domain;
    using Handler.Domain;

    public class DayOfWeek : Basic
    {
        protected override ParsingHandler Chain => base.Chain
            .AddDayOfWeek()
            .AddLastDayOfWeek()
            .AddNthDayOfWeek();
    }
}

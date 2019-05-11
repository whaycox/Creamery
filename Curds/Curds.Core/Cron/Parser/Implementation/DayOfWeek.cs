namespace Curds.Cron.Parser.Implementation
{
    using Domain;
    using Handler.Domain;

    internal class DayOfWeek : Basic
    {
        protected override ParsingHandler Chain => base.Chain
            .AddDayOfWeek()
            .AddLastDayOfWeek()
            .AddNthDayOfWeek();
    }
}

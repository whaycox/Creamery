using Curds.Infrastructure.Cron.Parsers.Handlers;

namespace Curds.Infrastructure.Cron.Parsers
{
    internal class DayOfMonth : Basic
    {
        protected override ParsingHandler Chain =>
            base.Chain
            .AddWeekdayNearest()
            .AddLastDayOfMonth();
    }
}

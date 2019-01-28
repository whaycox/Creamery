using Curds.Infrastructure.Cron.Parsers.Handlers;

namespace Curds.Infrastructure.Cron.Parsers
{
    internal class DayOfWeek : Basic
    {
        protected override ParsingHandler Chain =>
            base.Chain
            .AddDayOfWeek()
            .AddLastDayOfWeek()
            .AddNthDayOfWeek();
    }
}

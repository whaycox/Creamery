using Curds.Infrastructure.Cron.Parsers.Handlers;

namespace Curds.Infrastructure.Cron.Parsers
{
    internal class Month : Basic
    {
        protected override ParsingHandler Chain =>
            base.Chain
            .AddMonth();
    }
}

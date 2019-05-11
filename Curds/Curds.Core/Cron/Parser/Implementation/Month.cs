namespace Curds.Cron.Parser.Implementation
{
    using Domain;
    using Handler.Domain;

    internal class Month : Basic
    {
        protected override ParsingHandler Chain => base.Chain
            .AddMonth();
    }
}

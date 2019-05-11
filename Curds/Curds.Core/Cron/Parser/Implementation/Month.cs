namespace Curds.Cron.Parser.Implementation
{
    using Domain;
    using Handler.Domain;

    public class Month : Basic
    {
        protected override ParsingHandler Chain => base.Chain
            .AddMonth();
    }
}

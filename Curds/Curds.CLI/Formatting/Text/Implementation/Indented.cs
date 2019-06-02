namespace Curds.CLI.Formatting.Text.Implementation
{
    using Domain;
    using Token.Abstraction;
    using Token.Implementation;

    public class Indented : Temporary
    {
        public Indented(Formatted parentText)
            : base(parentText)
        { }

        protected override IToken EngageToken() => new IncreaseIndent();
        protected override IToken DisengageToken() => new DecreaseIndent();
    }
}

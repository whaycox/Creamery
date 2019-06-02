namespace Curds.CLI.Formatting.Text.Implementation
{
    using Domain;
    using Token.Abstraction;
    using Token.Implementation;

    public class Concatenated : Temporary
    {
        private string Start { get; }
        private IToken Between { get; }
        private string End { get; }

        public Concatenated(Formatted parentText, string start, string between, string end)
            : this(parentText, start, new PlainText(between), end)
        { }

        public Concatenated(Formatted parentText, string start, IToken between, string end)
            : base(parentText)
        {
            Start = start;
            Between = between;
            End = end;
        }

        protected override void AddToBuffer(IToken token)
        {
            if (ShouldAddBetweenToken(token))
                base.AddToBuffer(Between);
            base.AddToBuffer(token);
        }
        private bool ShouldAddBetweenToken(IToken token) => Buffer.Count > 0 && !(token is NewLine) && !(token is EndConcatenation);

        protected override IToken EngageToken() => new PlainText(Start);
        protected override IToken DisengageToken() => new EndConcatenation(End);
    }
}

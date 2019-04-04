namespace Curds.CLI.Formatting.Tokens
{
    public class ConcatenatedText : TemporaryText
    {
        private string Start { get; }
        private string Between { get; }
        private string End { get; }

        public ConcatenatedText(FormattedText parentText, string start, string between, string end)
            : base(parentText)
        {
            Start = start;
            Between = between;
            End = end;
        }

        protected override void AddToBuffer(BaseTextToken token)
        {
            if (Buffer.Count != 0 && !(token is NewLineToken) && !(token is EndToken))
                base.AddToBuffer(new BetweenToken(this));
            base.AddToBuffer(token);
        }

        protected override BaseTextToken EngageToken() => new StartToken(this);
        protected override BaseTextToken DisengageToken() => new EndToken(this);

        private abstract class MaybeToken : BaseTextToken
        {
            private string MaybeText { get; }
            
            public MaybeToken(string maybeText)
            {
                MaybeText = maybeText;
            }

            public override void Write(IConsoleWriter writer)
            {
                if (!string.IsNullOrEmpty(MaybeText))
                    writer.Write(MaybeText);
            }
        }

        private class StartToken : MaybeToken
        {
            public StartToken(ConcatenatedText parent)
                : base(parent.Start)
            { }
        }

        private class BetweenToken : MaybeToken
        {
            public BetweenToken(ConcatenatedText parent)
                : base(parent.Between)
            { }
        }

        private class EndToken : MaybeToken
        {
            public EndToken(ConcatenatedText parent)
                : base(parent.End)
            { }
        }
    }
}

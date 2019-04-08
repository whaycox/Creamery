﻿namespace Curds.CLI.Formatting.Tokens
{
    public class IndentedText : TemporaryText
    {
        public IndentedText(FormattedText parentText)
            : base(parentText)
        { }

        protected override BaseTextToken EngageToken() => new IncreaseIndentToken();
        protected override BaseTextToken DisengageToken() => new DecreaseIndentToken();

        private class IncreaseIndentToken : BaseTextToken
        {
            public override void Write(IConsoleWriter writer) => writer.Indents++;
        }

        private class DecreaseIndentToken : BaseTextToken
        {
            public override void Write(IConsoleWriter writer) => writer.Indents--;
        }
    }
}
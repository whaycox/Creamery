using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Formatting.Tokens
{
    public sealed class NewLineToken : PlainTextToken
    {
        public NewLineToken()
            : base(Environment.NewLine)
        { }

        public override void Write(IConsoleWriter writer)
        {
            base.Write(writer);
            writer.StartOfNewLine = true;
        }

        public static NewLineToken New => new NewLineToken();
    }
}

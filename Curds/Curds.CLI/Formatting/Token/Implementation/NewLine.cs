using System;

namespace Curds.CLI.Formatting.Token.Implementation
{
    using CLI.Abstraction;

    public sealed class NewLine : PlainText
    {
        public NewLine()
            : base(Environment.NewLine)
        { }

        public override void Write(IConsole console)
        {
            base.Write(console);
            console.ResetNewLine();
        }
    }
}

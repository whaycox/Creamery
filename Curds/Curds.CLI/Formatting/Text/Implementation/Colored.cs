using System;

namespace Curds.CLI.Formatting.Text.Implementation
{
    using CLI.Domain;
    using Domain;
    using Token.Abstraction;
    using Token.Implementation;

    public class Colored : Temporary
    {
        public ConsoleColor Color { get; }

        public Colored(Formatted parentText, ConsoleColor color)
            : base(parentText)
        {
            Color = color;
        }

        protected override IToken EngageToken() => new SetTextColor(Color);
        protected override IToken DisengageToken() => new ResetTextColor();
    }
}

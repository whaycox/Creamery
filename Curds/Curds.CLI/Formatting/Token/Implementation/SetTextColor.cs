using System;

namespace Curds.CLI.Formatting.Token.Implementation
{
    using Abstraction;
    using CLI.Abstraction;
    using Text.Implementation;

    public class SetTextColor : IToken
    {
        private ConsoleColor Color { get; }

        public SetTextColor(ConsoleColor color)
        {
            Color = color;
        }

        public void Write(IConsole console) => console.ApplyColor(Color);
    }
}

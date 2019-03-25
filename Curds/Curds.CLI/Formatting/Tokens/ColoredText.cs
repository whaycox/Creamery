using System;

namespace Curds.CLI.Formatting.Tokens
{
    public class ColoredText : TemporaryText
    {
        private ConsoleColor Color { get; }

        public ColoredText(ConsoleColor color)
        {
            Color = color;
        }
        
        protected override BaseTextToken EngageToken() => new ColorTextToken(this);
        protected override BaseTextToken DisengageToken() => new ResetTextColorToken();

        private class ColorTextToken : BaseTextToken
        {
            private ColoredText Parent { get; }

            public ColorTextToken(ColoredText parent)
            {
                Parent = parent;
            }

            public override void Write(IConsoleWriter writer) => writer.SetTextColor(Parent.Color);
        }

        private class ResetTextColorToken : BaseTextToken
        {
            public override void Write(IConsoleWriter writer) => writer.ResetTextColor();
        }
    }
}

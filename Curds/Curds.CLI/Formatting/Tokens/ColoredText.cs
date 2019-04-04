using System;

namespace Curds.CLI.Formatting.Tokens
{
    public class ColoredText : TemporaryText
    {
        private ConsoleColor Color { get; }

        private ConsoleColor PreviousColor { get; set; }

        public ColoredText(FormattedText parentText, ConsoleColor color)
            : base(parentText)
        {
            Color = color;
            PreviousColor = CLIEnvironment.DefaultTextColor;
        }
        
        protected override BaseTextToken EngageToken() => new ColorTextToken(this);
        protected override BaseTextToken DisengageToken() => new ResetTextColorToken(this);

        private class ColorTextToken : BaseTextToken
        {
            private ColoredText Parent { get; }

            public ColorTextToken(ColoredText parent)
            {
                Parent = parent;
            }

            public override void Write(IConsoleWriter writer)
            {
                Parent.PreviousColor = writer.CurrentColor;
                writer.SetTextColor(Parent.Color);
            }
        }

        private class ResetTextColorToken : BaseTextToken
        {
            private ColoredText Parent { get; }

            public ResetTextColorToken(ColoredText parent)
            {
                Parent = parent;
            }

            public override void Write(IConsoleWriter writer)
            {
                writer.SetTextColor(Parent.PreviousColor);
            }
        }
    }
}

using System;
using System.Text;

namespace Curds.CLI.Writer
{
    public class ConsoleWriter : BaseConsoleWriter
    {
        public override string Indentation { get; set; }
        public override int Indents { get; set; }

        public override bool StartOfNewLine { get; set; }

        public override ConsoleColor CurrentColor => Console.ForegroundColor;

        public ConsoleWriter()
        {
            Console.ForegroundColor = CLIEnvironment.DefaultTextColor;
        }

        public override void SetTextColor(ConsoleColor color) => Console.ForegroundColor = color;
        public override void ResetTextColor() => Console.ForegroundColor = CLIEnvironment.DefaultTextColor;

        protected override void WriteInternal(string message) => Console.Write(message);
    }
}

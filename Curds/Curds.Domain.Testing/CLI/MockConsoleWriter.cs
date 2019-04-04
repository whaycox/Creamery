using Curds.CLI;
using Curds.CLI.Writer;
using System;
using System.Collections.Generic;

namespace Curds.Domain.CLI
{
    public class MockConsoleWriter : BaseConsoleWriter
    {
        public static string StartOfNewLineWrite(bool newValue) => $"{nameof(StartOfNewLine)} changes to {newValue}";
        public static string TextColorChangeWrite(ConsoleColor textColor) => $"Color changes to {textColor}";
        public static string IndentsWrite(int indents) => $"Changing indents to {indents}";

        public List<string> Writes = new List<string>();

        public override string Indentation { get; set; }

        private ConsoleColor _current = CLIEnvironment.DefaultTextColor;
        public override ConsoleColor CurrentColor => _current;

        private int _indents = default(int);
        public override int Indents
        {
            get
            {
                return _indents;
            }
            set
            {
                Writes.Add(IndentsWrite(value));
                _indents = value;
            }
        }

        private bool _startOfNewLine = true;
        public override bool StartOfNewLine
        {
            get
            {
                return _startOfNewLine;
            }
            set
            {
                Writes.Add(StartOfNewLineWrite(value));
                _startOfNewLine = value;
            }
        }

        public override void ResetTextColor() => Writes.Add(TextColorChangeWrite(CLIEnvironment.DefaultTextColor));
        public override void SetTextColor(ConsoleColor color)
        {
            _current = color;
            Writes.Add(TextColorChangeWrite(color));
        }
        protected override void WriteInternal(string message) => Writes.Add(message);
    }
}

using Curds.CLI;
using Curds.CLI.Writer;
using System;
using System.Collections.Generic;

namespace Curds.Domain.CLI
{
    public class MockConsoleWriter : BaseConsoleWriter
    {
        public List<string> Writes = new List<string>();

        public override string Indentation { get; set; }

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

        public string StartOfNewLineWrite(bool newValue) => $"{nameof(StartOfNewLine)} changes to {newValue}";
        public string TextColorChangeWrite(ConsoleColor textColor) => $"Color changes to {textColor}";
        public string IndentsWrite(int indents) => $"Changing indents to {indents}";

        public override void ResetTextColor() => Writes.Add(TextColorChangeWrite(CLIEnvironment.DefaultTextColor));
        public override void SetTextColor(ConsoleColor color) => Writes.Add(TextColorChangeWrite(color));
        protected override void WriteInternal(string message) => Writes.Add(message);
    }
}

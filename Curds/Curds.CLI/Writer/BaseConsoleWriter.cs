using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Writer
{
    public abstract class BaseConsoleWriter : IConsoleWriter
    {
        public abstract string Indentation { get; set; }
        public abstract int Indents { get; set; }
        public abstract bool StartOfNewLine { get; set; }

        public abstract ConsoleColor CurrentColor { get; }

        public BaseConsoleWriter()
        {
            Indentation = CLIEnvironment.DefaultIndentation;
            StartOfNewLine = true;
        }
        
        public abstract void Exit(int exitCode);

        public abstract void ResetTextColor();
        public abstract void SetTextColor(ConsoleColor color);

        public void WriteLine(string message)
        {
            Write(message);
            Write(Environment.NewLine);
            StartOfNewLine = true;
        }
        public void Write(string message)
        {
            if (StartOfNewLine)
            {
                message = $"{BuildIndents()}{message}";
                StartOfNewLine = false;
            }
            WriteInternal(message);
        }
        private string BuildIndents()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Indents; i++)
                builder.Append(Indentation);
            return builder.ToString();
        }
        protected abstract void WriteInternal(string message);
    }
}

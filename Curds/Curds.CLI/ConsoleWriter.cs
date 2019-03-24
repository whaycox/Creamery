using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI
{
    public class ConsoleWriter
    {
        private const string DefaultIndentation = "\t";

        public string Indentation { get; set; } = DefaultIndentation;
        public int Indents { get; set; }

        public void Write(string message) => Console.Write($"{BuildIndents()}{message}");
        private string BuildIndents()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Indents; i++)
                builder.Append(Indentation);
            return builder.ToString();
        }

    }
}

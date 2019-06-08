using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Implementation
{
    using Abstraction;
    using Domain;

    public class Console : IConsole
    {
        private Stack<ConsoleColor> Colors = new Stack<ConsoleColor>();
        private bool IsAtNewLine = true;
        private int CurrentIndents = 0;

        public string Indentation { get; set; }

        public ConsoleColor CurrentColor => Colors.Count == 0 ? CLIEnvironment.DefaultTextColor : Colors.Peek();

        public void Exit(int exitCode) => Environment.Exit(exitCode);

        public void ApplyColor(ConsoleColor color) => Colors.Push(color);
        public void RemoveColor() => Colors.Pop();

        public void ResetNewLine() => IsAtNewLine = true;

        public void IncreaseIndent() => CurrentIndents++;
        public void DecreaseIndent() => CurrentIndents--;

        public void Write(string message)
        {
            if (IsAtNewLine)
            {
                message = $"{BuildIndents()}{message}";
                IsAtNewLine = false;
            }
            System.Console.ForegroundColor = CurrentColor;
            System.Console.Write(message);
        }
        private string BuildIndents()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < CurrentIndents; i++)
                builder.Append(Indentation);
            return builder.ToString();
        }
    }
}

using System;

namespace Curds.CLI
{
    public interface IConsoleWriter
    {
        string Indentation { get; set; }
        int Indents { get; set; }

        bool StartOfNewLine { get; set; }

        ConsoleColor CurrentColor { get; }

        void Exit(int exitCode);

        void SetTextColor(ConsoleColor color);
        void ResetTextColor();

        void Write(string message);
        void WriteLine(string message);
    }
}

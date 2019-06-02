using System;

namespace Curds.CLI.Abstraction
{
    public interface IConsole
    {
        string Indentation { get; set; }

        ConsoleColor CurrentColor { get; }

        void Exit(int exitCode);

        void ApplyColor(ConsoleColor color);
        void RemoveColor();

        void IncreaseIndent();
        void DecreaseIndent();

        void Write(string message);
        void ResetNewLine();
    }
}

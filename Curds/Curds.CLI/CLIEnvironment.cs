using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI
{
    public static class CLIEnvironment
    {
        public const string DefaultIndentation = "    "; //Four spaces

        public static ConsoleColor DefaultTextColor = ConsoleColor.White;

        public static ConsoleColor Error = ConsoleColor.Red;
        public static ConsoleColor Application = ConsoleColor.Magenta;
        public static ConsoleColor Operation = ConsoleColor.Cyan;
        public static ConsoleColor Argument = ConsoleColor.Green;
        public static ConsoleColor Value = ConsoleColor.Yellow;
    }
}

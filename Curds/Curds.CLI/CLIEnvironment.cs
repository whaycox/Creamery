using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI
{
    public static class CLIEnvironment
    {
        public const string DefaultIndentation = "\t";

        public static ConsoleColor DefaultTextColor = ConsoleColor.White;

        public static ConsoleColor Error = ConsoleColor.Red;       
        public static ConsoleColor Operations = ConsoleColor.Cyan;
        public static ConsoleColor Arguments = ConsoleColor.Green;
        public static ConsoleColor Value = ConsoleColor.Yellow;
    }
}

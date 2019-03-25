using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Writer
{
    using Formatting;

    public static class ConsoleWriterExtensions
    {
        public static IConsoleWriter OutputText(this IConsoleWriter writer, FormattedText text)
        {
            text.Write(writer);
            return writer;
        }
    }
}

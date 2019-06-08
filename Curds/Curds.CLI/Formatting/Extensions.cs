using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Formatting
{
    using Text.Implementation;
    using Token.Abstraction;
    using Token.Implementation;
    using Text.Domain;

    public static class Extensions
    {
        public static Formatted Append(this Formatted text, IToken token)
        {
            text.Add(token);
            return text;
        }
        public static Formatted AppendLine(this Formatted text, IToken token)
        {
            text.AddLine(token);
            return text;
        }
        public static Formatted Append(this Formatted text, IEnumerable<IToken> tokens)
        {
            foreach (IToken token in tokens)
                text.Add(token);
            return text;
        }
        public static Formatted AppendLine(this Formatted text, IEnumerable<IToken> tokens)
        {
            using (Concatenated concatenated = new Concatenated(text, null, new NewLine(), null))
                foreach (IToken token in tokens)
                    concatenated.Add(token);
            return text;
        }

        public static Formatted Indent(this Formatted text, IToken indentedToken) => IndentLine(text, new List<IToken> { indentedToken });
        public static Formatted IndentLine(this Formatted text, IEnumerable<IToken> indentedTokens)
        {
            using (Indented indent = new Indented(text))
                indent.ConcatenateNewLines(indentedTokens);
            return text;
        }

        public static Formatted Color(this Formatted text, ConsoleColor textColor) => new Formatted().Color(textColor, text);
        public static Formatted Color(this Formatted text, ConsoleColor textColor, IToken coloredToken) => Color(text, textColor, new List<IToken> { coloredToken });
        public static Formatted ColorLine(this Formatted text, ConsoleColor textColor, IToken coloredToken) => Color(text, textColor, new List<IToken> { coloredToken, new NewLine() });
        public static Formatted Color(this Formatted text, ConsoleColor textColor, IEnumerable<IToken> coloredTokens)
        {
            using (Colored color = new Colored(text, textColor))
                foreach (IToken token in coloredTokens)
                    color.Add(token);
            return text;
        }

        public static Formatted ConcatenateNewLines(this Formatted text, IEnumerable<IToken> concatenatedTokens)
        {
            using (Concatenated concatenated = new Concatenated(text, null, new NewLine(), null))
                foreach (IToken token in concatenatedTokens)
                    concatenated.Add(token);
            return text;
        }
        public static Formatted Concatenate(this Formatted text, string start, string between, string end, IEnumerable<IToken> concatenatedTokens)
        {
            using (Concatenated concatenated = new Concatenated(text, start, between, end))
                foreach (IToken token in concatenatedTokens)
                    concatenated.Add(token);
            return text;
        }
    }
}

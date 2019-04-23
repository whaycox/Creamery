using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Formatting
{
    using Tokens;

    public static class FormattingExtensions
    {
        public static FormattedText Append(this FormattedText text, BaseTextToken token)
        {
            text.Add(token);
            return text;
        }
        public static FormattedText AppendLine(this FormattedText text, BaseTextToken token)
        {
            text.AddLine(token);
            return text;
        }
        public static FormattedText Append(this FormattedText text, IEnumerable<BaseTextToken> tokens)
        {
            foreach (BaseTextToken token in tokens)
                text.Add(token);
            return text;
        }
        public static FormattedText AppendLine(this FormattedText text, IEnumerable<BaseTextToken> tokens)
        {
            foreach (BaseTextToken token in tokens)
                text.AddLine(token);
            return text;
        }

        public static FormattedText Indent(this FormattedText text, BaseTextToken indentedToken) => Indent(text, new List<BaseTextToken> { indentedToken });
        public static FormattedText IndentLine(this FormattedText text, BaseTextToken indentedToken) => Indent(text, new List<BaseTextToken> { indentedToken, NewLineToken.New });
        public static FormattedText Indent(this FormattedText text, IEnumerable<BaseTextToken> indentedTokens)
        {
            using (IndentedText indent = new IndentedText(text))
                foreach (BaseTextToken token in indentedTokens)
                    indent.Add(token);
            return text;
        }
        public static FormattedText IndentLine(this FormattedText text, IEnumerable<BaseTextToken> indentedTokens)
        {
            using (IndentedText indent = new IndentedText(text))
                foreach (BaseTextToken token in indentedTokens)
                    indent.AddLine(token);
            return text;
        }

        public static FormattedText Color(this FormattedText text, ConsoleColor textColor) => FormattedText.New.Color(textColor, text);
        public static FormattedText Color(this FormattedText text, ConsoleColor textColor, BaseTextToken coloredToken) => Color(text, textColor, new List<BaseTextToken> { coloredToken });
        public static FormattedText ColorLine(this FormattedText text, ConsoleColor textColor, BaseTextToken coloredToken) => Color(text, textColor, new List<BaseTextToken> { coloredToken, NewLineToken.New });
        public static FormattedText Color(this FormattedText text, ConsoleColor textColor, IEnumerable<BaseTextToken> coloredTokens)
        {
            using (ColoredText color = new ColoredText(text, textColor))
                foreach (BaseTextToken token in coloredTokens)
                    color.Add(token);
            return text;
        }

        public static FormattedText Concatenate(this FormattedText text, string start, string between, string end, IEnumerable<BaseTextToken> concatenatedTokens)
        {
            BaseTextToken[] tokenArray = concatenatedTokens.ToArray();
            if (tokenArray.Length == 0)
                throw new ArgumentNullException(nameof(concatenatedTokens));

            using (ConcatenatedText concatenate = new ConcatenatedText(text, start, between, end))
                foreach (BaseTextToken token in concatenatedTokens)
                    concatenate.Add(token);
            return text;
        }
    }
}

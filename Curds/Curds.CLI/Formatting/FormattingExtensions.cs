using System;
using System.Collections.Generic;

namespace Curds.CLI.Formatting
{
    using Tokens;

    public static class FormattingExtensions
    {
        public static FormattedText Append(this FormattedText text, BaseTextToken formattedText)
        {
            text.Add(formattedText);
            return text;
        }
        public static FormattedText AppendLine(this FormattedText text, BaseTextToken formattedText)
        {
            text.AddLine(formattedText);
            return text;
        }

        public static FormattedText Indent(this FormattedText text, BaseTextToken indentedText) => Indent(text, new List<BaseTextToken> { indentedText });
        public static FormattedText IndentLine(this FormattedText text, BaseTextToken indentedText) => Indent(text, new List<BaseTextToken> { indentedText, NewLineToken.New });
        public static FormattedText Indent(this FormattedText text, IEnumerable<BaseTextToken> indentedTexts)
        {
            IndentedText indent = new IndentedText();
            indent.Engage();
            foreach (BaseTextToken indented in indentedTexts)
                indent.Add(indented);
            indent.Disengage();
            text.Add(indent);
            return text;
        }

        public static FormattedText Color(this FormattedText text, ConsoleColor textColor, BaseTextToken coloredText) => Color(text, textColor, new List<BaseTextToken> { coloredText });
        public static FormattedText ColorLine(this FormattedText text, ConsoleColor textColor, BaseTextToken coloredText) => Color(text, textColor, new List<BaseTextToken> { coloredText, NewLineToken.New });
        public static FormattedText Color(this FormattedText text, ConsoleColor textColor, IEnumerable<BaseTextToken> coloredTexts)
        {
            ColoredText color = new ColoredText(textColor);
            color.Engage();
            foreach (BaseTextToken colored in coloredTexts)
                color.Add(colored);
            color.Disengage();
            text.Add(color);
            return text;
        }
    }
}

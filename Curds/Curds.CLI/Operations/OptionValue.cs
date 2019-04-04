using System;
using System.Collections.Generic;

namespace Curds.CLI.Operations
{
    using Formatting;
    using Formatting.Tokens;

    public abstract class OptionValue
    {
        public const string Indent = "\t";

        public abstract string Name { get; }
        public abstract string Description { get; }

        protected FormattedText EncloseIdentifiers(string start, string end, string message) => FormattedText.New
            .Concatenate(start, null, end, new List<BaseTextToken> { PlainTextToken.Create(message) });

        protected FormattedText FormattedNameAndDescription(ConsoleColor nameColor) => FormattedText.New
            .Color(nameColor, PlainTextToken.Create(Name))
            .Append(PlainTextToken.Create($": {Description}"));

        protected FormattedText IndentChildren(string header, ConsoleColor headerColor, IEnumerable<BaseTextToken> children) => FormattedText.New
            .ColorLine(headerColor, PlainTextToken.Create(header))
            .IndentLine(children);

        public abstract FormattedText Syntax { get; }
        public abstract FormattedText Usage { get; }
    }
}

using System;
using System.Collections.Generic;

namespace Curds.CLI.Operations.Domain
{
    using Formatting;
    using Formatting.Text.Domain;
    using Formatting.Token.Abstraction;
    using Formatting.Token.Implementation;

    public abstract class OptionValue
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        protected Formatted EncloseIdentifiers(string start, string end, string message) =>
            EncloseIdentifiers(start, end, new Formatted().Append(new PlainText(message)));
        protected Formatted EncloseIdentifiers(string start, string end, Formatted text) => new Formatted()
            .Concatenate(start, null, end, text.Output);

        protected Formatted FormattedNameAndDescription(ConsoleColor nameColor) => new Formatted()
            .Color(nameColor, new PlainText(Name))
            .Append(new PlainText($": {Description}"));

        protected Formatted IndentChildren(string header, ConsoleColor headerColor, IEnumerable<IToken> children) => new Formatted()
            .ColorLine(headerColor, new PlainText(header))
            .IndentLine(children);

        public abstract Formatted Syntax { get; }
        public abstract Formatted Usage { get; }
    }
}

using System.Collections.Generic;

namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Query.Abstraction;
    using Text.Abstraction;
    using Tokens.Implementation;

    public abstract class BaseSqlQueryFormatter : ISqlQueryFormatter, ISqlQueryFormatVisitor
    {
        protected IIndentStringBuilder StringBuilder { get; }

        public BaseSqlQueryFormatter(IIndentStringBuilder stringBuilder)
        {
            StringBuilder = stringBuilder;
        }

        public string FormatTokens(IEnumerable<ISqlQueryToken> tokens)
        {
            foreach (ISqlQueryToken token in tokens)
            {
                token.AcceptFormatVisitor(this);
                StringBuilder.SetNewLine();
            }

            return StringBuilder.Flush();
        }

        public abstract void VisitLiteral(LiteralSqlQueryToken token);
        public abstract void VisitTokenList(TokenListSqlQueryToken token);
        public abstract void VisitValueEntity(ValueEntitySqlQueryToken token);
    }
}

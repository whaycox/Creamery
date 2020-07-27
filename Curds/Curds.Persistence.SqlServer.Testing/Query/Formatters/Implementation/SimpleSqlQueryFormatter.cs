namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Text.Abstraction;
    using Tokens.Implementation;

    public class SimpleSqlQueryFormatter : BaseSqlQueryFormatter
    {
        public SimpleSqlQueryFormatter(IIndentStringBuilder stringBuilder)
            : base(stringBuilder)
        { }

        public override void VisitLiteral(LiteralSqlQueryToken token) { }
        public override void VisitTokenList(TokenListSqlQueryToken token) { }
        public override void VisitValueEntity(ValueEntitySqlQueryToken token) { }
    }
}

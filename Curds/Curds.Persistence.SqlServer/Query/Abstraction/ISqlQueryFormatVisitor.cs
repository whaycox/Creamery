namespace Curds.Persistence.Query.Abstraction
{
    using Tokens.Implementation;

    public interface ISqlQueryFormatVisitor
    {
        void VisitLiteral(LiteralSqlQueryToken token);
        void VisitTokenList(TokenListSqlQueryToken token);
        void VisitValueEntity(ValueEntitySqlQueryToken token);
    }
}

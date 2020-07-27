namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class LiteralSqlQueryToken : BaseSqlQueryToken
    {
        public abstract string Literal { get; }

        public sealed override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitLiteral(this);
    }
}

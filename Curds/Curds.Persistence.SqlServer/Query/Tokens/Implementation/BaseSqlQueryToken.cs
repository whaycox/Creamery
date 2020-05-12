namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class BaseSqlQueryToken : ISqlQueryToken
    {
        public abstract void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor);
    }
}

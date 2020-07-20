namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class BaseSqlQueryToken : ISqlQueryToken
    {
        protected ISqlQueryTokenFactory TokenFactory { get; }

        public BaseSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
        {
            TokenFactory = tokenFactory;
        }

        public abstract void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor);
    }
}

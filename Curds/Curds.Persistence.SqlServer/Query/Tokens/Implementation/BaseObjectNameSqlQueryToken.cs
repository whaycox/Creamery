namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public abstract class BaseObjectNameSqlQueryToken : RedirectedSqlQueryToken
    {
        public bool UseAlias { get; set; }

        public BaseObjectNameSqlQueryToken(ISqlQueryTokenFactory tokenFactory)
            : base(tokenFactory)
        { }
    }
}

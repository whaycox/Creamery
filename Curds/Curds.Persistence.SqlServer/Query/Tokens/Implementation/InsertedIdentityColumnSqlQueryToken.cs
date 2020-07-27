namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class InsertedIdentityColumnSqlQueryToken : RedirectedSqlQueryToken
    {
        public ISqlColumn Identity { get; }

        public InsertedIdentityColumnSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlColumn identity)
            : base(tokenFactory)
        {
            Identity = identity;
        }

        protected override ISqlQueryToken RedirectedToken() => TokenFactory.QualifiedObject(
            nameof(SqlQueryKeyword.inserted),
            Identity.Name);
    }
}

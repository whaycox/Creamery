namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class InsertedIdentityColumnSqlQueryToken : QualifiedObjectSqlQueryToken
    {
        public InsertedIdentityColumnSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlColumn identity)
            : base(
                tokenFactory,
                new ObjectNameSqlQueryToken(nameof(SqlQueryKeyword.inserted)),
                new ObjectNameSqlQueryToken(identity.Name))
        { }
    }
}

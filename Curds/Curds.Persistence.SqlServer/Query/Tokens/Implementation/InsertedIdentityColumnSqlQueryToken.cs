namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class InsertedIdentityColumnSqlQueryToken : QualifiedObjectSqlQueryToken
    {
        public InsertedIdentityColumnSqlQueryToken(ISqlColumn identity)
            : base(
                new ObjectNameSqlQueryToken(nameof(SqlQueryKeyword.inserted)),
                new ObjectNameSqlQueryToken(identity.Name))
        { }
    }
}

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class TableNameSqlQueryToken : BaseObjectNameSqlQueryToken
    {
        public ISqlTable Table { get; }

        public bool UseSqlName { get; set; }

        public TableNameSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlTable table)
            : base(tokenFactory)
        {
            Table = table;
        }

        protected override ISqlQueryToken GenerateNameToken() => UseAlias ? 
            WithAliasToken() : 
            WithoutAliasToken;
        private ISqlQueryToken WithAliasToken()
        {
            if (UseSqlName)
                return TokenFactory.Phrase(
                    new QualifiedObjectSqlQueryToken(
                        TokenFactory,
                        new ObjectNameSqlQueryToken(Table.Schema),
                        new ObjectNameSqlQueryToken(Table.Name)),
                    new ObjectNameSqlQueryToken(Table.Alias));
            else
                return new ObjectNameSqlQueryToken(Table.Alias);
        }
        private ISqlQueryToken WithoutAliasToken => new QualifiedObjectSqlQueryToken(
            TokenFactory,
            new ObjectNameSqlQueryToken(Table.Schema),
            new ObjectNameSqlQueryToken(Table.Name));
    }
}

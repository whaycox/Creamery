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

        protected override ISqlQueryToken RedirectedToken() =>
            UseAlias ?
                AliasedName :
                SqlName;
        private ISqlQueryToken AliasedName =>
            UseSqlName ?
                TokenFactory.Phrase(
                    SqlName,
                    Alias) :
                Alias;
        private ISqlQueryToken SqlName => TokenFactory.QualifiedObject(
            Table.Schema,
            Table.Name);
        private ISqlQueryToken Alias => TokenFactory.ObjectName(Table.Alias);
    }
}

using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class TableDefinitionSqlQueryToken : RedirectedSqlQueryToken
    {
        private ISqlTable Table { get; }

        public TableDefinitionSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlTable table)
            : base(tokenFactory)
        {
            Table = table;
        }

        protected override ISqlQueryToken RedirectedToken() => TokenFactory.Phrase(
            TokenFactory.TableName(Table, useAlias: false),
            TokenFactory.GroupedList(
                Table.Columns.Select(column => TokenFactory.ColumnDefinition(column))));
    }
}

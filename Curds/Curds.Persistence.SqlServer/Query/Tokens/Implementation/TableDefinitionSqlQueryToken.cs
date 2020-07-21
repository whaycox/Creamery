using Curds.Persistence.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;

    public class TableDefinitionSqlQueryToken : BaseSqlQueryToken
    {
        private ISqlTable Table { get; }

        public TableDefinitionSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ISqlTable table)
            : base(tokenFactory)
        {
            Table = table;
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            TokenFactory.Phrase(
                TokenFactory.TableName(Table, useAlias: false),
                TokenFactory.ColumnList(Table.Columns, true))
                .AcceptFormatVisitor(visitor);
        }
    }
}

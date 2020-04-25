using System;
using System.Collections.Generic;
using System.Data;

namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Curds.Persistence.Query.Abstraction;
    using Curds.Persistence.Query.Tokens.Implementation;
    using Text.Abstraction;

    public abstract class BaseSqlQueryFormatter : ISqlQueryFormatter, ISqlQueryFormatVisitor
    {
        protected static readonly Dictionary<SqlDbType, string> TypeMap = new Dictionary<SqlDbType, string>
        {
            { SqlDbType.NVarChar, "NVARCHAR(100)" },
            { SqlDbType.Bit, "BIT" },
            { SqlDbType.TinyInt, "TINYINT" },
            { SqlDbType.SmallInt, "SMALLINT" },
            { SqlDbType.Int, "INT" },
            { SqlDbType.BigInt, "BIGINT" },
            { SqlDbType.DateTime, "DATETIME" },
            { SqlDbType.DateTimeOffset, "DATETIMEOFFSET" },
            { SqlDbType.Decimal, "DECIMAL(10, 3)" },
            { SqlDbType.Float, "FLOAT" },
        };

        protected IIndentStringBuilder StringBuilder { get; }

        public BaseSqlQueryFormatter(IIndentStringBuilder stringBuilder)
        {
            StringBuilder = stringBuilder;
        }

        public string FormatTokens(IEnumerable<ISqlQueryToken> tokens)
        {
            foreach (ISqlQueryToken token in tokens)
            {
                token.AcceptFormatVisitor(this);
                StringBuilder.SetNewLine();
            }

            return StringBuilder.Flush();
        }

        public abstract void VisitLiteral(LiteralSqlQueryToken token);
        public abstract void VisitColumnList(ColumnListSqlQueryToken token);
        public abstract void VisitValueEntities(ValueEntitiesSqlQueryToken token);
        public abstract void VisitValueEntity(ValueEntitySqlQueryToken token);
    }
}

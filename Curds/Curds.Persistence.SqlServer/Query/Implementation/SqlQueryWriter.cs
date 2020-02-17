using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Curds.Persistence.Query.Implementation
{
    using Abstraction;
    using Curds.Persistence.Model.Domain;
    using Domain;

    internal class SqlQueryWriter : ISqlQueryWriter
    {
        private const string CREATE = nameof(CREATE);
        private const string DROP = nameof(DROP);
        private const string TABLE = nameof(TABLE);
        private const string INSERT = nameof(INSERT);
        private const string OUTPUT = nameof(OUTPUT);
        private const string INTO = nameof(INTO);
        private const string VALUES = nameof(VALUES);
        private const string SELECT = nameof(SELECT);
        private const string FROM = nameof(FROM);

        private static readonly Dictionary<SqlDbType, string> TypeNames = new Dictionary<SqlDbType, string>
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

        private StringBuilder QueryBuilder { get; } = new StringBuilder();

        private ISqlQueryParameterBuilder ParameterBuilder { get; }

        public SqlQueryWriter(ISqlQueryParameterBuilder parameterBuilder)
        {
            ParameterBuilder = parameterBuilder;
        }

        public SqlCommand Flush()
        {
            SqlCommand query = new SqlCommand(QueryBuilder.ToString());
            QueryBuilder.Clear();
            query.Parameters.AddRange(ParameterBuilder.Flush());
            return query;
        }

        private string FormatTableName(Table table) => $"{(string.IsNullOrWhiteSpace(table.Schema) ? string.Empty : $"[{table.Schema}].")}[{table.Name}]";
        private string FormatColumnName(Column column) => $"[{column.Name}]";
        private string FormatTemporaryIdentityName(Table table) => $"[#{table.Name}_Identities]";

        public void CreateTemporaryIdentityTable(Table table)
        {
            Column identityColumn = table.IdentityColumn;
            QueryBuilder.AppendLine($"{CREATE} {TABLE} {FormatTemporaryIdentityName(table)} ({FormatColumnName(identityColumn ?? throw new InvalidOperationException("No identity column configured"))} {TypeNames[identityColumn.SqlType]} NOT NULL)");
        }

        public void OutputIdentitiesToTemporaryTable(Table table)
        {
            Column identityColumn = table.IdentityColumn;
            QueryBuilder.AppendLine($"{OUTPUT} [inserted].[{identityColumn.Name}] {INTO} {FormatTemporaryIdentityName(table)}");
        }

        public void SelectTemporaryIdentityTable(Table table)
        {
            Column identityColumn = table.IdentityColumn;
            QueryBuilder.AppendLine($"{SELECT} [{identityColumn.Name}] {FROM} {FormatTemporaryIdentityName(table)}");
        }

        public void DropTemporaryIdentityTable(Table table) =>
            QueryBuilder.AppendLine($"{DROP} {TABLE} {FormatTemporaryIdentityName(table)}");

        public void Insert(Table table)
        {
            QueryBuilder.Append($"{INSERT} {FormatTableName(table)}");

            List<Column> eligibleColumns = table
                .Columns
                .Where(column => !column.IsIdentity)
                .ToList();
            if (eligibleColumns.Count == 1)
                WriteOneColumn(eligibleColumns.First());
            else
                WriteMultipleColumns(eligibleColumns);
        }
        private void WriteOneColumn(Column column) => QueryBuilder.AppendLine($" ({FormatColumnName(column)})");
        private void WriteMultipleColumns(List<Column> columns)
        {
            QueryBuilder.Append(Environment.NewLine);
            QueryBuilder.AppendLine("(");
            for (int i = 0; i < columns.Count; i++)
                QueryBuilder.AppendLine($"\t{(i == 0 ? " " : ",")}{FormatColumnName(columns[i])}");
            QueryBuilder.AppendLine(")");
        }

        public void ValueEntities(IEnumerable<ValueEntity> entities)
        {
            QueryBuilder.AppendLine(VALUES);
            int writtenEntities = 0;
            foreach (ValueEntity valueEntity in entities)
            {
                if (writtenEntities++ == 0)
                    AddValueEntity(valueEntity);
                else
                {
                    QueryBuilder.AppendLine(",");
                    AddValueEntity(valueEntity);
                }
            }
            QueryBuilder.AppendLine(string.Empty);
        }
        private void AddValueEntity(ValueEntity entity)
        {
            QueryBuilder.Append("(");
            for (int i = 0; i < entity.Values.Count; i++)
            {
                string paramName = ParameterBuilder.RegisterNewParamater(entity.Values[i]);
                if (i == 0)
                    QueryBuilder.Append($"@{paramName}");
                else
                    QueryBuilder.Append($", @{paramName}");
            }
            QueryBuilder.Append(")");
        }
    }
}

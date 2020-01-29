using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Domain;
    using Query.Domain;

    internal class SqlQueryWriter : ISqlQueryWriter
    {
        private const string INSERT = nameof(INSERT);
        private const string VALUES = nameof(VALUES);

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

        public void Insert(Table table)
        {
            QueryBuilder.AppendLine($"{INSERT} {FormatTableName(table)}");
            QueryBuilder.AppendLine("(");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (i == 0)
                    QueryBuilder.AppendLine($"\t{(table.Columns.Count > 1 ? " " : string.Empty)}{FormatColumnName(table.Columns[i])}");
                else
                    QueryBuilder.AppendLine($"\t,{FormatColumnName(table.Columns[i])}");
            }
            QueryBuilder.AppendLine(")");
        }

        public void ValueEntities(List<ValueEntity> entities)
        {
            QueryBuilder.AppendLine(VALUES);
            for (int i = 0; i < entities.Count; i++)
            {
                if (i == 0)
                    AddValueEntity(entities[i]);
                else
                {
                    QueryBuilder.AppendLine(",");
                    AddValueEntity(entities[i]);
                }
            }
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

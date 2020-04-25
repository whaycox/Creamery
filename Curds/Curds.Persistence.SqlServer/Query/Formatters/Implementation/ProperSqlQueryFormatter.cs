using System;

namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Curds.Persistence.Query.Tokens.Implementation;
    using Text.Abstraction;
    using Tokens.Implementation;
    using Query.Abstraction;
    using Model.Domain;

    public class ProperSqlQueryFormatter : BaseSqlQueryFormatter
    {
        public ProperSqlQueryFormatter(IIndentStringBuilder stringBuilder)
            : base(stringBuilder)
        { }

        public override void VisitLiteral(LiteralSqlQueryToken token) => StringBuilder.Append(token.Literal);

        public override void VisitColumnList(ColumnListSqlQueryToken token)
        {
            StringBuilder.SetNewLine();
            if (token.IncludeGrouping)
                StringBuilder.AppendLine("(");
            using (StringBuilder.CreateIndentScope())
            {
                if (token.Columns.Count == 1)
                    FormatColumn(token.Columns[0], token.IncludeDefinition);
                else
                {
                    for (int i = 0; i < token.Columns.Count; i++)
                    {
                        if (i == 0)
                            StringBuilder.Append(" ");
                        else
                            StringBuilder.Append(",");

                        FormatColumn(token.Columns[i], token.IncludeDefinition);

                        if (i < token.Columns.Count - 1)
                            StringBuilder.SetNewLine();
                    }
                }
            }
            if (token.IncludeGrouping)
            {
                StringBuilder.SetNewLine();
                StringBuilder.Append(")");
            }
        }
        private void FormatColumn(Column column, bool includeDefinition)
        {
            new ObjectNameSqlQueryToken(column.Name)
                .AcceptFormatVisitor(this);

            if (includeDefinition)
                StringBuilder.Append($" {TypeMap[column.SqlType]} NOT NULL");
        }

        public override void VisitValueEntities(ValueEntitiesSqlQueryToken token)
        {
            StringBuilder.SetNewLine();
            if (token.Entities.Count == 1)
                token.Entities[0].AcceptFormatVisitor(this);
            else
            {
                for (int i = 0; i < token.Entities.Count; i++)
                {
                    token.Entities[i].AcceptFormatVisitor(this);

                    if (i < token.Entities.Count - 1)
                        StringBuilder.AppendLine(",");
                }
            }
        }

        public override void VisitValueEntity(ValueEntitySqlQueryToken token)
        {
            StringBuilder.Append("(");
            for (int i = 0; i < token.Values.Count; i++)
            {
                token.Values[i].AcceptFormatVisitor(this);

                if (i < token.Values.Count - 1)
                    StringBuilder.Append(", ");
            }
            StringBuilder.Append(")");
        }
    }
}

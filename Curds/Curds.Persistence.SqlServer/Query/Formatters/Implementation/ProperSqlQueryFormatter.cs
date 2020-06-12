using System;

namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Curds.Persistence.Query.Tokens.Implementation;
    using Text.Abstraction;
    using Tokens.Implementation;
    using Query.Abstraction;
    using Model.Domain;
    using Model.Abstraction;

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
        private void FormatColumn(ISqlColumn column, bool includeDefinition)
        {
            new ObjectNameSqlQueryToken(column.Name)
                .AcceptFormatVisitor(this);

            if (includeDefinition)
                StringBuilder.Append($" {TypeMap[column.Type]} NOT NULL");
        }

        public override void VisitValueEntities(ValueEntitiesSqlQueryToken token)
        {
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
            StringBuilder.SetNewLine();
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

        public override void VisitBooleanCombination(BooleanCombinationSqlQueryToken token)
        {
            if (token.Elements.Count == 1)
            {
                StringBuilder.Append("(");
                token.Elements[0].AcceptFormatVisitor(this);
                StringBuilder.Append(")");
            }
            else
            {
                StringBuilder.AppendLine("(");
                using (StringBuilder.CreateIndentScope())
                    for (int i = 0; i < token.Elements.Count; i++)
                    {
                        if (i > 0)
                        {
                            token.Operation.AcceptFormatVisitor(this);
                            StringBuilder.SetNewLine();
                        }

                        token.Elements[i].AcceptFormatVisitor(this);
                        StringBuilder.SetNewLine();
                    }
                StringBuilder.AppendLine(")");
            }
        }

        public override void VisitSetValues(SetValuesSqlQueryToken token)
        {

            using (StringBuilder.CreateIndentScope())
            {
                if (token.SetValueTokens.Count == 1)
                    token.SetValueTokens[0].AcceptFormatVisitor(this);
                else
                {
                    for (int i = 0; i < token.SetValueTokens.Count; i++)
                    {
                        if (i == 0)
                            StringBuilder.Append(" ");
                        else
                            StringBuilder.Append(",");

                        token.SetValueTokens[i].AcceptFormatVisitor(this);

                        if (i < token.SetValueTokens.Count - 1)
                            StringBuilder.SetNewLine();
                    }
                }
            }
        }
    }
}

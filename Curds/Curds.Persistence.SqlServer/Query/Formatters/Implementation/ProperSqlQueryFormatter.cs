namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Curds.Persistence.Query.Tokens.Implementation;
    using Query.Abstraction;
    using Text.Abstraction;

    public class ProperSqlQueryFormatter : BaseSqlQueryFormatter
    {
        public ProperSqlQueryFormatter(IIndentStringBuilder stringBuilder)
            : base(stringBuilder)
        { }

        public override void VisitLiteral(LiteralSqlQueryToken token) => StringBuilder.Append(token.Literal);

        public override void VisitTokenList(TokenListSqlQueryToken token)
        {
            StringBuilder.SetNewLine();
            if (token.IncludeGrouping)
                StringBuilder.AppendLine("(");
            using (StringBuilder.CreateIndentScope())
            {
                if (token.Tokens.Count == 1)
                {
                    token.Tokens[0].AcceptFormatVisitor(this);
                }
                else
                {
                    for (int i = 0; i < token.Tokens.Count; i++)
                    {
                        if (token.IncludeSeparators)
                            StringBuilder.Append(Separator(i));

                        token.Tokens[i].AcceptFormatVisitor(this);

                        if (i < token.Tokens.Count - 1)
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
        private string Separator(int listIndex) => listIndex == 0 ? " " : ",";

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
    }
}

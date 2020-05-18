using Curds.Persistence.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Domain;

    public class BooleanCombinationSqlQueryToken : BaseSqlQueryToken
    {
        private static ISqlQueryToken AndToken { get; } = new KeywordSqlQueryToken(SqlQueryKeyword.AND);
        private static ISqlQueryToken OrToken { get; } = new KeywordSqlQueryToken(SqlQueryKeyword.OR);

        public ISqlQueryToken Operation { get; }
        public List<ISqlQueryToken> Elements { get; }

        public BooleanCombinationSqlQueryToken(
            BooleanCombination operation,
            IEnumerable<ISqlQueryToken> elements)
        {
            Operation = OperationKeyword(operation);
            Elements = elements.ToList();
        }
        private ISqlQueryToken OperationKeyword(BooleanCombination operation)
        {
            switch (operation)
            {
                case BooleanCombination.And:
                    return AndToken;
                case BooleanCombination.Or:
                    return OrToken;
                default:
                    throw new ArgumentException($"Unsupported operation: {operation}");
            }
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor) => visitor.VisitBooleanCombination(this);
    }
}

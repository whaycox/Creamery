using System;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class BooleanComparisonSqlQueryToken : RedirectedSqlQueryToken
    {
        private static Dictionary<BooleanComparison, SqlQueryKeyword> KeywordMap { get; } = new Dictionary<BooleanComparison, SqlQueryKeyword>
        {
            { BooleanComparison.Equals, SqlQueryKeyword.Equals },
            { BooleanComparison.NotEquals, SqlQueryKeyword.NotEquals },
            { BooleanComparison.GreaterThan, SqlQueryKeyword.GreaterThan },
            { BooleanComparison.GreaterThanOrEquals, SqlQueryKeyword.GreaterThanOrEquals },
            { BooleanComparison.LessThan, SqlQueryKeyword.LessThan },
            { BooleanComparison.LessThanOrEquals, SqlQueryKeyword.LessThanOrEquals },
        };

        public BooleanComparison Operation { get; }
        public ISqlQueryToken Left { get; }
        public ISqlQueryToken Right { get; }

        public BooleanComparisonSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            BooleanComparison operation,
            ISqlQueryToken left,
            ISqlQueryToken right)
            : base(tokenFactory)
        {
            Operation = operation;
            Left = left;
            Right = right;
        }

        protected override ISqlQueryToken RedirectedToken() => TokenFactory.Phrase(
            Left,
            OperationKeyword(),
            Right);
        private ISqlQueryToken OperationKeyword()
        {
            if (!KeywordMap.TryGetValue(Operation, out SqlQueryKeyword keyword))
                throw new InvalidOperationException($"Unsupported operation: {Operation}");
            return TokenFactory.Keyword(keyword);
        }
    }
}

using System;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class ArithmeticOperationSqlQueryToken : RedirectedSqlQueryToken
    {
        private static Dictionary<ArithmeticOperation, SqlQueryKeyword> KeywordMap { get; } = new Dictionary<ArithmeticOperation, SqlQueryKeyword>
        {
            { ArithmeticOperation.Equals, SqlQueryKeyword.Equals },
            { ArithmeticOperation.Modulo, SqlQueryKeyword.Modulo },
        };

        public ArithmeticOperation Operation { get; }
        public ISqlQueryToken Left { get; }
        public ISqlQueryToken Right { get; }

        public ArithmeticOperationSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ArithmeticOperation operation,
            ISqlQueryToken left,
            ISqlQueryToken right)
            : base(tokenFactory)
        {
            Left = left;
            Operation = operation;
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

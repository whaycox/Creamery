﻿using System;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class ArithmeticOperationSqlQueryToken : BaseSqlQueryToken
    {
        private static ISqlQueryToken EqualsToken { get; } = new ConstantSqlQueryToken(" = ");
        private static ISqlQueryToken ModuloToken { get; } = new ConstantSqlQueryToken(" % ");

        public ISqlQueryToken Left { get; }
        public ISqlQueryToken Operation { get; }
        public ISqlQueryToken Right { get; }

        public ArithmeticOperationSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            ArithmeticOperation operation,
            ISqlQueryToken left,
            ISqlQueryToken right)
            : base(tokenFactory)
        {
            Operation = OperationKeyword(operation);
            Left = left;
            Right = right;
        }
        private ISqlQueryToken OperationKeyword(ArithmeticOperation operation)
        {
            switch (operation)
            {
                case ArithmeticOperation.Equals:
                    return EqualsToken;
                case ArithmeticOperation.Modulo:
                    return ModuloToken;
                default:
                    throw new ArgumentException($"Unsupported operation: {operation}");
            }
        }

        public override void AcceptFormatVisitor(ISqlQueryFormatVisitor visitor)
        {
            Left.AcceptFormatVisitor(visitor);
            Operation.AcceptFormatVisitor(visitor);
            Right.AcceptFormatVisitor(visitor);
        }
    }
}

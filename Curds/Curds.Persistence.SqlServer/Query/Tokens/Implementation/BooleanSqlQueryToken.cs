using Curds.Persistence.Query.Abstraction;
using System;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Domain;

    public class BooleanSqlQueryToken : BaseSqlQueryToken
    {
        private static ConstantSqlQueryToken EqualsToken { get; } = new ConstantSqlQueryToken("=");
        private static ConstantSqlQueryToken NotEqualsToken { get; } = new ConstantSqlQueryToken("<>");
        private static ConstantSqlQueryToken GreaterThanToken { get; } = new ConstantSqlQueryToken(">");
        private static ConstantSqlQueryToken GreaterThanOrEqualsToken { get; } = new ConstantSqlQueryToken(">=");
        private static ConstantSqlQueryToken LessThanToken { get; } = new ConstantSqlQueryToken("<");
        private static ConstantSqlQueryToken LessThanOrEqualsToken { get; } = new ConstantSqlQueryToken("<=");

        public ISqlQueryToken Left { get; }
        public ISqlQueryToken Operation { get; }
        public ISqlQueryToken Right { get; }

        public BooleanSqlQueryToken(
            SqlBooleanOperation operation,
            ISqlQueryToken left,
            ISqlQueryToken right)
        {
            Operation = OperationConstant(operation);
            Left = left;
            Right = right;
        }
        private ConstantSqlQueryToken OperationConstant(SqlBooleanOperation operation)
        {
            switch (operation)
            {
                case SqlBooleanOperation.Equals:
                    return EqualsToken;
                case SqlBooleanOperation.NotEquals:
                    return NotEqualsToken;
                case SqlBooleanOperation.GreaterThan:
                    return GreaterThanToken;
                case SqlBooleanOperation.GreaterThanOrEquals:
                    return GreaterThanOrEqualsToken;
                case SqlBooleanOperation.LessThan:
                    return LessThanToken;
                case SqlBooleanOperation.LessThanOrEquals:
                    return LessThanOrEqualsToken;
                default:
                    throw new ArgumentException($"Unsupported operation {operation}");
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

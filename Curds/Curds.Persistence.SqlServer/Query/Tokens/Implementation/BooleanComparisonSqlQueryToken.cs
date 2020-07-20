using System;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Query.Abstraction;
    using Query.Domain;

    public class BooleanComparisonSqlQueryToken : BaseSqlQueryToken
    {
        private static ConstantSqlQueryToken EqualsToken { get; } = new ConstantSqlQueryToken(" = ");
        private static ConstantSqlQueryToken NotEqualsToken { get; } = new ConstantSqlQueryToken(" <> ");
        private static ConstantSqlQueryToken GreaterThanToken { get; } = new ConstantSqlQueryToken(" > ");
        private static ConstantSqlQueryToken GreaterThanOrEqualsToken { get; } = new ConstantSqlQueryToken(" >= ");
        private static ConstantSqlQueryToken LessThanToken { get; } = new ConstantSqlQueryToken(" < ");
        private static ConstantSqlQueryToken LessThanOrEqualsToken { get; } = new ConstantSqlQueryToken(" <= ");

        public ISqlQueryToken Left { get; }
        public ISqlQueryToken Operation { get; }
        public ISqlQueryToken Right { get; }

        public BooleanComparisonSqlQueryToken(
            ISqlQueryTokenFactory tokenFactory,
            BooleanComparison operation,
            ISqlQueryToken left,
            ISqlQueryToken right)
            : base(tokenFactory)
        {
            Operation = OperationConstant(operation);
            Left = left;
            Right = right;
        }
        private ConstantSqlQueryToken OperationConstant(BooleanComparison operation)
        {
            switch (operation)
            {
                case BooleanComparison.Equals:
                    return EqualsToken;
                case BooleanComparison.NotEquals:
                    return NotEqualsToken;
                case BooleanComparison.GreaterThan:
                    return GreaterThanToken;
                case BooleanComparison.GreaterThanOrEqual:
                    return GreaterThanOrEqualsToken;
                case BooleanComparison.LessThan:
                    return LessThanToken;
                case BooleanComparison.LessThanOrEqual:
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

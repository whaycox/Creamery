using Curds.Persistence.Query.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Query.Tokens.Implementation
{
    using Domain;

    public class BooleanSqlQueryToken : BaseSqlQueryToken
    {
        private static ConstantSqlQueryToken Equals { get; } = new ConstantSqlQueryToken("=");

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
                    return Equals;
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

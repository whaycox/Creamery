using System;

namespace Curds.Expressions.Implementation
{
    internal class SimpleBaseExpressionVisitor : BaseExpressionVisitor<object>
    {
        public override object Build() => throw new NotImplementedException();
    }
}

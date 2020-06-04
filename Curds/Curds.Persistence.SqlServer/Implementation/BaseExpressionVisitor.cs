using System;
using System.Collections.Generic;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Curds.Persistence.ExpressionNodes.Implementation;
    using ExpressionNodes.Implementation;

    internal abstract class BaseExpressionVisitor<TReturn> : IExpressionVisitor<TReturn>
    {
        public virtual TReturn VisitConstant(ConstantNode<TReturn> constantNode) => throw new NotImplementedException();
        public virtual TReturn VisitConvert(ConvertNode<TReturn> convertNode) => throw new NotImplementedException();
        public virtual TReturn VisitEqual(EqualNode<TReturn> equalNode) => throw new NotImplementedException();
        public virtual TReturn VisitLessThan(LessThanNode<TReturn> lessThanNode) => throw new NotImplementedException();
        public virtual TReturn VisitLessThanOrEqual(LessThanOrEqualNode<TReturn> lessThanOrEqualNode) => throw new NotImplementedException();
        public virtual TReturn VisitLambda(LambdaNode<TReturn> lambdaNode) => throw new NotImplementedException();
        public virtual TReturn VisitMemberAccess(MemberAccessNode<TReturn> memberAccessNode) => throw new NotImplementedException();
        public virtual TReturn VisitParameter(ParameterNode<TReturn> parameterNode) => throw new NotImplementedException();
        public virtual TReturn VisitModulo(ModuloNode<TReturn> moduloNode) => throw new NotImplementedException();
    }
}

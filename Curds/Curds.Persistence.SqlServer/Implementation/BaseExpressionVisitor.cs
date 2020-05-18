using System;
using System.Collections.Generic;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Curds.Persistence.ExpressionNodes.Implementation;
    using ExpressionNodes.Implementation;

    internal abstract class BaseExpressionVisitor<TReturn, TContext> : IExpressionVisitor<TReturn, TContext>
    {
        protected Stack<object> VisitationState { get; } = new Stack<object>();

        public virtual TReturn VisitConstant(TContext context, ConstantNode<TReturn, TContext> constantNode) => throw new NotImplementedException();
        public virtual TReturn VisitConvert(TContext context, ConvertNode<TReturn, TContext> convertNode) => throw new NotImplementedException();
        public virtual TReturn VisitEqual(TContext context, EqualNode<TReturn, TContext> equalNode) => throw new NotImplementedException();
        public virtual TReturn VisitLessThan(TContext context, LessThanNode<TReturn, TContext> lessThanNode) => throw new NotImplementedException();
        public virtual TReturn VisitLessThanOrEqual(TContext context, LessThanOrEqualNode<TReturn, TContext> lessThanOrEqualNode) => throw new NotImplementedException();
        public virtual TReturn VisitLambda(TContext context, LambdaNode<TReturn, TContext> lambdaNode) => throw new NotImplementedException();
        public virtual TReturn VisitMemberAccess(TContext context, MemberAccessNode<TReturn, TContext> memberAccessNode) => throw new NotImplementedException();
        public virtual TReturn VisitParameter(TContext context, ParameterNode<TReturn, TContext> parameterNode) => throw new NotImplementedException();
        public virtual TReturn VisitModulo(TContext context, ModuloNode<TReturn, TContext> moduloNode) => throw new NotImplementedException();
    }
}

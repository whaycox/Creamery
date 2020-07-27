using System;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using ExpressionNodes.Domain;

    internal abstract class BaseExpressionVisitor<TReturn> : IExpressionVisitor<TReturn>
    {
        public virtual TReturn VisitConstant(ConstantNode constantNode) => throw new NotImplementedException();
        public virtual TReturn VisitConvert(ConvertNode convertNode) => throw new NotImplementedException();
        public virtual TReturn VisitEqual(EqualNode equalNode) => throw new NotImplementedException();
        public virtual TReturn VisitNotEqual(NotEqualNode notEqualNode) => throw new NotImplementedException();
        public virtual TReturn VisitLessThan(LessThanNode lessThanNode) => throw new NotImplementedException();
        public virtual TReturn VisitLessThanOrEqual(LessThanOrEqualNode lessThanOrEqualNode) => throw new NotImplementedException();
        public virtual TReturn VisitLambda(LambdaNode lambdaNode) => throw new NotImplementedException();
        public virtual TReturn VisitMemberAccess(MemberAccessNode memberAccessNode) => throw new NotImplementedException();
        public virtual TReturn VisitParameter(ParameterNode parameterNode) => throw new NotImplementedException();
        public virtual TReturn VisitModulo(ModuloNode moduloNode) => throw new NotImplementedException();
    }
}

using System;

namespace Curds.Expressions.Implementation
{
    using Abstraction;
    using Nodes.Domain;

    public abstract class BaseExpressionVisitor<TReturn> : IExpressionVisitor<TReturn>
    {
        public virtual void VisitConstant(ConstantNode constantNode) => throw new NotImplementedException();
        public virtual void VisitConvert(ConvertNode convertNode) => throw new NotImplementedException();
        public virtual void VisitEqual(EqualNode equalNode) => throw new NotImplementedException();
        public virtual void VisitNotEqual(NotEqualNode notEqualNode) => throw new NotImplementedException();
        public virtual void VisitLessThan(LessThanNode lessThanNode) => throw new NotImplementedException();
        public virtual void VisitLessThanOrEqual(LessThanOrEqualNode lessThanOrEqualNode) => throw new NotImplementedException();
        public virtual void VisitGreaterThan(GreaterThanNode greaterThanNode) => throw new NotImplementedException();
        public virtual void VisitGreaterThanOrEqual(GreaterThanOrEqualNode greaterThanOrEqualNode) => throw new NotImplementedException();
        public virtual void VisitLambda(LambdaNode lambdaNode) => throw new NotImplementedException();
        public virtual void VisitMemberAccess(MemberAccessNode memberAccessNode) => throw new NotImplementedException();
        public virtual void VisitParameter(ParameterNode parameterNode) => throw new NotImplementedException();
        public virtual void VisitModulo(ModuloNode moduloNode) => throw new NotImplementedException();

        public abstract TReturn Build();
    }
}

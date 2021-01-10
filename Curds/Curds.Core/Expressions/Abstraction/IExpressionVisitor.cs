namespace Curds.Expressions.Abstraction
{
    using Nodes.Domain;

    public interface IExpressionVisitor
    {
        void VisitLambda(LambdaNode lambdaNode);
        void VisitEqual(EqualNode equalNode);
        void VisitNotEqual(NotEqualNode notEqualNode);
        void VisitLessThan(LessThanNode lessThanNode);
        void VisitLessThanOrEqual(LessThanOrEqualNode lessThanOrEqualNode);
        void VisitGreaterThan(GreaterThanNode greaterThanNode);
        void VisitGreaterThanOrEqual(GreaterThanOrEqualNode greaterThanOrEqualNode);
        void VisitMemberAccess(MemberAccessNode memberAccessNode);
        void VisitConvert(ConvertNode convertNode);
        void VisitParameter(ParameterNode parameterNode);
        void VisitConstant(ConstantNode constantNode);
        void VisitModulo(ModuloNode moduloNode);
    }
    
    public interface IExpressionVisitor<TReturn> : IExpressionVisitor
    {
        TReturn Build();
    }
}

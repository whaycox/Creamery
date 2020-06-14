namespace Curds.Persistence.Abstraction
{
    using ExpressionNodes.Domain;

    public interface IExpressionVisitor<TReturn>
    {
        TReturn VisitLambda(LambdaNode lambdaNode);
        TReturn VisitEqual(EqualNode equalNode);
        TReturn VisitLessThan(LessThanNode lessThanNode);
        TReturn VisitLessThanOrEqual(LessThanOrEqualNode lessThanOrEqualNode);
        TReturn VisitMemberAccess(MemberAccessNode memberAccessNode);
        TReturn VisitConvert(ConvertNode convertNode);
        TReturn VisitParameter(ParameterNode parameterNode);
        TReturn VisitConstant(ConstantNode constantNode);
        TReturn VisitModulo(ModuloNode moduloNode);
    }
}

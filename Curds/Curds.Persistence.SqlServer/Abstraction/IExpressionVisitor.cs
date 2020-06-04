namespace Curds.Persistence.Abstraction
{
    using ExpressionNodes.Implementation;

    internal interface IExpressionVisitor<TReturn>
    {
        TReturn VisitLambda(LambdaNode<TReturn> lambdaNode);
        TReturn VisitEqual(EqualNode<TReturn> equalNode);
        TReturn VisitLessThan(LessThanNode<TReturn> lessThanNode);
        TReturn VisitLessThanOrEqual(LessThanOrEqualNode<TReturn> lessThanOrEqualNode);
        TReturn VisitMemberAccess(MemberAccessNode<TReturn> memberAccessNode);
        TReturn VisitConvert(ConvertNode<TReturn> convertNode);
        TReturn VisitParameter(ParameterNode<TReturn> parameterNode);
        TReturn VisitConstant(ConstantNode<TReturn> constantNode);
        TReturn VisitModulo(ModuloNode<TReturn> moduloNode);
    }
}

namespace Curds.Persistence.Abstraction
{
    using ExpressionNodes.Implementation;

    internal interface IExpressionVisitor<TReturn, TContext>
    {
        TReturn VisitLambda(TContext context, LambdaNode<TReturn, TContext> lambdaNode);
        TReturn VisitEqual(TContext context, EqualNode<TReturn, TContext> equalNode);
        TReturn VisitLessThan(TContext context, LessThanNode<TReturn, TContext> lessThanNode);
        TReturn VisitLessThanOrEqual(TContext context, LessThanOrEqualNode<TReturn, TContext> lessThanOrEqualNode);
        TReturn VisitMemberAccess(TContext context, MemberAccessNode<TReturn, TContext> memberAccessNode);
        TReturn VisitConvert(TContext context, ConvertNode<TReturn, TContext> convertNode);
        TReturn VisitParameter(TContext context, ParameterNode<TReturn, TContext> parameterNode);
        TReturn VisitConstant(TContext context, ConstantNode<TReturn, TContext> constantNode);
        TReturn VisitModulo(TContext context, ModuloNode<TReturn, TContext> moduloNode);
    }
}

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Implementation
{
    using Nodes.Domain;

    internal class PropertySelectionVisitor : BaseExpressionVisitor<PropertyInfo>
    {
        private ParameterExpression LambdaParameter { get; set; }
        private PropertyInfo SelectedProperty { get; set; }

        public override void VisitLambda(LambdaNode lambdaNode)
        {
            if (SelectedProperty != null)
                throw new InvalidOperationException("Cannot visit twice with the same visitor");

            LambdaParameter = lambdaNode
               .SourceExpression
               .Parameters
               .Single();
            lambdaNode
                .Body
                .AcceptVisitor(this);
        }

        public override void VisitMemberAccess(MemberAccessNode memberAccessNode)
        {
            if (LambdaParameter != memberAccessNode.SourceExpression.Expression)
                throw new InvalidOperationException("Property selection must be lambda parameter");

            MemberInfo memberInfo = memberAccessNode
                .SourceExpression
                .Member;
            if (!(memberInfo is PropertyInfo))
                throw new InvalidOperationException("Must select a property");

            SelectedProperty = memberInfo as PropertyInfo;
        }

        public override PropertyInfo Build()
        {
            if (SelectedProperty == null)
                throw new InvalidOperationException("Cannot build the result without visiting first");
            return SelectedProperty;
        }
    }
}

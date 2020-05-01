namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;

    internal class DelegateMapper : IDelegateMapper
    {
        private IValueExpressionBuilder ValueExpressionBuilder { get; }
        private IAssignIdentityExpressionBuilder AssignIdentityExpressionBuilder { get; }
        private IProjectEntityExpressionBuilder ProjectEntityExpressionBuilder { get; }

        public DelegateMapper(
            IValueExpressionBuilder valueExpressionBuilder,
            IAssignIdentityExpressionBuilder assignIdentityExpressionBuilder,
            IProjectEntityExpressionBuilder projectEntityExpressionBuilder)
        {
            ValueExpressionBuilder = valueExpressionBuilder;
            AssignIdentityExpressionBuilder = assignIdentityExpressionBuilder;
            ProjectEntityExpressionBuilder = projectEntityExpressionBuilder;
        }

        public ValueEntityDelegate MapValueEntityDelegate(IEntityModel entityModel) => ValueExpressionBuilder.BuildValueEntityDelegate(entityModel);
        public AssignIdentityDelegate MapAssignIdentityDelegate(IEntityModel entityModel) => AssignIdentityExpressionBuilder.BuildAssignIdentityDelegate(entityModel);
        public ProjectEntityDelegate MapProjectEntityDelegate(IEntityModel entityModel) => ProjectEntityExpressionBuilder.BuildProjectEntityDelegate(entityModel);
    }
}

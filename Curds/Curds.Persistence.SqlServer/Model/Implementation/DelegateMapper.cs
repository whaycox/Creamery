using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Configuration.Abstraction;
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Query.Domain;
    using Configuration.Domain;

    internal class DelegateMapper : IDelegateMapper
    {
        private IValueExpressionBuilder ValueExpressionBuilder { get; }
        private IAssignIdentityExpressionBuilder AssignIdentityExpressionBuilder { get; }
        private ITypeMapper TypeMapper { get; }
        private IModelConfigurationFactory ConfigurationFactory { get; }

        public DelegateMapper(
            IValueExpressionBuilder valueExpressionBuilder,
            IAssignIdentityExpressionBuilder assignIdentityExpressionBuilder,
            ITypeMapper typeMapper,
            IModelConfigurationFactory configurationFactory)
        {
            ValueExpressionBuilder = valueExpressionBuilder;
            AssignIdentityExpressionBuilder = assignIdentityExpressionBuilder;
            TypeMapper = typeMapper;
            ConfigurationFactory = configurationFactory;
        }

        public ValueEntityDelegate MapValueEntityDelegate<TModel>(Type entityType)
            where TModel : IDataModel
        {
            IEnumerable<PropertyInfo> valueProperties = TypeMapper
                .ValueTypes(entityType);

            CompiledConfiguration<TModel> entityConfiguration = ConfigurationFactory.Build<TModel>(entityType);
            foreach (CompiledColumnConfiguration<TModel> configuredColumn in entityConfiguration.Columns.Values)
                if (configuredColumn.IsIdentity)
                    valueProperties = valueProperties.Where(property => property.Name != configuredColumn.ValueName);

            return ValueExpressionBuilder.BuildValueEntityDelegate(entityType, valueProperties);
        }

        public AssignIdentityDelegate MapAssignIdentityDelegate<TModel>(Type entityType)
            where TModel : IDataModel
        {
            CompiledConfiguration<TModel> entityConfiguration = ConfigurationFactory.Build<TModel>(entityType);
            CompiledColumnConfiguration<TModel> identityConfiguration = entityConfiguration.Columns.First(column => column.Value.IsIdentity).Value;
            PropertyInfo identityProperty = TypeMapper.ValueTypes(entityType).Where(property => property.Name == identityConfiguration.ValueName).First();
            return AssignIdentityExpressionBuilder.BuildAssignIdentityDelegate(entityType, identityProperty);
        }
    }
}

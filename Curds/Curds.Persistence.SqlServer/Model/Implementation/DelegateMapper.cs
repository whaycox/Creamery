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
        private IValueExpressionBuilder ExpressionBuilder { get; }
        private ITypeMapper TypeMapper { get; }
        private IModelConfigurationFactory ConfigurationFactory { get; }

        public DelegateMapper(
            IValueExpressionBuilder expressionBuilder,
            ITypeMapper typeMapper,
            IModelConfigurationFactory configurationFactory)
        {
            ExpressionBuilder = expressionBuilder;
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

            return ExpressionBuilder.BuildValueEntityDelegate(entityType, valueProperties);
        }
    }
}

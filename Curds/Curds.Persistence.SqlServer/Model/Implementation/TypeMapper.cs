using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Domain;
    using Domain;
    using Configuration.Abstraction;
    using Configuration.Domain;
    using Curds.Persistence.Domain;

    internal class TypeMapper : ITypeMapper
    {
        private const string InvalidModelPropertiesMessage = "All model properties must be ITable<SomeEntity>";

        private IModelConfigurationFactory ModelConfigurationFactory { get; }

        public TypeMapper(IModelConfigurationFactory modelConfigurationFactory)
        {
            ModelConfigurationFactory = modelConfigurationFactory;
        }

        public IEnumerable<(string tableName, Type tableType)> TableTypes<TModel>()
            where TModel : IDataModel => ModelProperties(typeof(TModel))
                .Select(property => ProcessTableType(property))
                .ToList();
        private IEnumerable<PropertyInfo> ModelProperties(Type modelType)
        {
            if (!modelType.IsInterface)
                throw new ModelException("All data models must be interfaces");
            return modelType.GetProperties();
        }
        private (string tableName, Type tableType) ProcessTableType(PropertyInfo propertyInfo) => (propertyInfo.Name, ParseModelPropertyToTableType(propertyInfo));
        private Type ParseModelPropertyToTableType(PropertyInfo propertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            if (!propertyType.IsGenericType)
                throw new ModelException(InvalidModelPropertiesMessage);
            Type genericType = propertyType.GetGenericTypeDefinition();
            if (genericType != typeof(ITable<>))
                throw new ModelException(InvalidModelPropertiesMessage);

            return propertyType.GenericTypeArguments[0];
        }

        public IEnumerable<PropertyInfo> ValueTypes(Type entityType) => entityType
            .GetProperties()
            .Where(property => property.CanRead && property.CanWrite)
            .OrderBy(property => property.Name);

        public Table MapTable<TModel>(Type entityType)
            where TModel : IDataModel
        {
            IModelEntityConfiguration configuration = ModelConfigurationFactory.Build<TModel>(entityType);
            Table table = new Table
            {
                Schema = configuration.Schema,
                Name = configuration.Table,
            };
            foreach (PropertyInfo propertyInfo in ValueTypes(entityType))
                table.Columns.Add(BuildColumn(propertyInfo, configuration));

            return table;
        }
        private Column BuildColumn(PropertyInfo propertyInfo, IModelEntityConfiguration configuration) => new Column
        {
            Name = propertyInfo.Name,
            IsIdentity = propertyInfo.Name == configuration.Identity,
        };
    }
}

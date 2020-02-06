using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;

    internal class TypeMapper : ITypeMapper
    {
        private const string InvalidModelPropertiesMessage = "All model properties must be ITable<SomeEntity>";

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
    }
}

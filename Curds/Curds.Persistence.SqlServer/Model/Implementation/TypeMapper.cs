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
        private const string InvalidModelPropertiesMessage = "All model properties must be IEntity types";

        public IEnumerable<Type> EntityTypes<TModel>()
            where TModel : IDataModel => ModelProperties(typeof(TModel))
                .Select(property => ParseModelPropertyToTableType(property))
                .ToList();
        private IEnumerable<PropertyInfo> ModelProperties(Type modelType)
        {
            if (!modelType.IsInterface)
                throw new ModelException("All data models must be interfaces");
            return modelType.GetProperties();
        }
        private Type ParseModelPropertyToTableType(PropertyInfo propertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            if (!typeof(IEntity).IsAssignableFrom(propertyType))
                throw new ModelException(InvalidModelPropertiesMessage);

            return propertyType;
        }

        public IEnumerable<PropertyInfo> ValueTypes(Type entityType) => entityType
            .GetProperties()
            .Where(property => property.CanRead && property.CanWrite)
            .OrderBy(property => property.Name);
    }
}

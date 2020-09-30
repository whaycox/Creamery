using System;

namespace Curds.Persistence.Model
{
    public static class TypeExtensions
    {
        public static Type ResolveUnderlyingType(this Type propertyType)
        {
            if (propertyType.IsGenericType)
                propertyType = ResolveGenericType(propertyType);
            if (propertyType.IsEnum)
                propertyType = Enum.GetUnderlyingType(propertyType);

            return propertyType;

        }
        private static Type ResolveGenericType(Type propertyType)
        {
            Type baseGeneric = propertyType.GetGenericTypeDefinition();
            if (baseGeneric != typeof(Nullable<>))
                throw new InvalidOperationException("Only Nullable<> generic types are supported");

            return propertyType.GetGenericArguments()[0];
        }
    }
}

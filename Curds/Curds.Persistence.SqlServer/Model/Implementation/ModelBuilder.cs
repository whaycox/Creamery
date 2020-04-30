using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Data;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Domain;
    using Configuration.Abstraction;
    using Domain;
    using Configuration.Domain;

    internal class ModelBuilder : IModelBuilder
    {
        private static Dictionary<Type, SqlDbType> ColumnTypeMap { get; } = new Dictionary<Type, SqlDbType>
        {
            { typeof(string), SqlDbType.NVarChar },
            { typeof(bool), SqlDbType.Bit },
            { typeof(bool?), SqlDbType.Bit },
            { typeof(byte), SqlDbType.TinyInt },
            { typeof(byte?), SqlDbType.TinyInt },
            { typeof(short), SqlDbType.SmallInt },
            { typeof(short?), SqlDbType.SmallInt },
            { typeof(int), SqlDbType.Int },
            { typeof(int?), SqlDbType.Int },
            { typeof(long), SqlDbType.BigInt },
            { typeof(long?), SqlDbType.BigInt },
            { typeof(DateTime), SqlDbType.DateTime },
            { typeof(DateTime?), SqlDbType.DateTime },
            { typeof(DateTimeOffset), SqlDbType.DateTimeOffset },
            { typeof(DateTimeOffset?), SqlDbType.DateTimeOffset },
            { typeof(decimal), SqlDbType.Decimal },
            { typeof(decimal?), SqlDbType.Decimal },
            { typeof(double), SqlDbType.Float },
            { typeof(double?), SqlDbType.Float },
        };

        private IModelConfigurationFactory ConfigurationFactory { get; }
        private ITypeMapper TypeMapper { get; }
        private IDelegateMapper DelegateMapper { get; }

        public ModelBuilder(
            IModelConfigurationFactory configurationFactory,
            ITypeMapper typeMapper,
            IDelegateMapper delegateMapper)
        {
            ConfigurationFactory = configurationFactory;
            TypeMapper = typeMapper;
            DelegateMapper = delegateMapper;
        }

        public IEnumerable<IEntityModel> BuildEntityModels<TModel>() 
            where TModel : IDataModel
        {
            foreach (Type entityType in TypeMapper.EntityTypes<TModel>())
                yield return BuildEntityModel<TModel>(entityType);
        }
        private IEntityModel BuildEntityModel<TModel>(Type entityType)
            where TModel : IDataModel
        {
            CompiledConfiguration<TModel> entityConfiguration = ConfigurationFactory.Build<TModel>(entityType);
            EntityModel entityModel = new EntityModel(entityType)
            {
                Schema = entityConfiguration.Schema,
                Table = entityConfiguration.Table,
            };
            foreach (PropertyInfo propertyInfo in TypeMapper.ValueTypes(entityType))
            {
                ValueModel valueConfiguration = BuildDefaultColumn(propertyInfo);

                if (entityConfiguration.Columns.TryGetValue(propertyInfo.Name, out CompiledColumnConfiguration<TModel> configuredColumn))
                {
                    valueConfiguration.Name = configuredColumn.Name ?? valueConfiguration.Name;
                    valueConfiguration.IsIdentity = configuredColumn.IsIdentity;
                }
                entityModel.ValueModels.Add(valueConfiguration);
            }

            entityModel.ValueEntity = DelegateMapper.MapValueEntityDelegate(entityModel);
            entityModel.AssignIdentity = DelegateMapper.MapAssignIdentityDelegate(entityModel);
            entityModel.ProjectEntity = DelegateMapper.MapProjectEntityDelegate(entityModel);

            return entityModel;
        }

        private ValueModel BuildDefaultColumn(PropertyInfo propertyInfo) => new ValueModel
        {
            Name = propertyInfo.Name,
            Property = propertyInfo,
            SqlType = ColumnTypeMap[propertyInfo.PropertyType],
        };
    }
}

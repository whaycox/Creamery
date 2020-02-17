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
        private static readonly Dictionary<Type, SqlDbType> ColumnTypeMap = new Dictionary<Type, SqlDbType>
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

        private Table BuildTable<TModel>(Type entityType)
            where TModel : IDataModel
        {
            CompiledConfiguration<TModel> configuration = ConfigurationFactory.Build<TModel>(entityType);
            Table table = new Table
            {
                Schema = configuration.Schema,
                Name = configuration.Table,
            };
            foreach (PropertyInfo propertyInfo in TypeMapper.ValueTypes(entityType))
            {
                Column valueColumn = BuildDefaultColumn(propertyInfo);

                if (configuration.Columns.TryGetValue(propertyInfo.Name, out CompiledColumnConfiguration<TModel> configuredColumn))
                {
                    valueColumn.Name = configuredColumn.Name ?? valueColumn.Name;
                    valueColumn.IsIdentity = configuredColumn.IsIdentity;
                }
                table.Columns.Add(valueColumn);
            }
            return table;
        }

        public Column BuildDefaultColumn(PropertyInfo propertyInfo) => new Column 
        { 
            Name = propertyInfo.Name,
            SqlType = ColumnTypeMap[propertyInfo.PropertyType],
        };

        public Dictionary<Type, Table> TablesByType<TModel>()
            where TModel : IDataModel
        {
            Dictionary<Type, Table> tables = new Dictionary<Type, Table>();
            foreach (Type entityType in TypeMapper.TableTypes<TModel>())
                tables.Add(entityType, BuildTable<TModel>(entityType));
            return tables;
        }

        public Dictionary<Type, ValueEntityDelegate> ValueEntityDelegatesByType<TModel>()
            where TModel : IDataModel
        {
            Dictionary<Type, ValueEntityDelegate> valueEntityDelegates = new Dictionary<Type, ValueEntityDelegate>();
            foreach (Type entityType in TypeMapper.TableTypes<TModel>())
                valueEntityDelegates.Add(entityType, DelegateMapper.MapValueEntityDelegate<TModel>(entityType));
            return valueEntityDelegates;
        }

        public Dictionary<Type, AssignIdentityDelegate> AssignIdentityDelegatesByType<TModel>() 
            where TModel : IDataModel
        {
            Dictionary<Type, AssignIdentityDelegate> assignIdentityDelegates = new Dictionary<Type, AssignIdentityDelegate>();
            foreach (Type entityType in TypeMapper.TableTypes<TModel>())
                if (TypeHasIdentityColumn<TModel>(entityType))
                    assignIdentityDelegates.Add(entityType, DelegateMapper.MapAssignIdentityDelegate<TModel>(entityType));
            return assignIdentityDelegates;
        }
        private bool TypeHasIdentityColumn<TModel>(Type entityType)
            where TModel : IDataModel
        {
            CompiledConfiguration<TModel> entityConfiguration = ConfigurationFactory.Build<TModel>(entityType);
            return entityConfiguration.Columns.Any(column => column.Value.IsIdentity);
        }
    }
}

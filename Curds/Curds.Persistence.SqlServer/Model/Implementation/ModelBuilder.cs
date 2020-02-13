using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

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
                Column valueColumn = new Column { Name = propertyInfo.Name };

                if (configuration.Columns.TryGetValue(propertyInfo.Name, out CompiledColumnConfiguration<TModel> configuredColumn))
                {
                    valueColumn.Name = configuredColumn.Name ?? valueColumn.Name;
                    valueColumn.IsIdentity = configuredColumn.IsIdentity;
                }
                table.Columns.Add(valueColumn);
            }
            return table;
        }

        public Dictionary<string, Table> TablesByName<TModel>()
            where TModel : IDataModel
        {
            Dictionary<string, Table> tables = new Dictionary<string, Table>();
            foreach (var tableEntityTypes in TypeMapper.TableTypes<TModel>())
                tables.Add(tableEntityTypes.tableName, BuildTable<TModel>(tableEntityTypes.tableType));
            return tables;
        }

        public Dictionary<Type, Table> TablesByType<TModel>()
            where TModel : IDataModel
        {
            Dictionary<Type, Table> tables = new Dictionary<Type, Table>();
            foreach (var tableEntityTypes in TypeMapper.TableTypes<TModel>())
                tables.Add(tableEntityTypes.tableType, BuildTable<TModel>(tableEntityTypes.tableType));
            return tables;
        }

        public Dictionary<Type, ValueEntityDelegate> ValueEntityDelegatesByType<TModel>()
            where TModel : IDataModel
        {
            Dictionary<Type, ValueEntityDelegate> valueEntityDelegates = new Dictionary<Type, ValueEntityDelegate>();
            foreach (var tableType in TypeMapper.TableTypes<TModel>())
                valueEntityDelegates.Add(tableType.tableType, DelegateMapper.MapValueEntityDelegate<TModel>(tableType.tableType));
            return valueEntityDelegates;
        }
    }
}

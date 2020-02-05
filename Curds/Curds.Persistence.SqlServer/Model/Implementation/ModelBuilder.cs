using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Curds.Persistence.Model.Implementation
{
    using Abstraction;
    using Persistence.Abstraction;
    using Query.Domain;
    using Configuration.Abstraction;

    public class ModelBuilder : IModelBuilder
    {
        private ITypeMapper TypeMapper { get; }
        private IDelegateMapper DelegateMapper { get; }
        private IModelConfigurationFactory ConfigurationFactory { get; }

        public ModelBuilder(
            ITypeMapper typeMapper,
            IDelegateMapper delegateMapper,
            IModelConfigurationFactory configurationFactory)
        {
            TypeMapper = typeMapper;
            DelegateMapper = delegateMapper;
            ConfigurationFactory = configurationFactory;
        }

        private Table BuildTable<TModel>(Type entityType)
            where TModel : IDataModel
        {
            IModelEntityConfiguration configuration = ConfigurationFactory.Build<TModel>(entityType);
            Table table = new Table
            {
                Schema = configuration.Schema,
                Name = configuration.Table,
            };
            foreach (PropertyInfo propertyInfo in TypeMapper.ValueTypes(entityType))
                table.Columns.Add(BuildColumn(propertyInfo));
            return table;
        }
        private Column BuildColumn(PropertyInfo propertyInfo) => new Column
        {
            Name = propertyInfo.Name,
        };


        public Dictionary<string, Table> TablesByName<TModel>()
            where TModel : IDataModel
        {
            Dictionary<string, Table> tables = new Dictionary<string, Table>();
            foreach (var tableType in TypeMapper.TableTypes<TModel>())
                tables.Add(tableType.tableName, BuildTable<TModel>(tableType.tableType));
            return tables;
        }

        public Dictionary<Type, Table> TablesByType<TModel>() 
            where TModel : IDataModel
        {
            Dictionary<Type, Table> tables = new Dictionary<Type, Table>();
            foreach (var tableType in TypeMapper.TableTypes<TModel>())
                tables.Add(tableType.tableType, BuildTable<TModel>(tableType.tableType));
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

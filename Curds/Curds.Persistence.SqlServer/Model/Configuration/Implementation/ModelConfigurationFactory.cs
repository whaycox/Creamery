using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Model.Configuration.Implementation
{
    using Abstraction;
    using Domain;
    using Persistence.Abstraction;
    using Persistence.Domain;
    using Model.Domain;
    using Model.Abstraction;

    internal class ModelConfigurationFactory : IModelConfigurationFactory
    {
        private List<IGlobalConfiguration> GlobalConfigurations { get; }
        private Dictionary<Type, List<IEntityConfiguration>> EntityConfigurations { get; }
        private Dictionary<Type, List<IModelConfiguration>> ModelConfigurations { get; }
        private Dictionary<Type, Dictionary<Type, List<IModelEntityConfiguration>>> ModelEntityConfigurations { get; } = new Dictionary<Type, Dictionary<Type, List<IModelEntityConfiguration>>>();

        public ModelConfigurationFactory(
            IEnumerable<IGlobalConfiguration> globalConfigurations,
            IEnumerable<IEntityConfiguration> entityConfigurations,
            IEnumerable<IModelConfiguration> modelConfigurations,
            IEnumerable<IModelEntityConfiguration> modelEntityConfigurations)
        {
            GlobalConfigurations = globalConfigurations
                .ToList();
            EntityConfigurations = entityConfigurations
                .GroupBy(config => config.EntityType)
                .ToDictionary(key => key.Key, value => value.ToList());
            ModelConfigurations = modelConfigurations
                .GroupBy(config => config.ModelType)
                .ToDictionary(key => key.Key, value => value.ToList());
            foreach (var modelGroup in modelEntityConfigurations.GroupBy(config => config.ModelType))
            {
                Dictionary<Type, List<IModelEntityConfiguration>> modelEntityConfigurationMap = modelGroup
                    .GroupBy(group => group.EntityType)
                    .ToDictionary(key => key.Key, value => value.ToList());
                ModelEntityConfigurations.Add(modelGroup.Key, modelEntityConfigurationMap);
            }
        }

        public CompiledConfiguration<TModel> Build<TModel>(Type entityType)
            where TModel : IDataModel
        {
            CompiledConfiguration<TModel> configuration = new CompiledConfiguration<TModel>(entityType);
            configuration.Table = entityType.Name;

            ApplyGlobalConfigurations(configuration);
            ApplyEntityConfigurations(entityType, configuration);
            ApplyModelConfigurations(configuration);
            ApplyModelEntityConfigurations(entityType, configuration);

            return configuration;
        }

        private void ApplyGlobalConfigurations<TModel>(CompiledConfiguration<TModel> configuration)
            where TModel : IDataModel
        {
            foreach (GlobalConfiguration globalConfiguration in GlobalConfigurations)
                configuration.Schema = globalConfiguration.Schema ?? configuration.Schema;
        }

        private void ApplyEntityConfigurationToCompiled<TModel>(IEntityConfiguration suppliedConfig, CompiledConfiguration<TModel> compiledConfig)
            where TModel : IDataModel
        {
            compiledConfig.Schema = suppliedConfig.Schema ?? compiledConfig.Schema;
            compiledConfig.Table = suppliedConfig.Table ?? compiledConfig.Table;
            foreach (IColumnConfiguration suppliedColumnConfig in suppliedConfig.Columns)
            {
                if (compiledConfig.Columns.TryGetValue(suppliedColumnConfig.ValueName, out CompiledColumnConfiguration<TModel> compiledColumnConfig))
                {
                    compiledColumnConfig.Name = suppliedColumnConfig.Name ?? compiledColumnConfig.Name;
                    compiledColumnConfig.IsIdentity = suppliedColumnConfig.IsIdentity ?? compiledColumnConfig.IsIdentity;
                }
                else
                {
                    CompiledColumnConfiguration<TModel> newCompiledColumnConfig = new CompiledColumnConfiguration<TModel>(suppliedColumnConfig.ValueName)
                    {
                        Name = suppliedColumnConfig.Name,
                        IsIdentity = suppliedColumnConfig.IsIdentity ?? false,
                    };
                    compiledConfig.Columns.Add(suppliedColumnConfig.ValueName, newCompiledColumnConfig);
                }
            }
        }

        private void ApplyEntityConfigurations<TModel>(Type entityType, CompiledConfiguration<TModel> configuration)
            where TModel : IDataModel
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
                return;
            else
                ApplyEntityConfigurations(entityType.BaseType, configuration);

            if (EntityConfigurations.TryGetValue(entityType, out List<IEntityConfiguration> entityConfigurations))
                foreach (IEntityConfiguration entityConfiguration in entityConfigurations)
                    ApplyEntityConfigurationToCompiled(entityConfiguration, configuration);
        }

        private void ApplyModelConfigurations<TModel>(CompiledConfiguration<TModel> configuration)
            where TModel : IDataModel
        {
            if (ModelConfigurations.TryGetValue(typeof(TModel), out List<IModelConfiguration> modelConfigurations))
                foreach (IModelConfiguration modelConfiguration in modelConfigurations)
                    configuration.Schema = modelConfiguration.Schema ?? configuration.Schema;
        }

        private void ApplyModelEntityConfigurations<TModel>(Type entityType, CompiledConfiguration<TModel> configuration)
            where TModel : IDataModel
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
                return;
            else
                ApplyModelEntityConfigurations(entityType.BaseType, configuration);

            if (ModelEntityConfigurations.TryGetValue(typeof(TModel), out Dictionary<Type, List<IModelEntityConfiguration>> allModelConfigurations))
                if (allModelConfigurations.TryGetValue(entityType, out List<IModelEntityConfiguration> modelEntityConfigurations))
                    foreach (IModelEntityConfiguration modelEntityConfiguration in modelEntityConfigurations)
                        ApplyEntityConfigurationToCompiled(modelEntityConfiguration, configuration);
        }
    }
}

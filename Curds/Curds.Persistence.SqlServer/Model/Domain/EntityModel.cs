using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Model.Domain
{
    using Abstraction;
    using Persistence.Abstraction;

    public class EntityModel : IEntityModel
    {
        public Type EntityType { get; }

        public string Schema { get; set; }
        public string Table { get; set; }

        public List<IValueModel> ValueModels { get; } = new List<IValueModel>();
        public List<IValueModel> KeyDefinition { get; } = new List<IValueModel>();

        public IEnumerable<IValueModel> Values => ValueModels;
        public IList<IValueModel> Keys => KeyDefinition.ToList();
        public IValueModel KeyValue { get; } = CreateKeyValue();
        public IValueModel Identity => ValueModels.FirstOrDefault(value => value.IsIdentity);
        public IEnumerable<IValueModel> NonIdentities => ValueModels.Where(value => !value.IsIdentity);

        public AssignIdentityDelegate AssignIdentity { get; set; }
        public ProjectEntityDelegate ProjectEntity { get; set; }
        public ValueEntityDelegate ValueEntity { get; set; }

        public EntityModel(Type entityType)
        {
            EntityType = entityType;
        }

        private static IValueModel CreateKeyValue() => new ValueModel
        {
            Name = nameof(IEntity.Keys),
            Property = typeof(IEntity).GetProperty(nameof(IEntity.Keys)),
        };
    }
}

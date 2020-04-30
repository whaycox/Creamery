using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Curds.Persistence.Model.Domain
{
    using Abstraction;
    using Domain;
    using Query.Domain;
    using Persistence.Abstraction;

    public class EntityModel : IEntityModel
    {
        public Type EntityType { get; }

        public string Schema { get; set; }
        public string Table { get; set; }

        public List<IValueModel> ValueModels { get; set; } = new List<IValueModel>();

        public IEnumerable<IValueModel> Values => ValueModels;
        public IValueModel Identity => ValueModels.FirstOrDefault(value => value.IsIdentity);
        public IEnumerable<IValueModel> NonIdentities => ValueModels.Where(value => !value.IsIdentity);

        public AssignIdentityDelegate AssignIdentity { get; set; }
        public ProjectEntityDelegate ProjectEntity { get; set; }
        public ValueEntityDelegate ValueEntity { get; set; }

        public EntityModel(Type entityType)
        {
            EntityType = entityType;
        }

    }
}

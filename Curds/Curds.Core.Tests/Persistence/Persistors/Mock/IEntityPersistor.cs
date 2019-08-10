using System.Collections.Generic;
using System.Linq;

namespace Curds.Persistence.Persistors.Mock
{
    using Persistence.Mock;

    public class IEntityPersistor : Proto.IEntityPersistor<IEntity>
    {
        protected override List<IEntity> Samples => IEntity.Samples.ToList();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Curds.Persistence.Model.Abstraction
{
    using Domain;
    using Persistence.Abstraction;

    public interface IModelBuilder
    {
        IEnumerable<IEntityModel> BuildEntityModels<TModel>()
            where TModel : IDataModel;
    }
}

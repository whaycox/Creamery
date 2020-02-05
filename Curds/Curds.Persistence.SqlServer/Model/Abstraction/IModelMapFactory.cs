using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Persistence.Model.Abstraction
{
    using Persistence.Abstraction;

    public interface IModelMapFactory
    {
        IModelMap<TModel> Build<TModel>()
            where TModel : IDataModel;
    }
}

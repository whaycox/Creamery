using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;
    using Model.Abstraction;
    using Persistence.Abstraction;

    public interface ISqlQueryWriter
    {
        void CreateTemporaryIdentityTable(IEntityModel entityModel);
        void OutputIdentitiesToTemporaryTable(IEntityModel entityModel);
        void SelectTemporaryIdentityTable(IEntityModel entityModel);
        void DropTemporaryIdentityTable(IEntityModel entityModel);

        void Insert(IEntityModel entityModel);
        void ValueEntities(IEnumerable<ValueEntity> entities);
        void Select(List<IValueModel> values);

        void From(IEntityModel entityModel);

        SqlCommand Flush();
    }
}

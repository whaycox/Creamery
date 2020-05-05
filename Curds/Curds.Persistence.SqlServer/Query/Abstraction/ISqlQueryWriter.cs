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
        void CreateTemporaryIdentityTable(ISqlTable table);
        void OutputIdentitiesToTemporaryTable(ISqlTable table);
        void SelectTemporaryIdentityTable(ISqlTable table);
        void DropTemporaryIdentityTable(ISqlTable table);

        void Insert(ISqlTable table);
        void ValueEntities(IEnumerable<ValueEntity> entities);
        void Select(IList<ISqlColumn> values);

        void From(ISqlUniverse universe);

        SqlCommand Flush();
    }
}

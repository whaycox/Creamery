using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;
    using Model.Domain;

    public interface ISqlQueryWriter
    {
        void CreateTemporaryIdentityTable(Table table);
        void OutputIdentitiesToTemporaryTable(Table table);
        void SelectTemporaryIdentityTable(Table table);
        void DropTemporaryIdentityTable(Table table);

        void Insert(Table table);
        void ValueEntities(IEnumerable<ValueEntity> entities);
        void Select(List<Column> columns);

        void From(Table table);

        SqlCommand Flush();
    }
}

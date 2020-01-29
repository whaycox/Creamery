using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Curds.Persistence.Abstraction
{
    using Query.Domain;
    using Persistence.Domain;

    public interface ISqlQueryWriter
    {
        void Insert(Table table);
        void ValueEntities(List<ValueEntity> entities);

        SqlCommand Flush();
    }
}

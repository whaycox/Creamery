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
        void Insert(Table table);
        void ValueEntities(List<ValueEntity> entities);

        SqlCommand Flush();
    }
}

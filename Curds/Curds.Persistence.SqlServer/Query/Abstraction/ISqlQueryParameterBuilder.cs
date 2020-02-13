using System.Data.SqlClient;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;

    public interface ISqlQueryParameterBuilder
    {
        string RegisterNewParamater(Value value);
        SqlParameter[] Flush();
    }
}

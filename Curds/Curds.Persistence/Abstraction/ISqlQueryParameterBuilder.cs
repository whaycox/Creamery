using System.Data.SqlClient;

namespace Curds.Persistence.Abstraction
{
    using Query.Domain;

    public interface ISqlQueryParameterBuilder
    {
        string RegisterNewParamater(Value value);
        SqlParameter[] Flush();
    }
}

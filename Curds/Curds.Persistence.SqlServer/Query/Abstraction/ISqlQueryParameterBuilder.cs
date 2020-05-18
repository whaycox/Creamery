using System.Data.SqlClient;

namespace Curds.Persistence.Query.Abstraction
{
    using Domain;

    public interface ISqlQueryParameterBuilder
    {
        object UnregisterParameter(string registeredName);
        string RegisterNewParamater(string name, object value);
        SqlParameter[] Flush();
    }
}

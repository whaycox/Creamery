using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace Curds.Persistence.Implementation
{
    using Abstraction;
    using Query.Domain;

    internal class SqlQueryParameterBuilder : ISqlQueryParameterBuilder
    {
        private Dictionary<string, SqlParameter> ParameterLookup { get; } = new Dictionary<string, SqlParameter>();

        public string RegisterNewParamater(Value value)
        {
            string paramName = GenerateParameterName(value.Name);
            ParameterLookup.Add(paramName, new SqlParameter(paramName, value.Content));
            return paramName;
        }
        private string GenerateParameterName(string valueName)
        {
            string parameterName = valueName;
            int currentValue = 1;
            while (ParameterLookup.ContainsKey(parameterName))
                parameterName = $"{valueName}{currentValue++}";

            return parameterName;
        }

        public SqlParameter[] Flush()
        {
            SqlParameter[] parameters = ParameterLookup.Values.ToArray();
            ParameterLookup.Clear();
            return parameters;
        }
    }
}

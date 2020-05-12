using System;

namespace Curds.Persistence.Query.Domain
{
    public class InvalidColumnNameException : Exception
    {
        public static string ExceptionMessage(string columnName) => $"The supplied column name '{columnName}' was not found in the result set";

        public InvalidColumnNameException(string columnName)
            : base(ExceptionMessage(columnName))
        { }

        public InvalidColumnNameException(string columnName, Exception innerException)
            : base(ExceptionMessage(columnName), innerException)
        { }
    }
}

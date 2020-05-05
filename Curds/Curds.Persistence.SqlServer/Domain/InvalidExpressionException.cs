using System;
using System.Linq.Expressions;

namespace Curds.Persistence.Domain
{
    public class InvalidExpressionException : Exception
    {
        private static string FormatMessage(Expression expression, string reason)
        {
            string append = null;
            if (!string.IsNullOrWhiteSpace(reason))
                append = $": {reason}";

            return $"{expression} is not a valid expression{append}";
        }

        public InvalidExpressionException(Expression expression, string reason = null)
            : base(FormatMessage(expression, reason))
        { }

        public InvalidExpressionException(Expression expression, Exception innerException, string reason = null)
            : base(FormatMessage(expression, reason), innerException)
        { }
    }
}

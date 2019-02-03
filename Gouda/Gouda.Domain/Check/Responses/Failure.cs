using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check.Responses
{
    using Enumerations;

    public class Failure : BaseResponse
    {
        public string Error => Arguments[nameof(Error)];

        public Failure(Exception exception)
            : this(ConvertException(exception))
        { }

        public Failure(Dictionary<string, string> arguments)
            : base(arguments, ResponseType.Failure)
        { }

        private static Dictionary<string, string> ConvertException(Exception ex)
        {
            Dictionary<string, string> toReturn = new Dictionary<string, string>();
            toReturn.Add(nameof(Error), ex.Message);
            return toReturn;
        }
    }
}

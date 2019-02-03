using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check.Responses
{
    using Enumerations;

    public class Success : BaseResponse
    {
        public Success(BaseResponse response)
            : this(response.Arguments)
        {
            if (response.Type == ResponseType.Failure)
                throw new ArgumentException("Cannot supply a failure as the source of a success");
        }

        public Success(Dictionary<string, string> arguments)
            : base(arguments, ResponseType.Success)
        { }
    }
}

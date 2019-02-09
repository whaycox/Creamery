using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check.Responses;
using Gouda.Domain.Enumerations;

namespace Gouda.Application.Check
{
    public interface IResponseHandler
    {
        Guid ID { get; }

        Status Evaluate(Success response);
    }
}
